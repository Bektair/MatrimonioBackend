using AutoMapper;
using ContosoUniversity.DAL;
using MatrimonioBackend.DTOs.Location;
using MatrimonioBackend.DTOs.Post;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.Models;
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
        public ActionResult<Post> GetPostById(int post_id, string language)
        {
            var post = _unitOfWork.PostRepository.Get((post)=>post.Id == post_id, null, "Translations,Images").FirstOrDefault();
            if(post == null)
            {
                return NotFound();
            }

            return Ok(FlatMapPostTranslations(post, language));
        }

        [HttpGet("")]
        [EnableQuery]
        public ActionResult<IEnumerable<Post>> Get(string language = "")
        {
            //            Expression<Func<TEntity, bool>> filter = null, //We send in a lambda expression based on entity ex. student => student.LastName == "Smith"

            var posts = _unitOfWork.PostRepository.Get(null, null, "Translations,Images");
            if (posts == null)
            {
                return NotFound();
            }
            return Ok(posts.Select((post)=>FlatMapPostTranslations(post, language)));
        }

        public PostReadDTO FlatMapPostTranslations(Post post, string language)
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
                Images = _mapper.Map<ICollection<PostImageReadDTO>>(post.Images),
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
            return CreatedAtAction("GetPostById", new { post_id = post.Id }, _mapper.Map<PostReadDTO>(post));


        }

        [HttpPatch("")]
        public ActionResult Patch(int post_id, [FromBody] JsonPatchDocument<Post> patch)
        {
            var post = _unitOfWork.PostRepository.GetByID(post_id);
            if(post == null) { return NotFound();  }

            var originalPost = post.DeepCopy<Post>();

            patch.ApplyTo(post, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.Save();

            return Ok(new { original = _mapper.Map<PostReadDTO>(originalPost), patch = _mapper.Map<PostReadDTO>(post) });

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
