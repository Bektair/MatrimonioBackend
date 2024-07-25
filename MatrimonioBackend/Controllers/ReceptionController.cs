using AutoMapper;
using ContosoUniversity.DAL;
using MatrimonioBackend.DTOs.Location;
using MatrimonioBackend.DTOs.Post;
using MatrimonioBackend.DTOs.Reception;
using MatrimonioBackend.DTOs.ReligiousCeremony;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Hosting;

namespace MatrimonioBackend.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [Authorize]

    public class ReceptionController : ControllerBase
    {
        IMapper _mapper;
        UnitOfWork _unitOfWork = new UnitOfWork();

        public ReceptionController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("{reception_id}")]
        public ActionResult<ReceptionReadDTO> GetReceptionById(int reception_id, string language = "")
        {
            var Reception = _unitOfWork.ReceptionRepository.Get((rc)=>rc.Id == reception_id, null, "Translations,MenuOptions,MenuOptions.Translations,Location,Location.Translations").FirstOrDefault();

            if(Reception == null)
            {
                return NotFound();
            }


            return Ok(FlatMapReceptionTranslations(Reception, language));
        }

        [HttpGet("")]
        [EnableQuery]
        public ActionResult<IEnumerable<ReceptionReadDTO>> GetReceptions(string language = ""){
            var Receptions = _unitOfWork.ReceptionRepository.Get(null, null, "Translations,MenuOptions,MenuOptions.Translations,Location,Location.Translations"); //Location

            var receptionReadDTOs =  Receptions.Select((rc)=> FlatMapReceptionTranslations(rc, language));
            return Ok(receptionReadDTOs);
        }

        [HttpPost]
        public ActionResult CreateReception(ReceptionCreateDTO createDTO)
        {
            var reception = _mapper.Map<Reception>(createDTO);

            _unitOfWork.ReceptionRepository.Insert(reception);
            _unitOfWork.Save();

            return CreatedAtAction("GetReceptionById", new { reception_id = reception.Id }, FlatMapReceptionTranslations(reception, createDTO.Language));
        }

        [HttpPatch] //Patch kan adde location TODO: Patch broken after translate
        public ActionResult PatchReception(int reception_id, [FromBody]JsonPatchDocument<Reception> patch)
        {
            var reception = _unitOfWork.ReceptionRepository.GetByID(reception_id);
            if (reception == null) return NotFound();
            var original = reception.DeepCopy<Reception>();

            patch.ApplyTo(reception, ModelState);
            if (!ModelState.IsValid) //Checks Model validation, done after binding validation
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.Save();
            return Ok(new { original = _mapper.Map<ReceptionReadDTO>(original), patch = _mapper.Map<ReceptionReadDTO>(reception) });
        }

        public static ReceptionReadDTO FlatMapReceptionTranslations(Reception reception, string language)
        {
            ReceptionTranslation? translations = (string.IsNullOrEmpty(language)) ?
                reception.Translations.FirstOrDefault((w) => w.IsDefaultLanguage) :
                reception.Translations.FirstOrDefault((w) => w.Language == language);

            if (translations == null)
            {
                translations = reception.Translations.FirstOrDefault((w) => w.IsDefaultLanguage);
            }

            LocationReadDTO location = null;

            if(reception.Location != null)
                location = LocationController.FlatMapLocationTranslations(reception.Location, language);



            return new ReceptionReadDTO()
            {
                Id = reception.Id,
                Description = (translations != null) ? translations.Description : "",
                StartDate = reception.StartDate,
                EndDate = reception.EndDate,
                Language = (translations != null) ? translations.Language : "",
                IsDefaultLanguage = (translations != null) ? translations.IsDefaultLanguage : false,
                Location = location,
                WeddingId = reception.WeddingId,
                MenuOptions = (translations != null) ? reception.MenuOptions.Select((r)=>FlatMapMenuOptionTranslations(r, translations.Language)) : null,
            };


        }

        public static MenuOptionReadDTO FlatMapMenuOptionTranslations(MenuOption location, string language)
        {
            MenuOptionTranslation? translations = (string.IsNullOrEmpty(language)) ?
                location.Translations.FirstOrDefault((w) => w.IsDefaultLanguage) :
                location.Translations.FirstOrDefault((w) => w.Language == language);

            if (translations == null)
            {
                translations = location.Translations.FirstOrDefault((w) => w.IsDefaultLanguage);
            }
            return new MenuOptionReadDTO()
            {
                Id = location.Id,
                Language = (translations != null) ? translations.Language : "",
                IsDefaultLanguage = (translations != null) ? translations.IsDefaultLanguage : false,
                DishType = (translations != null) ? translations.DishType : "",
                Image = location.Image,
                Tags = (translations != null) ? translations.Tags : ""
            };
        }

        [HttpDelete("{reception_id}")]
        public ActionResult DeleteReception(int reception_id)
        {

            _unitOfWork.ReceptionRepository.Delete(reception_id);
            _unitOfWork.Save();

            return NoContent();
        }

        [HttpPost("AddMenuOption/{reception_id}")]
        public ActionResult<MenuOptionReadDTO> AddMenuOption(int reception_id, MenuOptionCreateDTO menuOptionCreate)
        {
            var menuOption = _mapper.Map<MenuOption>(menuOptionCreate);
            var reception = _unitOfWork.ReceptionRepository.GetByID(reception_id);

            if (reception != null)
                reception.MenuOptions.Add(menuOption);
            else
                return NotFound();
            _unitOfWork.Save();

            //Gives back a version of the reception with only the new menuItem
            return Ok(FlatMapMenuOptionTranslations(menuOption, menuOptionCreate.Language));
        }

        [HttpDelete("DeleteMenuOption/{reception_id}/{menuOptionId}")]
        public ActionResult RemoveMenuOption(int reception_id, int menuOptionId)
        {
            var receptionById = _unitOfWork.ReceptionRepository.Get((x) => x.Id == reception_id, null, "MenuOptions");
            
            if(receptionById == null)
                return NotFound();

            var reception = receptionById.First();
            var menuItem = reception.MenuOptions.First((x)=>x.Id == menuOptionId);

            reception.MenuOptions.Remove(menuItem);

            _unitOfWork.Save();
            return NoContent();
        }
    }
}
