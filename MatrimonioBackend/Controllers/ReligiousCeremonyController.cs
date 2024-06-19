using AutoMapper;
using ContosoUniversity.DAL;
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
            var ReligiousCeremony = _unitOfWork.ReligiousCeremonyRepository.GetByID(religiousCeremony_id);
            return _mapper.Map<ReligiousCeremonyReadDTO>(ReligiousCeremony);
        }

        [HttpGet("")]
        [EnableQuery]
        public ActionResult<IEnumerable<ReligiousCeremonyReadDTO>> GetReligiousCeremonys()
        {

            var ReligiousCeremonys = _unitOfWork.ReligiousCeremonyRepository.Get(null,null,"Location");

            return _mapper.Map<List<ReligiousCeremonyReadDTO>>(ReligiousCeremonys);
        }

        [HttpPost]
        public ActionResult CreateReligiousCeremony(ReligiousCeremonyCreateDTO createDTO)
        {

            var ReligiousCeremony = _mapper.Map<ReligiousCeremony>(createDTO);

            _unitOfWork.ReligiousCeremonyRepository.Insert(ReligiousCeremony);
            _unitOfWork.Save();

            return CreatedAtAction("GetReligiousCeremonyById", new { ReligiousCeremony_id = ReligiousCeremony.Id }, _mapper.Map<ReligiousCeremonyReadDTO>(ReligiousCeremony));
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
