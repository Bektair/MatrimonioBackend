using AutoMapper;
using ContosoUniversity.DAL;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.DTOs.User;
using MatrimonioBackend.DTOs.Wedding;
using MatrimonioBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MatrimonioBackend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        public UnitOfWork unitOfWork = new UnitOfWork();
        public IMapper _mapper { get; set; }

        public UserController(IMapper mapper)
        {
            _mapper = mapper;   
        }


        // GET: UserController
        [HttpGet("")]
        public ActionResult<IEnumerable<UserGetDTO>> GetUsers () 
        {
            var users = unitOfWork.UserRepository.Get();
            var map = _mapper.Map<List<UserGetDTO>>(users);

            return Ok(map);

        }


        // GET: UserController
        [HttpGet("{user_id}")]
        public ActionResult<UserGetDTO> GetUser(Guid user_id)
        {

            var user = unitOfWork.UserRepository.GetByID(user_id);

            return Ok(_mapper.Map<MarryMonioUser, UserGetDTO>(user));
        }


        // Post: UserController
        [HttpPost]
        public ActionResult<UserCreateDTO> CreateUser(UserCreateDTO createUser)
        {

            var user = _mapper.Map<UserCreateDTO, MarryMonioUser>(createUser);
            unitOfWork.UserRepository.Insert(user);

            unitOfWork.Save();

            return CreatedAtAction("CreateUser", new { id = user.Id }, user);
        }

        [HttpPatch("{user_id}")]
        public ActionResult UpdateUser(Guid user_id, [FromBody] JsonPatchDocument<UserUpdateDTO> patch) {
            var Users = unitOfWork.UserRepository.Get((user) => user_id == user.Id);
            var user = Users.FirstOrDefault();
            if (Users.Count() == 0)
            {
                return NotFound();
            }
            var WeddingReadOriginal = _mapper.Map<MarryMonioUser, UserGetDTO>(user);
            var Original = WeddingReadOriginal.DeepCopy<UserGetDTO>();

            var userPatch = _mapper.Map<JsonPatchDocument<MarryMonioUser>>(patch);

            userPatch.ApplyTo(user, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            unitOfWork.Save();

            return Ok(new { original = Original, patched = _mapper.Map<MarryMonioUser, UserGetDTO>(user) });



        }





    }
}
