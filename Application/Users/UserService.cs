using AutoMapper;
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
        private readonly IMapper Mapper;

        public UserService(IDataSource dataSource, IMapper mapper) 
        {
            Source = dataSource;
            Mapper = mapper;
        }

        public async Task<int> Add(UserDto user)
        {
            User newUser = new User(user.Name, @"/-/asł0", user.Email);
            await Source.AddUser(newUser);
            await Source.SaveChangesAsync();
            return newUser.Id;
        }

        public async Task Delete(int id)
        {
            await Source.DeleteUser(await Source.GetUserById(id));
            await Source.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users = await Source.GetUsers();
            return Mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetById(int id)
        {
            User user = await Source.GetUserById(id);
            return Mapper.Map<UserDto>(user);
        }

        public async Task<int> UpdateAsync(UserDto userDto)
        {
            if (!userDto.Id.HasValue)
                throw new UserNotFoundException();
            var user = await Source.GetUserById(userDto.Id.Value);
            user.SetName(userDto.Name);
            user.SetEmail(userDto.Email);
            
            await Source.SaveChangesAsync();
            return user.Id;
        }
    }
}
