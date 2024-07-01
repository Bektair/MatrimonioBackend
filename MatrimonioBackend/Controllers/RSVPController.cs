using AutoMapper;
using Azure;
using ContosoUniversity.DAL;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.Models;
using MatrimonioBackend.Profiles;
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
        public ActionResult GetRSVPs()
        {
            var RSVPs = unitOfWork.RSVPRepository.Get(null, null, "Signer,MenuOrders");

            var readRsvps = _mapper.Map<IEnumerable<RSVP>, IEnumerable<RSVPReadDTO>>(RSVPs);

            return Ok(readRsvps);
        }

        [HttpGet("{wedding_id}/{signer_id}")]
        public ActionResult GetRSVPsByWedding(string wedding_id, string signer_id)
        {

            var RSVPs = unitOfWork.RSVPRepository.Get((rsvp)=>rsvp.WeddingId.ToString() == wedding_id && rsvp.SignerId== Guid.Parse(signer_id) , null, "Signer,MenuOrders");

            var readRsvps = _mapper.Map<IEnumerable<RSVP>, IEnumerable<RSVPReadDTO>>(RSVPs);

            return Ok(readRsvps);
        }

        [HttpGet("{wedding_id}")]
        public ActionResult GetRSVPsByWedding(string wedding_id)
        {

            var RSVPs = unitOfWork.RSVPRepository.Get((rsvp) => rsvp.WeddingId.ToString() == wedding_id, null, "Signer,MenuOrders");

            var readRsvps = _mapper.Map<IEnumerable<RSVP>, IEnumerable<RSVPReadDTO>>(RSVPs);

            return Ok(readRsvps);
        }

        [HttpPost("")]
        public ActionResult CreateRSVP(RSVPCreateDTO rSVPCreateDTO)
        {
            var rsvp = _mapper.Map<RSVP>(rSVPCreateDTO);
            unitOfWork.RSVPRepository.Insert(rsvp);
            unitOfWork.Save();

            return CreatedAtAction("CreateRSVP", new {id = rsvp.Id}, _mapper.Map<RSVP, RSVPReadDTO>(rsvp));
        }

        [HttpGet("/{RSVP_id}")]
        public ActionResult GetRSVPById(int RSVP_id)
        {
            var RSVP = unitOfWork.RSVPRepository.GetByID(RSVP_id);
            return Ok(_mapper.Map<RSVP, RSVPReadDTO>(RSVP));
        }


        /// <summary>
        /// Kan brukes til å blant annet svare RSVP
        /// </summary>
        /// <param name="RSVP_id"></param>
        /// <param name="patch"></param>
        /// <returns></returns>
        [HttpPatch("/api/[controller]/{RSVP_id}")]
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

        [HttpDelete("/{RSVP_id}")]
        public ActionResult DeleteRSVP(int RSVP_id)
        {
            bool deleted = unitOfWork.RSVPRepository.Delete(RSVP_id);

            if(!deleted)
                return NotFound();
            
            unitOfWork.Save();
            return NoContent();
            

        }

        [HttpGet("/MenuOrder/{Menuorder_id}")]
        public ActionResult<MenuOrder> GetMenuOrder(int Menuorder_id)
        {
            var menuOrder = unitOfWork.MenuOrderRepository.Get((x) => x.Id == Menuorder_id).FirstOrDefault();
            if (menuOrder == null)
                return NotFound();

            return Ok(menuOrder);

        }

        [HttpPost("/{RSVP_Id}/MenuOrder")]
        public ActionResult<MenuOrder> AddMenuOder(int RSVP_Id, MenuOrderCreateDTO menuOrderCreateDTO)
        {
            var rsvp = unitOfWork.RSVPRepository.Get((x) => x.Id == RSVP_Id, null, "MenuOrders").FirstOrDefault();
            if (rsvp == null)
                return NotFound();

            var menuOrder = _mapper.Map<MenuOrder>(menuOrderCreateDTO);
            menuOrder.RSVPId = RSVP_Id;
            unitOfWork.MenuOrderRepository.Insert(menuOrder);
            unitOfWork.Save();

            return Ok(menuOrder);
        }



    }
}
