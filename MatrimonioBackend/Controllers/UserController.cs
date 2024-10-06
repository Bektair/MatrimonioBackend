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
using Microsoft.OpenApi.Models;
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
        public IConfiguration _configuration { get; set; }

        public UserController(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;

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

        [HttpPost("{email}")]
        public async Task<ActionResult<string>> MakeEmailInvitationLink(string email)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://dev-fnrkz1kw46cdu7zy.us.auth0.com/api/v2/tickets/password-change");

            request.Headers.Add("Accept", "application/json");



            request.Headers.Add("Authorization", "Bearer "+ _configuration.GetValue<string>("MANAGEMENT_TOKEN"));

            var content = new StringContent("{\"result_url\":\"https://marrymonio.azurewebsites.net\",\"email\": \"" + email +"\",\"connection_id\":\"con_f9M7acTOfS9rtXDF\"}", null, "application/json");

            request.Content = content;

            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var responseTxt = await response.Content.ReadAsStringAsync();

            responseTxt += "#type=invite" + "#app=AppName";

            Console.WriteLine(responseTxt);

            return Ok(responseTxt);
        }

        [HttpGet("{token}")]
        public async Task<ActionResult<string>> GetToken()
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://dev-fnrkz1kw46cdu7zy.us.auth0.com/oauth/token");
            request.Headers.Add("Accept", "application/json");
            var client_secret = _configuration.GetValue<string>("CLIENT_SECRET");
            var client_id = _configuration.GetValue<string>("CLIENT_ID");
            var contentStr = "{\"client_id\":\"" + $"{client_id}" + "\",\"client_secret\":\"" + $"{client_secret}" + "\",\"audience\":\"https://matrimoniobackend20240430143958.azurewebsites.net\",\"grant_type\":\"client_credentials\"}";

            var content = new StringContent("{\"client_id\":\"" + $"{client_id}" + "\",\"client_secret\":\"" + $"{client_secret}" + "\",\"audience\":\"https://matrimoniobackend20240430143958.azurewebsites.net\",\"grant_type\":\"client_credentials\"}", null, "application/json");
            request.Content = content;

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseTxt = await response.Content.ReadAsStringAsync();

            return Ok(responseTxt);

        }

        



    }
}
