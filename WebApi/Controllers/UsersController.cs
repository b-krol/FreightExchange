using Application.Users;
using Domain.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService UserService { get; }

        public UsersController(IUserService userService) 
        {
            UserService = userService;
        }        

        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            return await UserService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                return Ok( await UserService.GetById(id));
            }
            catch(UserNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            try
            {
                await UserService.Delete(id);
                return Ok();
            }
            catch(UserNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (UserNotDeletedException exception) 
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateUser(UserDto userDto)
        {
            int id = UserService.Create(userDto);
            return Created($"{Request.GetEncodedUrl()}/{id}", UserService.GetById(id));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDto userDto)
        {
            return Ok(await UserService.UpdateAsync(userDto));
        }

        //[HttpPatch("{id}")]
        //public IActionResult ChangeUserName(int id, string? name, string? email)
        //{
        //    User? user = _users.FirstOrDefault(user => user.Id == id);
        //    if (user == null)
        //        return BadRequest();

        //    if (name != null)
        //        user.Name = name;

        //    if(email != null)
        //        user.Email = email;

        //    return Ok(CreateUserDto(user));
        //}

    }
}
