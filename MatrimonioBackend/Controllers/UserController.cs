using AutoMapper;
using ContosoUniversity.DAL;
using MatrimonioBackend.DTOs.User;
using MatrimonioBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatrimonioBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        public UnitOfWork UnitOfWork = new UnitOfWork();
        public IMapper _mapper { get; set; }

        public UserController(IMapper mapper)
        {
            _mapper = mapper;   
        }


        // GET: UserController
        [HttpGet("")]
        [Authorize("read:users")]
        public ActionResult<IEnumerable<User>> GetUsers () 
        {
            return Ok(UnitOfWork.UserRepository.Get());
        }


        // GET: UserController
        [HttpGet("{user_id}")]
        public ActionResult<UserGetDTO> GetUser(int user_id)
        {

            var user = UnitOfWork.UserRepository.GetByID(user_id);

            return Ok(_mapper.Map<User, UserGetDTO>(user));
        }


        // Post: UserController
        [HttpPost]
        public ActionResult<UserCreateDTO> CreateUser(UserCreateDTO createUser)
        {

            var user = _mapper.Map<UserCreateDTO, User>(createUser);
            UnitOfWork.UserRepository.Insert(user);

            UnitOfWork.Save();

            return CreatedAtAction("CreateUser", new { id = user.Id }, user);
        }





    }
}
