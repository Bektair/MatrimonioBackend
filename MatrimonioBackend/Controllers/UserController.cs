using MatrimonioBackend.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlManager.Models;
using MySqlManager.Repository;

namespace MatrimonioBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;


        public UserController() //IUserRepository repository
        {
            //_repo = repository;
            this._repo = new UserRepository(new DAL.WeddingContext());

        }



        // GET: UserController
        [HttpGet("")]
        public ActionResult<IEnumerable<User>> GetUsers () 
        {

            return Ok(_repo.GetAll());
        }

        // GET: UserController
        [HttpGet("{user_id}")]
        public async Task<ActionResult<GetUserDTO>> GetUser(int user_id, bool isBride)
        {

            var user = _repo.GetUserById(user_id);

            return Ok(new GetUserDTO() { Id = user_id, Keycloakid = user.Keycloakid, Username = user.Username});
        }


        // Post: UserController
        [HttpPost]
        public ActionResult<CreateUserDTO> CreateUser(CreateUserDTO createUser)
        {
            //Context.User.Add(user)
            //Context.SaveChanges()
            User user = new User()
            {
                Keycloakid = createUser.Keycloakid,
                Username = createUser.Username,
            };

            _repo.InsertUser(user);
            _repo.Save();

            return CreatedAtAction(nameof(User), new { id = 2 }, user);
        }



    }
}
