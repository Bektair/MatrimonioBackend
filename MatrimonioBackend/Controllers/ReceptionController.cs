﻿using AutoMapper;
using ContosoUniversity.DAL;
using MatrimonioBackend.DTOs.Post;
using MatrimonioBackend.DTOs.Reception;
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
        public ActionResult<ReceptionReadDTO> GetReceptionById(int reception_id)
        {
            var Reception = _unitOfWork.ReceptionRepository.GetByID(reception_id);

            if(Reception == null)
            {
                return NotFound();
            }


            return _mapper.Map<ReceptionReadDTO>(Reception);
        }

        [HttpGet("")]
        [EnableQuery]
        public ActionResult<IEnumerable<ReceptionReadDTO>> GetReceptions(){
            var Receptions = _unitOfWork.ReceptionRepository.Get(null, null, "MenuOptions,Location"); //Location

            var receptionReadDTOs =  _mapper.Map<List<ReceptionReadDTO>>(Receptions.ToList());
            return receptionReadDTOs;
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
            return Ok( _mapper.Map<MenuOptionReadDTO>(menuOption));
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
