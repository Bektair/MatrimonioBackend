using AutoMapper;
using Azure;
using ContosoUniversity.DAL;
using MatrimonioBackend.DTOs.Location;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.DTOs.User;
using MatrimonioBackend.DTOs.Wedding;
using MatrimonioBackend.Models;
using MatrimonioBackend.Models.Constants;
using MatrimonioBackend.Profiles;
using MatrimonioBackend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace MatrimonioBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class RSVPController : ControllerBase
    {

        IMapper _mapper;
        UnitOfWork unitOfWork = new UnitOfWork();

        public RSVPController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [EnableQuery]
        [HttpGet("")]
        public ActionResult GetRSVPs(string language = "")
        {
            var RSVPs = unitOfWork.RSVPRepository.Get(null, null, "Signer,MenuOrders,Translations");

            var readRsvps = RSVPs.Select((rs) => FlatMapRSVPTranslations(rs, _mapper.Map<UserGetDTO>(rs.Signer), _mapper.Map<IEnumerable<MenuOrderReadDTO>>(rs.MenuOrders), language));

            return Ok(readRsvps);
        }

        [HttpGet("{wedding_id}/{signer_id}")]
        public ActionResult GetRSVPsByWedding(string wedding_id, string signer_id, string language="")
        {

            var RSVPs = unitOfWork.RSVPRepository.Get((rsvp)=>rsvp.WeddingId.ToString() == wedding_id && rsvp.SignerId== Guid.Parse(signer_id) , null, "Signer,MenuOrders,Translations");

            var readRsvps = RSVPs.Select((rs) => FlatMapRSVPTranslations(rs, _mapper.Map<UserGetDTO>(rs.Signer), _mapper.Map<IEnumerable<MenuOrderReadDTO>>(rs.MenuOrders), language));


            return Ok(readRsvps);
        }

        [HttpGet("Wedding/{wedding_id}")]
        public ActionResult GetRSVPsByWedding(string wedding_id, string language="")
        {

            var RSVPs = unitOfWork.RSVPRepository.Get((rsvp) => rsvp.WeddingId.ToString() == wedding_id, null, "Signer,MenuOrders,Translations");

            var readRsvps = RSVPs.Select((rs) => FlatMapRSVPTranslations(rs, _mapper.Map<UserGetDTO>(rs.Signer), _mapper.Map<IEnumerable<MenuOrderReadDTO>>(rs.MenuOrders), language));

            return Ok(readRsvps);
        }

        public static RSVPReadDTO FlatMapRSVPTranslations(RSVP location, UserGetDTO marryMonioUserRead, IEnumerable<MenuOrderReadDTO> orders, string language)
        {
            RSVPTranslation? translations = (string.IsNullOrEmpty(language)) ?
                location.Translations.FirstOrDefault((w) => w.IsDefaultLanguage) :
                location.Translations.FirstOrDefault((w) => w.Language == language);

            if (translations == null)
            {
                translations = location.Translations.FirstOrDefault((w) => w.IsDefaultLanguage);
            }

            return new RSVPReadDTO()
            {
                Id = location.Id,
                Deadline = location.Deadline,
                NumberOfGuests = location.NumberOfGuests,
                Status = location.Status,
                OtherDietaryRequirements = location.OtherDietaryRequirements,
                Signer = marryMonioUserRead,
                Body = (translations != null) ? translations.Body : "",
                Language = (translations != null) ? translations.Language : "",
                IsDefaultLanguage = (translations != null) ? translations.IsDefaultLanguage : false,
                MenuOrders = orders,
            };


        }

        [HttpPost("")]
        public ActionResult CreateRSVP(RSVPCreateDTO rSVPCreateDTO)
        {
            var rsvp = _mapper.Map<RSVP>(rSVPCreateDTO);
            unitOfWork.RSVPRepository.Insert(rsvp);
            unitOfWork.Save();

            return CreatedAtAction("CreateRSVP", new {id = rsvp.Id}, FlatMapRSVPTranslations(rsvp, _mapper.Map<UserGetDTO>(rsvp.Signer), _mapper.Map<IEnumerable<MenuOrderReadDTO>>(rsvp.MenuOrders), rSVPCreateDTO.Language));
        }

        [HttpGet("{RSVP_id}")]
        public ActionResult GetRSVPById(int RSVP_id, string language = "")
        {
            var RSVP = unitOfWork.RSVPRepository.Get((rsvp) => rsvp.Id == RSVP_id, null, "Signer,MenuOrders,Translations").FirstOrDefault();
            if (RSVP == null)
            {
                return NotFound();
            }

            return Ok(FlatMapRSVPTranslations(RSVP, _mapper.Map<UserGetDTO>(RSVP.Signer), _mapper.Map<IEnumerable<MenuOrderReadDTO>>(RSVP.MenuOrders), language));
        }


        /// <summary>
        /// Kan brukes til å blant annet svare RSVP
        /// TODO: FIX
        /// </summary>
        /// <param name="RSVP_id"></param>
        /// <param name="patch"></param>
        /// <returns></returns>
        [HttpPatch("{RSVP_id}")]
        public ActionResult UpdateRSVP(int RSVP_id, [FromBody] JsonPatchDocument<RSVPUpdateDTO> patch)
        {
            var RSVPs = unitOfWork.RSVPRepository.Get((rsvp) => RSVP_id == rsvp.Id, null, "Signer");

            if (RSVPs.Count() == 0)
            {
                return NotFound();
            }
            var RSVP = RSVPs.First();
            var RSVPReadOriginal = _mapper.Map<RSVP, RSVPReadDTO>(RSVP);
            var Original = RSVPReadOriginal.DeepCopy<RSVPReadDTO>();

            var rsvpPatch = _mapper.Map<JsonPatchDocument<RSVP>>(patch);

            rsvpPatch.ApplyTo(RSVP, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.Save();

            return Ok(new { original= Original, patched = _mapper.Map<RSVP, RSVPReadDTO>(RSVP) });
        }

        [HttpDelete("{RSVP_id}")]
        public ActionResult DeleteRSVP(int RSVP_id)
        {
            bool deleted = unitOfWork.RSVPRepository.Delete(RSVP_id);

            if(!deleted)
                return NotFound();
            
            unitOfWork.Save();
            return NoContent();
        }

        [HttpPost("{rsvp_id}/Translation")]
        public ActionResult AddRSVPTranslation(string rsvp_id, RSVPTranslationCreateDTO createDTO)
        {
            var RSVP = unitOfWork.RSVPRepository.Get((rsvp) => rsvp_id == rsvp.Id.ToString(), null, "Translations").FirstOrDefault();
            if (RSVP == null) return NotFound();
            if (TranslationService.TranslationAllreadyExists(RSVP.Translations, createDTO.Language))
            {
                var translate = RSVP.Translations.FirstOrDefault((trans) => trans.Language == createDTO.Language.ToUpper());
                if(translate != null) { 
                    translate.Body = createDTO.Body;
                }
            }else { 
                var mapped = _mapper.Map<RSVPTranslation>(createDTO);
                RSVP.Translations.Add(mapped);
            }
            unitOfWork.Save();
            return NoContent();
        }

        [HttpGet("MenuOrder/{Menuorder_id}")]
        public ActionResult<MenuOrder> GetMenuOrder(int Menuorder_id)
        {
            var menuOrder = unitOfWork.MenuOrderRepository.Get((x) => x.Id == Menuorder_id).FirstOrDefault();
            if (menuOrder == null)
                return NotFound();

            return Ok(menuOrder);

        }

        [HttpGet("{RSVP_Id}/MenuOrders")]
        public ActionResult<IEnumerable<MenuOrder>> GetMenuOrders(int RSVP_Id)
        {
            var rsvp = unitOfWork.RSVPRepository.Get((x) => x.Id == RSVP_Id, null, "MenuOrders").FirstOrDefault();
            if (rsvp == null)
                return NotFound();


            return Ok(_mapper.Map< IEnumerable<MenuOrderReadDTO>>(rsvp.MenuOrders));

        }

        [HttpPost("{RSVP_Id}/AddMenuOrder")]
        public ActionResult<MenuOrder> AddMenuOder(int RSVP_Id, MenuOrderCreateDTO menuOrderCreateDTO)
        {
            var rsvp = unitOfWork.RSVPRepository.Get((x) => x.Id == RSVP_Id, null, "MenuOrders").FirstOrDefault();
            if (rsvp == null)
                return NotFound();

            var menuOrder = _mapper.Map<MenuOrder>(menuOrderCreateDTO);
            menuOrder.RSVPId = RSVP_Id;
            unitOfWork.MenuOrderRepository.Insert(menuOrder);
            unitOfWork.Save();

            return Ok(_mapper.Map<MenuOrderReadDTO>(menuOrder));
        }

        [HttpDelete("MenuOrder/{MenuOrder_Id}")]
        public ActionResult RemoveMenuOrder(int MenuOrder_Id)
        {

            unitOfWork.MenuOrderRepository.Delete(MenuOrder_Id);
            unitOfWork.Save();

            return NoContent();

        }


    }
}
