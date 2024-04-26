using Application.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAuthenticationController : ControllerBase
    {
        private readonly IUserAuthenticationService AuthenticationService;

        public UsersAuthenticationController(IUserAuthenticationService authenticationService)
        {
            AuthenticationService = authenticationService;
        }

        [HttpPost]
        public IActionResult Authenticate(string email, string password)
        {
            return Ok();
        }
    }
}
