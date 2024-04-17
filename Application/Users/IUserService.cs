using Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAll();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Users.UserNotFoundException">Method throws UserNotFoundException when can't find specified user</exception>
        Task<UserDto> GetById(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Users.UserNotFoundException">Method throws UserNotFoundException when can't find specified user</exception>
        /// <exception cref="Users.UserNotDeletedException">Method throws UserNotDeletedException when can't delete specified user</exception>
        void Delete(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Users.UserNotCreatedException">Method throws UserNotCreatedException when can't create specified user</exception>
        int Create(UserDto user);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Users.UserNotFoundException">Method throws UserNotFoundException when can't find specified user</exception>
        /// <exception cref="Users.UserNotUpdatedException">Method throws UserNotUpdatedException when can't update specified user</exception>
        Task<UserDto> UpdateAsync(UserDto user);
    }
}
