using AutoMapper;
using Azure;
using ContosoUniversity.DAL;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.Models;
using MatrimonioBackend.Profiles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using static MatrimonioBackend.Models.RSVPExtension;

namespace MatrimonioBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public ActionResult GetRSVPsByWedding()
        {
            var RSVPs = unitOfWork.RSVPRepository.Get(null, null, "Signer");

            var readRsvps = _mapper.Map<IEnumerable<RSVP>, IEnumerable<RSVPReadDTO>>(RSVPs);

            return Ok(readRsvps);
        }

        [HttpGet("{wedding_id}/{signer_id}")]
        public ActionResult GetRSVPsByWedding(string wedding_id, string signer_id)
        {

            var RSVPs = unitOfWork.RSVPRepository.Get((rsvp)=>rsvp.WeddingId.ToString() == wedding_id && rsvp.SignerId== Guid.Parse(signer_id) , null, "Signer");

            var readRsvps = _mapper.Map<IEnumerable<RSVP>, IEnumerable<RSVPReadDTO>>(RSVPs);

            return Ok(readRsvps);
        }

        [HttpGet("{wedding_id}")]
        public ActionResult GetRSVPsByWedding(string wedding_id)
        {

            var RSVPs = unitOfWork.RSVPRepository.Get((rsvp) => rsvp.WeddingId.ToString() == wedding_id, null, "Signer");

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
        [HttpPatch("/{RSVP_id}")]
        public ActionResult UpdateRSVP(int RSVP_id, [FromBody] JsonPatchDocument<RSVP> patch)
        {
            var RSVP = unitOfWork.RSVPRepository.GetByID(RSVP_id);

            if (RSVP == null)
            {
                return NotFound();
            }

            var Original = RSVP.DeepCopy<RSVP>();
            patch.ApplyTo(RSVP, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.Save();

            return Ok(new { original= _mapper.Map<RSVP, RSVPReadDTO>(Original), patched = _mapper.Map<RSVP, RSVPReadDTO>(RSVP) });
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




    }
}
