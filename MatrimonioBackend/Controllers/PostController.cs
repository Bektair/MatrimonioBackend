using AutoMapper;
using ContosoUniversity.DAL;
using MatrimonioBackend.DTOs.Location;
using MatrimonioBackend.DTOs.Post;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.DTOs.Wedding;
using MatrimonioBackend.Models;
using MatrimonioBackend.Models.Constants;
using MatrimonioBackend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.Extensions.Hosting;
using System.Linq.Expressions;

namespace MatrimonioBackend.Controllers
{
    [Route("api/[controller]/")]
    [Authorize]
    [ApiController]
    public class PostController : ODataController
    {

        IMapper _mapper;
        UnitOfWork _unitOfWork = new UnitOfWork();
        public PostController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("{post_id}")]
        public ActionResult<PostReadDTO> GetPostById(int post_id, string language)
        {
            var post = _unitOfWork.PostRepository.Get((post)=>post.Id == post_id, null, "Translations,Images").FirstOrDefault();
            if(post == null)
            {
                return NotFound();
            }

            return Ok(FlatMapPostTranslations(post, _mapper.Map<ICollection<PostImageReadDTO>>(post.Images), language));
        }

        [HttpGet("")]
        [EnableQuery]
        public ActionResult<IEnumerable<PostReadDTO>> Get(string language = "")
        {
            //            Expression<Func<TEntity, bool>> filter = null, //We send in a lambda expression based on entity ex. student => student.LastName == "Smith"
            var userUnit = User;
            var posts = _unitOfWork.PostRepository.Get(null, null, "Translations,Images");
            if (posts == null)
            {
                return NotFound();
            }
            return Ok(posts.Select((post)=>FlatMapPostTranslations(post, _mapper.Map<ICollection<PostImageReadDTO>>(post.Images), language)));
        }

        public static PostReadDTO FlatMapPostTranslations(Post post, ICollection<PostImageReadDTO> dtos, string language)
        {
            PostTranslation? translations = (string.IsNullOrEmpty(language)) ?
                post.Translations.FirstOrDefault((w) => w.IsDefaultLanguage) :
                post.Translations.FirstOrDefault((w) => w.Language == language); 

            if (translations == null)
            {
                translations = post.Translations.FirstOrDefault((w) => w.IsDefaultLanguage);
            }
            return new PostReadDTO()
            {
                Id = post.Id,
                Title = (translations != null) ? translations.Title : "",
                Body = (translations != null) ? translations.Body : "",
                AuthorId = post.AuthorId,
                WeddingId = post.WeddingId,
                Images = dtos,
                Language = (translations != null) ? translations.Language : "",
                IsDefaultLanguage = (translations != null) ? translations.IsDefaultLanguage : false
            };
        }

        [HttpPost("")]
        public ActionResult<PostReadDTO> Create(PostCreateDTO postCreate)
        {
            var post = _mapper.Map<Post>(postCreate);
            
            _unitOfWork.PostRepository.Insert(post);
            _unitOfWork.Save();
            return CreatedAtAction("GetPostById", new { post_id = post.Id }, FlatMapPostTranslations(post, postCreate.Images, postCreate.Language));


        }

        [HttpPatch("{Post_id}")]
        public ActionResult Patch(int post_id, [FromBody] JsonPatchDocument<PostUpdateDTO> patch, string language = "")
        {
            var post = _unitOfWork.PostRepository.Get(((postGet) => postGet.Id == post_id), null, "Translations,Images").FirstOrDefault();
            if(post == null) { return NotFound();  }
            var PostRead = _mapper.Map<Post, PostReadDTO>(post);
            var originalPost = PostRead.DeepCopy<PostReadDTO>();

            var lang = post.Translations.FirstOrDefault((trans) => trans.Language == language.ToUpper());

            if (lang == null)
            {
                return NotFound("Translation not found");
            }

            var postPatch = _mapper.Map<JsonPatchDocument<Post>>(patch);
            var postTranslationPatch = _mapper.Map<JsonPatchDocument<PostTranslation>>(patch);
            

            postPatch.ApplyTo(post, ModelState);
            postTranslationPatch.ApplyTo(lang, ModelState);
                                                
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.Save();

            return Ok(new { original = _mapper.Map<PostReadDTO>(originalPost), patch = _mapper.Map<PostReadDTO>(post) });

        }
        [HttpPost("{post_id}/Translation")]
        public ActionResult AddTranslation(PostTranslationCreateDTO createDTO, int post_id, bool update)
        {
            if (!Language.IsSupported(createDTO.Language))
                return BadRequest("Selected language is not supported");

            var post = _unitOfWork.PostRepository.Get((e) => e.Id == post_id, null, "Translations").FirstOrDefault();
            if (post == null)
                return NotFound();

            if (!update && TranslationService.TranslationAllreadyExists(post.Translations, createDTO.Language))
            {
                return BadRequest("Selected language allready Exsists");
            }
            var mapped = _mapper.Map<PostTranslation>(createDTO);

            if (update)
            {
                var toBeChanged = post.Translations.FirstOrDefault((x) => x.PostId == post_id && x.Language == createDTO.Language);
                if (toBeChanged != null)
                    post.Translations.Remove(toBeChanged);
            }

            post.Translations.Add(mapped);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(int post_id)
        {
            _unitOfWork.PostRepository.Delete(post_id);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
