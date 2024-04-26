using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IDataSource Source;
        private readonly ISessionService SessionService;
        public UserAuthenticationService(IDataSource source, ISessionService sessionService)
        {
            Source = source;
            SessionService = sessionService;
        }

        public Task<Session> AuthenticateUser(string email, string password)
        {
            Source.FindUser(email, password);

            throw new NotImplementedException();
        }
    }
}
