using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAuthenticationController : ControllerBase
    {
        [HttpPost]
        public IActionResult Authenticate(string email, string password)
        {
            return Ok();
        }
    }
}
