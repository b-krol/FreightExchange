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


        public int Create(UserDto user)
        {
            User newUser = new User() { Id = (Source.GetUsers().Count() + 1), Name = user.Name, Email = user.Email }; //TODO przyznawanie id nowo tworzonym userom
            Source.CreateUser(newUser);
            return newUser.Id;
        }

        public void Delete(UserDto user)
        {
            try
            {
                Source.DeleteUser(new User() { Id = (Source.GetUsers().Count() + 1), Name = user.Name, Email = user.Email });//TODO przyznawanie id nowo tworzonym userom
            }
            catch(UserNotDeletedException) 
            {
                throw;
            }
            catch (UserNotFoundException)
            {
                throw;
            }
        }

        public IEnumerable<UserDto> GetAll()
        {
            var usersDto = new List<UserDto>();
            foreach (var user in Source.GetUsers())
            {
                usersDto.Add(CreateUserDto(user));
            }
            return usersDto;
        }

        public UserDto GetById(int id)
        {
            User? user = Source.GetUsers().FirstOrDefault(user => user.Id == id);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            return CreateUserDto(user);
        }

        public UserDto Update(UserDto user)
        {
            return CreateUserDto(Source.UpdateUser(new User() { Id = (int)user.Id, Name = user.Name, Email = user.Email }));
        }
    }
}
