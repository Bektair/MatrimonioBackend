using AutoMapper;
using ContosoUniversity.DAL;
using MatrimonioBackend.DTOs.Post;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
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
        public ActionResult<Post> GetPostById(int post_id)
        {
            var post = _unitOfWork.PostRepository.GetByID(post_id);

            return Ok(post);
        }

        [HttpGet("")]
        [EnableQuery]
        public ActionResult<IEnumerable<Post>> Get()
        {
//            Expression<Func<TEntity, bool>> filter = null, //We send in a lambda expression based on entity ex. student => student.LastName == "Smith"

            var posts = _unitOfWork.PostRepository.Get();
            
            return Ok(posts);
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
