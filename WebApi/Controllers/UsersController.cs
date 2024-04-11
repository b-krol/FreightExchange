﻿using Application.Users;
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
    public class UsersController : BaseApiController
    {
        private IUserService UserService { get; }

        public UsersController(IUserService userService) 
        {
            UserService = userService;
        }        

        [HttpGet]
        public IEnumerable<UserDto> GetUsers()
        {
            return UserService.GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                return Ok(UserService.GetById(id));
            }
            catch(UserNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUserById(int id)
        {
            try
            {
                UserService.Delete(id);
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
        public IActionResult UpdateUser(UserDto userDto)
        {
            return Ok(UserService.Update(userDto));
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
