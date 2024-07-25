using AutoMapper;
using ContosoUniversity.DAL;
using MatrimonioBackend.DTOs.Location;
using MatrimonioBackend.DTOs.Participant;
using MatrimonioBackend.DTOs.ReligiousCeremony;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace MatrimonioBackend.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [Authorize]

    public class ReligiousCeremonyController : ControllerBase
    {
        IMapper _mapper;
        UnitOfWork _unitOfWork = new UnitOfWork();

        public ReligiousCeremonyController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("{religiousCeremony_id}")]
        public ActionResult<ReligiousCeremonyReadDTO> GetReligiousCeremonyById(int religiousCeremony_id)
        {
            var ReligiousCeremony = _unitOfWork.ReligiousCeremonyRepository.Get((rc)=>rc.Id == religiousCeremony_id, null, "Location,Location.Translations,Translations");
            return _mapper.Map<ReligiousCeremonyReadDTO>(ReligiousCeremony);
        }

        [HttpGet("")]
        [EnableQuery]
        public ActionResult<IEnumerable<ReligiousCeremonyReadDTO>> GetReligiousCeremonies(string language = "")
        {

            var ReligiousCeremonies = _unitOfWork.ReligiousCeremonyRepository.Get(null,null, "Location,Location.Translations,Translations");


            return Ok(ReligiousCeremonies.Select((rc)=> FlatMapCeremonyTranslations(rc, language)));
        }

        public static ReligiousCeremonyReadDTO FlatMapCeremonyTranslations(ReligiousCeremony ceremony, string language)
        {
            ReligiousCeremonyTranslation? translations = (string.IsNullOrEmpty(language)) ?
                ceremony.Translations.FirstOrDefault((w) => w.IsDefaultLanguage) :
                ceremony.Translations.FirstOrDefault((w) => w.Language == language);

            if (translations == null)
            {
                translations = ceremony.Translations.FirstOrDefault((w) => w.IsDefaultLanguage);
            }

            var location = LocationController.FlatMapLocationTranslations(ceremony.Location, language);

            return new ReligiousCeremonyReadDTO()
            {
                Id = ceremony.Id,
                Description = (translations != null) ? translations.Description : "",
                StartDate = ceremony.StartDate,
                EndDate = ceremony.EndDate,
                Language = (translations != null) ? translations.Language : "",
                IsDefaultLanguage = (translations != null) ? translations.IsDefaultLanguage : false,
                Location = location,
                WeddingId = ceremony.WeddingId
            };


        }





        [HttpPost]
        public ActionResult CreateReligiousCeremony(ReligiousCeremonyCreateDTO createDTO)
        {

            var ReligiousCeremony = _mapper.Map<ReligiousCeremony>(createDTO);
            _unitOfWork.ReligiousCeremonyRepository.Insert(ReligiousCeremony);
            _unitOfWork.Save();

            var location = _unitOfWork.LocationRepository.Get((location) => location.Id == ReligiousCeremony.LocationId, null, "Translations").FirstOrDefault();
            if(location == null)
            {
                return BadRequest("Failure to create the associated location");
            }
            ReligiousCeremony.Location = location;

            return CreatedAtAction("GetReligiousCeremonyById", new { ReligiousCeremony_id = ReligiousCeremony.Id }, FlatMapCeremonyTranslations(ReligiousCeremony, createDTO.Language));
        }

        [HttpPatch] //Patch kan adde location
        public ActionResult PatchReligiousCeremony(int ReligiousCeremony_id, [FromBody] JsonPatchDocument<ReligiousCeremony> patch)
        {
            var ReligiousCeremony = _unitOfWork.ReligiousCeremonyRepository.GetByID(ReligiousCeremony_id);
            if (ReligiousCeremony == null) return NotFound();
            var original = ReligiousCeremony.DeepCopy<ReligiousCeremony>();

            patch.ApplyTo(ReligiousCeremony, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.Save();
            return Ok(new { original = _mapper.Map<ReligiousCeremonyReadDTO>(original), patch = _mapper.Map<ReligiousCeremonyReadDTO>(ReligiousCeremony) });
        }

        [HttpDelete("{ReligiousCeremony_id}")]
        public ActionResult DeleteReligiousCeremony(int ReligiousCeremony_id)
        {
            _unitOfWork.ReligiousCeremonyRepository.Delete(ReligiousCeremony_id);
            _unitOfWork.Save();

            return NoContent();
        }


    }
}
