

using AutoMapper;
using ContosoUniversity.DAL;
using MatrimonioBackend.DTOs.Participant;
using MatrimonioBackend.DTOs.Wedding;
using MatrimonioBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MatrimonioBackend.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class WeddingController : ControllerBase
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        public IMapper _mapper { get; set; }

        public WeddingController(IMapper mapper) {
            _mapper = mapper;

        }

        [HttpGet("")]
        public ActionResult<IEnumerable<WeddingGetDTO>> GetWeddings()
        {
            var weddings = unitOfWork.WeddingRepository.Get();
            var weddingDTOs = _mapper.Map<List<WeddingGetDTO>>(weddings);

            return Ok(weddingDTOs);
        }


        [HttpGet("{wedding_id}")]
        public ActionResult<IEnumerable<WeddingGetDTO>> GetWeddingById(int wedding_id)
        {
            var wedding = unitOfWork.WeddingRepository.GetByID(wedding_id);
            var weddingDTO = _mapper.Map<WeddingGetDTO>(wedding);

            return Ok(weddingDTO);
        }

        [HttpPost("")]
        public ActionResult<WeddingGetDTO> CreateWedding(WeddingCreateDTO createWeddingDTO)
        {
            //I need automapper :)
            var wedding = _mapper.Map<WeddingCreateDTO, Wedding>(createWeddingDTO);
            unitOfWork.WeddingRepository.Insert(wedding);
            unitOfWork.Save();

            return CreatedAtAction("GetWeddingById", new { wedding_id = wedding.Id }, _mapper.Map<WeddingGetDTO>(wedding));
        }

        [HttpPut("")]
        public ActionResult UpdateWedding(WeddingUpdateDTO updateWeddingDTO)
        {
            var wedding = _mapper.Map<WeddingUpdateDTO, Wedding>(updateWeddingDTO);
            unitOfWork.WeddingRepository.Update(wedding);
            unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("")]
        public ActionResult DeleteWedding(int id)
        {
            unitOfWork.WeddingRepository.Delete(id);
            unitOfWork.Save();
            return NoContent();
        }

        [HttpPost("participant")]
        public ActionResult CreateParticipant(int user_id, int wedding_id, string role)
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
