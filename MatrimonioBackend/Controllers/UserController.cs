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
using System.Security.Claims;

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
        [HttpGet("ByEmail")]
        public ActionResult<UserGetDTO> GetUser()
        {
            var email = User.FindFirst("https://marrymonio.azurewebsites.net/email")?.Value;
            var emailVerified = User.FindFirst("https://marrymonio.azurewebsites.net/emailVerified")?.Value;

            if(email == null || emailVerified == null)
            {
                return NotFound("Du må ha registrert Email");
            }
            if (emailVerified=="false")
            {
                return BadRequest("You don't have a verified email");
            }

            var user = unitOfWork.UserRepository.Get((user) => user.Email == email).FirstOrDefault();
            if(user == null)
            {
                return NotFound("Unable to find the user with the specified Email");
            }

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

        [HttpPost("Social")]
        public ActionResult<UserGetDTO> CreateUserSocial(UserSocialCreateDTO createUser)
        {
            var user = _mapper.Map<UserSocialCreateDTO, MarryMonioUser>(createUser);

            var existingUser = unitOfWork.UserRepository.Get((user)=> user.Email == createUser.Email);

            if(existingUser == null)
            {
                unitOfWork.UserRepository.Insert(user);
            }



            unitOfWork.Save();

            return CreatedAtAction("CreateUser", new { id = user.Id }, _mapper.Map<UserGetDTO>(user));
        }




        [HttpPatch("{user_id}")]
        public ActionResult<UserGetDTO> UpdateUser(Guid user_id, [FromBody] JsonPatchDocument<UserUpdateDTO> patch) {
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
