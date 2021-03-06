﻿namespace CheatPads.IdentityServer.UI.Login
{
    using System.Collections.Generic;
    using System.Linq;

    using IdentityServer4.Core.Services.InMemory;

    public class LoginService2
    {
        private readonly List<InMemoryUser> _users;

        public LoginService2(List<InMemoryUser> users)
        {
            _users = users;
        }

        public bool ValidateCredentials(string username, string password)
        {
            var user = FindByUsername(username);
            if (user != null)
            {
                return user.Password.Equals(password);
            }
            return false;
        }

        public InMemoryUser FindByUsername(string username)
        {
            return _users.FirstOrDefault(x=>x.Username.Equals(username, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}
