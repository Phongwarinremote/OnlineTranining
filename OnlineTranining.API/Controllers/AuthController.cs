using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineTranining.API.Data;
using OnlineTranining.API.Data.DataAccess;
using OnlineTranining.API.Data.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace OnlineTranining.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext appDbContext;
        private ApiResponse _response;
        private string SecretKey;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<AuthController> logger;

        public AuthController(ApplicationDbContext appDbContext, IConfiguration configuration,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            ILogger<AuthController> logger)
        {
            this.appDbContext = appDbContext;
            _response = new ApiResponse();
            SecretKey = configuration.GetValue<string>("JWTSection:SecretKey");
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser appUser = appDbContext
                        .Users.FirstOrDefault(x => x.UserName.ToLower().Equals(model.Username.ToLower()));

                    if(appUser is not null)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        _response.Result = null;
                        _response.ErrorMessages.Add("Username already exist.");

                        return BadRequest(_response);
                    }

                    ApplicationUser newUser = new ApplicationUser()
                    {
                        Id = Guid.NewGuid().ToString().Substring(1, 9),
                        UserName = model.Username,
                        Email = model.Email,
                        Firstname = model.FirstName,
                        LastName = model.LastName,
                        DepartmentId = model.DepartmentId,
                        Department = model.Department,
                        CreateDate = DateTime.UtcNow.AddHours(7),
                    };

                    // Generate Token Section
                    // --- How to implement JWT Token
                    // 1. Initial "JwtSecurityTokenHandler" object as "tokenHandler"
                    // 2. Initial byte array of "SecretKey" 
                    //   -- How to initial byte[] -- 
                    //   2.1 Inject the Dependency injection of "IConfiguration" as configuration in "Contructor"
                    //   2.2 Assign variable as "SecretKey" on top of "Constrctor"
                    //       -> Private readonly SecretKey;

                    //   2.3 Assign value of SecretKey in appsettings.json follow these step
                    //    ** In constructor **
                    //    - SecretKey = configutation.GetSection("JWTSection:SecretKey").value;
                    //   2.4 Assign byte[] in "Register" action & "Login" Action
                    //    - byte[] token = Encoding.ASCII.GetBytes(SecretKey)
                    // 3. Initial "SecurityTokenDescriptor" as securityTokenDescriptor

                    var tokenHandler = new JwtSecurityTokenHandler();
                    byte[] token = Encoding.ASCII.GetBytes(SecretKey);

                    var securityTokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, appUser.UserName.ToLower())
                        }),
                        
                    };
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Result = null;
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Bad Request.");
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occur please see the log \n {ex.Message}");
                _response.Result = null;
                _response.IsSuccess = false;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add(ex.Message);

                return BadRequest(_response);
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {

        }
    }
}
