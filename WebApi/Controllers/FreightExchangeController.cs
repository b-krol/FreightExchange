using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using WebApi.Controllers.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreightExchangeController : BaseApiController
    {

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
        public IEnumerable<User> GetUsers()
        {
            return _users;
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            User? us = _users.FirstOrDefault(user => user.Id == id);
            if (us == null)
            {
                return BadRequest();
            }
            return Ok(us);
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
            return Ok();//TODO what if item is not succesfully removed
        }

        [HttpPost]
        public IActionResult CreateUser(CreateUserDto dto)
        {
            User newUser = new User() { Id = _idCount++, Name = dto.Name, Email = dto.Email };
            _users.Add(newUser);
            return Created($"/users/{newUser.Id}", newUser);
        }

        //[HttpPut]
        //public IActionResult OverwriteUser()
        //{

        //}

    }
}
