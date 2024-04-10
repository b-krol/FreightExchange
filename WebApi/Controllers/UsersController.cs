using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using WebApi.Controllers.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        private static UserDto CreateUserDto(User user)
        {
            return new UserDto { Name = user.Name, Email = user.Email, Id = user.Id };
        }

        private static List<User> _users = new List<User>()
        {
            new Entities.User()
            {
                Id = 1,
                Name = "Test",
                Email = "Test"
            },
            new Entities.User()
            {
                Id = 2,
                Name = "Test2",
                Email = "Test2"
            }
        };

        private static int _idCount = _users.Count + 1;

        [HttpGet]
        public IEnumerable<UserDto> GetUsers()
        {
            var usersDto = new List<UserDto>();
            foreach (var user in _users)
            {
                usersDto.Add(CreateUserDto(user));
            }
            return usersDto;
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            User? user = _users.FirstOrDefault(user => user.Id == id);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(CreateUserDto(user));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUserById(int id)
        {
            User? user = _users.FirstOrDefault(user => user.Id == id);
            if (user == null)
            {
                return BadRequest();
            }
            if (_users.Remove(user))
            {
                return Ok();
            }
            //TODO what if item is not succesfully removed || log information?
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost]
        public IActionResult CreateUser(UserDto dto)
        {
            User newUser = new User() { Id = _idCount++, Name = dto.Name, Email = dto.Email };
            _users.Add(newUser);
            return Created($"{Request.GetEncodedUrl()}/{newUser.Id}", CreateUserDto(newUser));
        }

        [HttpPut("{id}")]
        public IActionResult OverwriteUser(int id, UserDto dto)
        {
            User? user = _users.FirstOrDefault(user => user.Id == id);
            if (user == null)
            {
                return BadRequest();
            }
            user.Name = dto.Name;
            user.Email = dto.Email;
            return Ok(CreateUserDto(user));
        }

        [HttpPatch("{id}")]
        public IActionResult ChangeUserName(int id, string? name, string? email)
        {
            User? user = _users.FirstOrDefault(user => user.Id == id);
            if (user == null)
                return BadRequest();

            if (name != null)
                user.Name = name;

            if(email != null)
                user.Email = email;

            return Ok(CreateUserDto(user));
        }

    }
}
