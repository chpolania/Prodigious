﻿namespace WebApi.Filters
{
    using System.Security.Principal;

    public class BasicAuthenticationIdentity : GenericIdentity
    {
        public string Password { get; set; }

        public string UserName { get; set; }

        public int UserId { get; set; }

        public BasicAuthenticationIdentity(string userName, string password)
            : base(userName, "Basic")
        {
            Password = password;
            UserName = userName;
        }
    }
}