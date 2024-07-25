using AutoMapper;
using ContosoUniversity.DAL;
using MatrimonioBackend.DTOs.Location;
using MatrimonioBackend.DTOs.Participant;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.Models;
using MatrimonioBackend.Models.Constants;
using MatrimonioBackend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Linq;

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
        public ActionResult<LocationReadDTO> GetLocationById(int Location_id, string language="")
        {
            var Location = _unitOfWork.LocationRepository.Get((location)=>location.Id == Location_id, null, "Translations").FirstOrDefault();
            if(Location == null)
            {
                return NotFound();
            }


            return Ok(FlatMapLocationTranslations(Location, language));
        }

        [EnableQuery]
        [HttpGet("")]
        public ActionResult<IEnumerable<LocationReadDTO>> GetLocations(string language = "")
        {

            var Locations = _unitOfWork.LocationRepository.Get(null, null, "Translations");
            
            return Ok(Locations.Select((l)=>FlatMapLocationTranslations(l, language)));
        }

        [HttpPost]
        public ActionResult CreateLocation(LocationCreateDTO createDTO)
        {

            var Location = _mapper.Map<Location>(createDTO);

            _unitOfWork.LocationRepository.Insert(Location);
            _unitOfWork.Save();

            return CreatedAtAction("GetLocationById", new { Location_id = Location.Id }, FlatMapLocationTranslations(Location, createDTO.Language));
        }

        [HttpPatch("{Location_id}")] //Patch kan adde location
        public ActionResult PatchLocation(int Location_id, [FromBody] JsonPatchDocument<Location> patch, string language="")
        {
            var Location = _unitOfWork.LocationRepository.GetByID(Location_id);
            if (Location == null) return NotFound();
            var original = Location.DeepCopy<Location>();

            var lang  = Location.Translations.FirstOrDefault((trans) => trans.Language == language.ToUpper());

            if (lang == null)
            {
                return NotFound("Translation not found");
            }

            var locationPatch = _mapper.Map<JsonPatchDocument<Location>>(patch);
            var locationTranslationPatch = _mapper.Map<JsonPatchDocument<LocationTranslation>>(patch);

            locationPatch.ApplyTo(Location, ModelState);
            locationTranslationPatch.ApplyTo((LocationTranslation)lang, ModelState);
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

        [HttpPost("location/{location_id}/translation")]
        public ActionResult AddLocationTranslation(int location_id, LocationTranslationCreateDTO createDTO)
        {
            var location = _unitOfWork.LocationRepository.Get((l)=>l.Id ==  location_id, null, "Translations").FirstOrDefault();
            if(location == null) return NotFound();

            if(TranslationService.TranslationAllreadyExists(location.Translations, createDTO.Language))
            {
                return BadRequest("Selected language allready Exsists");
            }
            var mapped = _mapper.Map<LocationTranslation>(createDTO);
            mapped.LocationId = location_id;
            location.Translations.Add(mapped);
            _unitOfWork.Save();
            return Ok(FlatMapLocationTranslations(location, createDTO.Language));
        }

        public static LocationReadDTO FlatMapLocationTranslations(Location location, string language)
        {
            LocationTranslation? translations = (string.IsNullOrEmpty(language)) ?
                location.Translations.FirstOrDefault((w) => w.IsDefaultLanguage) :
                location.Translations.FirstOrDefault((w) => w.Language == language);

            if (translations == null)
            {
                translations = location.Translations.FirstOrDefault((w) => w.IsDefaultLanguage);  
            }
            return new LocationReadDTO()
            {
                Id = location.Id,
                Title = (translations != null) ? translations.Title : "",
                Body = (translations != null) ? translations.Body : "",
                Country = (translations != null) ? translations.Country : "",
                Address = (translations != null) ? translations.Address : "",
                Image = location.Image,
                Language = (translations != null) ? translations.Language : "",
                IsDefaultLanguage = (translations != null) ? translations.IsDefaultLanguage : false
            };
        }



    }
}
