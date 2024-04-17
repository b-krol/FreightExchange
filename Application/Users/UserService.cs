using Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users
{
    internal class UserService : IUserService
    {
        private readonly IDataSource Source;
        public UserService(IDataSource dataSource) 
        {
            Source = dataSource;
        }

        private static UserDto CreateUserDto(User user)
        {
            return new UserDto { Name = user.Name, Email = user.Email, Id = user.Id };
        }

        private static User CreateUserFromDto(UserDto user)
        {
            var newUser = new User(user.Name, user.Email);
            if(user.Id != null)
                newUser.Id = (int)user.Id;
            return newUser;
        }


        public int Create(UserDto user)
        {
            User newUser = CreateUserFromDto(user);
            return Source.AddUser(newUser);
        }

        public void Delete(int id)
        {
            Source.DeleteUser(Source.GetUserById(id));
        }

        public IEnumerable<UserDto> GetAll()
        {
            var userDtos = new List<UserDto>();
            foreach (var user in Source.GetUsers())
            {
                userDtos.Add(CreateUserDto(user));
            }
            return userDtos;
        }

        public UserDto GetById(int id)
        {
            User user = Source.GetUserById(id);
            return CreateUserDto(user);
        }

        public async Task<UserDto> UpdateAsync(UserDto userDto)
        {
            if (!userDto.Id.HasValue)
                throw new UserNotFoundException();
            var user = Source.GetUserById(userDto.Id.Value);
            user.SetName(userDto.Name);
            user.SetEmail(userDto.Email);
            
            await Source.SaveChangesAsync();
            return CreateUserDto(user);
        }
    }
}
