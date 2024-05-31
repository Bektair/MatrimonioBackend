using AutoMapper;
using ContosoUniversity.DAL;
using MatrimonioBackend.DTOs.Post;
using MatrimonioBackend.DTOs.Reception;
using MatrimonioBackend.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace MatrimonioBackend.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class ReceptionController : ControllerBase
    {
        IMapper _mapper;
        UnitOfWork _unitOfWork = new UnitOfWork();

        public ReceptionController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("{reception_id}")]
        public ActionResult<ReceptionReadDTO> GetReceptionById(int reception_id)
        {
            var Reception = _unitOfWork.ReceptionRepository.GetByID(reception_id);
            return _mapper.Map<ReceptionReadDTO>(Reception);
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<ReceptionReadDTO>> GetReceptions(){

            var Receptions = _unitOfWork.ReceptionRepository.Get(null, null, "Location");
            return _mapper.Map<List<ReceptionReadDTO>>(Receptions);
        }

        [HttpPost]
        public ActionResult CreateReception(ReceptionCreateDTO createDTO)
        {

            var reception = _mapper.Map<Reception>(createDTO);

            _unitOfWork.ReceptionRepository.Insert(reception);
            _unitOfWork.Save();

            return CreatedAtAction("GetReceptionById", new { reception_id = reception.Id }, _mapper.Map<ReceptionReadDTO>(reception));
        }

        [HttpPatch] //Patch kan adde location
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

        [HttpDelete("{reception_id}")]
        public ActionResult DeleteReception(int reception_id)
        {

            _unitOfWork.ReceptionRepository.Delete(reception_id);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
