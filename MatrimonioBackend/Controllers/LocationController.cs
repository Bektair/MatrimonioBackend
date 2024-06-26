﻿using AutoMapper;
using ContosoUniversity.DAL;
using MatrimonioBackend.DTOs.Location;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.Models;
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
    public class LocationController : ControllerBase
    {
        IMapper _mapper;
        UnitOfWork _unitOfWork = new UnitOfWork();

        public LocationController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("{Location_id}")]
        public ActionResult<LocationReadDTO> GetLocationById(int Location_id)
        {
            var Location = _unitOfWork.LocationRepository.GetByID(Location_id);
            return _mapper.Map<LocationReadDTO>(Location);
        }

    [EnableQuery]
        [HttpGet("")]
        public ActionResult<IEnumerable<LocationReadDTO>> GetLocations()
        {

            var Locations = _unitOfWork.LocationRepository.Get();
            return _mapper.Map<List<LocationReadDTO>>(Locations);
        }

        [HttpPost]
        public ActionResult CreateLocation(LocationCreateDTO createDTO)
        {

            var Location = _mapper.Map<Location>(createDTO);

            _unitOfWork.LocationRepository.Insert(Location);
            _unitOfWork.Save();

            return CreatedAtAction("GetLocationById", new { Location_id = Location.Id }, _mapper.Map<LocationReadDTO>(Location));
        }

        [HttpPatch("{Location_id}")] //Patch kan adde location
        public ActionResult PatchLocation(int Location_id, [FromBody] JsonPatchDocument<Location> patch)
        {
            var Location = _unitOfWork.LocationRepository.GetByID(Location_id);
            if (Location == null) return NotFound();
            var original = Location.DeepCopy<Location>();

            patch.ApplyTo(Location, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.Save();
            return Ok(new { original = _mapper.Map<LocationReadDTO>(original), patch = _mapper.Map<LocationReadDTO>(Location) });
        }

        [HttpDelete("{Location_id}")]
        public ActionResult DeleteLocation(int Location_id)
        {
            _unitOfWork.LocationRepository.Delete(Location_id);
            _unitOfWork.Save();

            return NoContent();
        }




    }
}
