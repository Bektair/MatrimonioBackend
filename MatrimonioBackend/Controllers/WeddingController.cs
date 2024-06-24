

using AutoMapper;
using ContosoUniversity.DAL;
using MatrimonioBackend.DTOs.Participant;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.DTOs.Wedding;
using MatrimonioBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.IdentityModel.Tokens;

namespace MatrimonioBackend.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [Authorize]
    public class WeddingController : ControllerBase
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        public IMapper _mapper { get; set; }

        public WeddingController(IMapper mapper) {
            _mapper = mapper;
        }

        [HttpGet("")]
        [EnableQuery]
        public ActionResult<IEnumerable<WeddingReadDTO>> GetWeddings()
        {
            var weddings = unitOfWork.WeddingRepository.Get();
            var weddingDTOs = _mapper.Map<List<WeddingReadDTO>>(weddings);

            return Ok(weddingDTOs);
        }

        [HttpGet("participant/{participant_id}")]
        public ActionResult<IEnumerable<WeddingReadDTO>> GetWeddingsByParticipant(Guid participant_id)
        {

            var participations = unitOfWork.ParticipantRepository.Get((participant)=> participant.UserId == participant_id, null, "Wedding");

            var weddings = participations.Select((participant) => participant.Wedding);


            var weddingDTOs = _mapper.Map<List<WeddingReadDTO>>(weddings);

            return Ok(weddingDTOs);
        }


        [HttpGet("{wedding_id}")]
        public ActionResult<IEnumerable<WeddingReadDTO>> GetWeddingById(int wedding_id)
        {
            var wedding = unitOfWork.WeddingRepository.GetByID(wedding_id);
            var weddingDTO = _mapper.Map<WeddingReadDTO>(wedding);

            return Ok(weddingDTO);
        }

        [HttpPost("")]
        public ActionResult<WeddingReadDTO> CreateWedding(WeddingCreateDTO createWeddingDTO)
        {
            //I need automapper :)
            var wedding = _mapper.Map<WeddingCreateDTO, Wedding>(createWeddingDTO);
            unitOfWork.WeddingRepository.Insert(wedding);
            unitOfWork.Save();

            return CreatedAtAction("GetWeddingById", new { wedding_id = wedding.id }, _mapper.Map<WeddingReadDTO>(wedding));
        }

        [HttpPut("")]
        public ActionResult UpdateWedding(WeddingUpdateDTO updateWeddingDTO)
        {
            var wedding = _mapper.Map<WeddingUpdateDTO, Wedding>(updateWeddingDTO);
            unitOfWork.WeddingRepository.Update(wedding);
            unitOfWork.Save();

            return NoContent();
        }

        [HttpPatch("{Wedding_id}")]
        public ActionResult UpdateRSVP(int Wedding_id, [FromBody] JsonPatchDocument<WeddingUpdateDTO> patch)
        {
            var Weddings = unitOfWork.WeddingRepository.Get((wedding) => Wedding_id == wedding.id);

            if (Weddings.Count() == 0)
            {
                return NotFound();
            }
            var Wedding = Weddings.First();
            var WeddingReadOriginal = _mapper.Map<Wedding, WeddingReadDTO>(Wedding);
            var Original = WeddingReadOriginal.DeepCopy<WeddingReadDTO>();

            var weddingPatch = _mapper.Map<JsonPatchDocument<Wedding>>(patch);

            weddingPatch.ApplyTo(Wedding, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.Save();

            return Ok(new { original = Original, patched = _mapper.Map<Wedding, WeddingReadDTO>(Wedding) });
        }

        [HttpDelete("")]
        public ActionResult DeleteWedding(int id)
        {
            unitOfWork.WeddingRepository.Delete(id);
            unitOfWork.Save();
            return NoContent();
        }



        [HttpPost("participant")]
        public ActionResult CreateParticipant(Guid user_id, int wedding_id, string role)
        {
            var participant = new Participant() { UserId = user_id, WeddingId = wedding_id, Role = role };

            unitOfWork.ParticipantRepository.Insert(participant);
            unitOfWork.Save();

            return NoContent();
        }

        [HttpGet("participants/{wedding_id}")]
        public ActionResult<IEnumerable<Participant>> GetParticipants(int wedding_id, string role="")
        {
            IEnumerable<Participant> participants;
            if(role.IsNullOrEmpty())
                participants = unitOfWork.ParticipantRepository.Get((p) => p.WeddingId == wedding_id);
            else
                participants = unitOfWork.ParticipantRepository.Get((p) => p.WeddingId == wedding_id && p.Role.ToUpper() == role.ToUpper());

            return Ok(participants);
        }

        [HttpGet("participants/{wedding_id}/{user_id}")]
        public ActionResult<IEnumerable<Participant>> GetParticipant(int wedding_id, int user_id)
        {
            var participant = unitOfWork.ParticipantRepository.GetByIDComposite([wedding_id, user_id]);

            return Ok(participant);
        }

        [HttpPut("participants")]
        public ActionResult UpdateParticipant(ParticipantUpdateDTO participant)
        {
            
            unitOfWork.ParticipantRepository.Update(_mapper.Map<Participant>(participant));
            unitOfWork.Save();

            return NoContent();
        }

        //GetPeopleAcceptingWedding







    }
}
