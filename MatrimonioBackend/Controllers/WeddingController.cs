

using AutoMapper;
using ContosoUniversity.DAL;
using MatrimonioBackend.DTOs.Participant;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.DTOs.Wedding;
using MatrimonioBackend.Models;
using MatrimonioBackend.Models.Constants;
using MatrimonioBackend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder.Core.V1;

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
        public ActionResult<IEnumerable<WeddingReadDTO>> GetWeddings(string language = "")
        {
           if(!IsLanguageSupported(language))
           {
                return BadRequest("Unsupported Language: " + language);
           }

            var weddings = unitOfWork.WeddingRepository.Get(null, null, "Translations");


            var weddingDTOs = weddings.Select((w)=> FlatMapTranslations(w, language));



            return Ok(weddingDTOs);
        }

        private WeddingReadDTO FlatMapTranslations(Wedding wedding, string language="")
        {
             var defaultLanguage = wedding.Translations.FirstOrDefault((w) => w.IsDefaultLanguage);

             var translations = (string.IsNullOrEmpty(language)) ? defaultLanguage
                : wedding.Translations.FirstOrDefault((w) => w.Language == language);

            if (translations == null)
            {
                translations = defaultLanguage;
            }

            return new WeddingReadDTO()
            {
                backgroundImage = wedding.backgroundImage,
                bodyFont = wedding.bodyFont,
                headingFont = wedding.headingFont,
                id = wedding.id,
                picture = wedding.picture,
                primaryColor = wedding.primaryColor,
                primaryFontColor = wedding.primaryFontColor,
                secoundaryColor = wedding.secoundaryColor,
                secoundaryFontColor = wedding.secoundaryFontColor,
                language = (translations != null) ? translations.Language : "",
                description = (translations != null) ? translations.Description : "",
                dresscode = (translations != null) ? translations.Dresscode : "",
                title = (translations != null) ? translations.Title : "",
                isDefaultLanguage = (translations != null) ? (translations.IsDefaultLanguage) ? true : false : true,
                defaultLanguage = (defaultLanguage != null) ? defaultLanguage.Language : ""
            };




        }

        private bool IsLanguageSupported(string language)
        {

            if (!Language.supportedLanguages.Contains(language))
            {
                if (!string.IsNullOrEmpty(language))
                {
                    return false;
                }
            }
            return true;
        }

        [HttpGet("participant/{participant_id}")]
        public ActionResult<IEnumerable<WeddingReadDTO>> GetWeddingsByParticipant(Guid participant_id, string language="")
        {

            var participations = unitOfWork.ParticipantRepository.Get((participant)=> participant.UserId == participant_id, null, "Wedding,Wedding.Translations");

            var weddings = participations.Select((participant) => participant.Wedding);


            var weddingDTOs = weddings.Select((w) => FlatMapTranslations(w, language));

            return Ok(weddingDTOs);
        }


        [HttpGet("{wedding_id}")]
        public ActionResult<IEnumerable<WeddingReadDTO>> GetWeddingById(int wedding_id, string language="")
        {
            var wedding = unitOfWork.WeddingRepository.Get((wedding) => wedding.id == wedding_id, null, "Translations").FirstOrDefault();
            if (wedding == null) return NotFound();

            return Ok(FlatMapTranslations(wedding, language));
        }

        [HttpPost("")]
        public ActionResult<WeddingReadDTO> CreateWedding(WeddingCreateDTO createWeddingDTO)
        {
            //I need automapper :)
            var wedding = _mapper.Map<WeddingCreateDTO, Wedding>(createWeddingDTO);
            unitOfWork.WeddingRepository.Insert(wedding);
            unitOfWork.Save();

            return CreatedAtAction("GetWeddingById", new { wedding_id = wedding.id }, FlatMapTranslations(wedding, createWeddingDTO.language));
        }

        //[HttpPut("")]
        //public ActionResult UpdateWedding(WeddingUpdateDTO updateWeddingDTO)
        //{

        //    var wedding = _mapper.Map<WeddingUpdateDTO, Wedding>(updateWeddingDTO);
        //    unitOfWork.WeddingRepository.Update(wedding);
        //    unitOfWork.Save();

        //    return NoContent();
        //}

        [HttpPost("{Wedding_id}/translation")]
        public ActionResult AddTranslation(WeddingTranslationCreateDTO weddingTranslations, int Wedding_id)
        {
            if(!Language.IsSupported(weddingTranslations.language)) 
                return BadRequest("Selected language is not supported");

            var wedding = unitOfWork.WeddingRepository.Get((e)=> e.id == Wedding_id, null, "Translations").FirstOrDefault();
            if (wedding == null)
                return NotFound();

            if(TranslationService.TranslationAllreadyExists(wedding.Translations, weddingTranslations.language)) {
                return BadRequest("Selected language allready Exsists");
            }
            var mapped = _mapper.Map<WeddingTranslation>(weddingTranslations);
            wedding.Translations.Add(mapped);
            unitOfWork.Save();
            return NoContent();
        }


        [HttpPatch("{Wedding_id}")]
        public ActionResult PatchWedding(int Wedding_id, [FromBody] JsonPatchDocument<WeddingUpdateDTO> patch, string language="")
        {
            var Weddings = unitOfWork.WeddingRepository.Get((wedding) => Wedding_id == wedding.id, null, "Translations");

            if (Weddings.Count() == 0)
            {
                return NotFound("Wedding not found");
            }
            var Wedding = Weddings.First();
            var WeddingReadOriginal = _mapper.Map<Wedding, WeddingReadDTO>(Wedding);
            var Original = WeddingReadOriginal.DeepCopy<WeddingReadDTO>();

            var lang = Wedding.Translations.FirstOrDefault((trans) => trans.Language == language.ToUpper());

            if (lang == null)
            {
                return NotFound("Translation not found");
            }

            var weddingPatch = _mapper.Map<JsonPatchDocument<Wedding>>(patch);
            var weddingTranslationPatch = _mapper.Map<JsonPatchDocument<WeddingTranslation>>(patch);
            //Vit hvilken Translation som skal oppdateres, lag patch for denne og?



            weddingPatch.ApplyTo(Wedding, ModelState);
            weddingTranslationPatch.ApplyTo((WeddingTranslation)lang, ModelState);
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
        public ActionResult CreateParticipant(ParticipantCreateDTO createDTO)
        {
            var translation = _mapper.Map<ParticipantTranslation>(createDTO);
            translation.WeddingId = createDTO.WeddingId;
            translation.UserId = createDTO.UserId;

            var participant = new Participant() { UserId = createDTO.UserId, WeddingId = createDTO.WeddingId, Translations = new List<ParticipantTranslation>() { translation } };

            unitOfWork.ParticipantRepository.Insert(participant);
            unitOfWork.Save();

            return CreatedAtAction("GetParticipant", new { wedding_id = createDTO.WeddingId, user_id = createDTO.UserId }, FlatMapParticipantTranslations(participant, createDTO.Language));
        }

        [HttpGet("participants/{wedding_id}")]
        public ActionResult<IEnumerable<ParticipantReadDTO>> GetParticipants(int wedding_id, string lang="", string role="")
        {
            IEnumerable<Participant> participants;
            if(role.IsNullOrEmpty())
                participants = unitOfWork.ParticipantRepository.Get((p) => p.WeddingId == wedding_id, null, "Translations");
            else {
                participants = unitOfWork.ParticipantRepository.Get((p) => p.WeddingId == wedding_id, null, "Translations");
                var withRole = participants.Where((p)=>p.Translations.Count(t=>t.Role.ToUpper() == role.ToUpper() && t.Language.ToUpper() == lang.ToUpper()) > 0).ToList();
            }

            return Ok(participants.Select((e)=> FlatMapParticipantTranslations(e, lang)));
        }

        [HttpGet("participants/{wedding_id}/{user_id}")]
        public ActionResult<IEnumerable<ParticipantReadDTO>> GetParticipant(int wedding_id, Guid user_id, string lang="")
        {
            var participant = unitOfWork.ParticipantRepository.Get((p)=>p.WeddingId == wedding_id && p.UserId == user_id, null, "Translations").FirstOrDefault();
            if(participant == null)
            {
                return BadRequest("Participant Not found");
            }

            var language = TranslationService.GetLangOrDefaultTranslation(participant.Translations, lang);
            if(language == null)
            {
                return UnprocessableEntity("Cannot find the language specified nor a default one");
            }

            return Ok(FlatMapParticipantTranslations(participant, language.Language));
        }

        [HttpPut("participants")]
        public ActionResult UpdateParticipant(ParticipantUpdateDTO participant)
        {
            
            unitOfWork.ParticipantRepository.Update(_mapper.Map<Participant>(participant));
            unitOfWork.Save();

            return NoContent();
        }

        [HttpPost("participants/{wedding_id}/{user_id}/translation")]
        public ActionResult PostParticipantTranslation(ParticipantTranslationCreateDTO createDTO, int wedding_id, Guid user_id)
        {
            var participant = unitOfWork.ParticipantRepository.Get((e) => e.WeddingId == wedding_id && e.UserId == user_id, null, "Translations").FirstOrDefault();
            if (participant == null)
                return NotFound();


            if (TranslationService.TranslationAllreadyExists(participant.Translations, createDTO.Language))
            {
                return BadRequest("Selected language allready Exsists");
            }

            var mapped = _mapper.Map<ParticipantTranslation>(createDTO);
            mapped.UserId = user_id;
            mapped.WeddingId = wedding_id;

            participant.Translations.Add(mapped);
            

            unitOfWork.Save();
            return Ok(FlatMapParticipantTranslations(participant, createDTO.Language));

        }

        private ParticipantReadDTO FlatMapParticipantTranslations(Participant participant, string language)
        {
            ParticipantTranslation? translations = (string.IsNullOrEmpty(language)) ?
                participant.Translations.FirstOrDefault((w) => w.IsDefaultLanguage) : 
                participant.Translations.FirstOrDefault((w) => w.Language == language);

            if (translations == null)
            {
                translations = participant.Translations.FirstOrDefault((w) => w.IsDefaultLanguage);
            }

            return new ParticipantReadDTO()
            {
                UserId = participant.UserId,
                WeddingId = participant.WeddingId,
                Language = (translations != null) ? translations.Language : "",
                Role = (translations != null) ? translations.Role : "",
                IsDefaultLanguage = (translations != null) ? (translations.IsDefaultLanguage) ? true : false : true,
            };


        }






    }
}
