using FlatFinder.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatFinder.Domain.Users
{
    public static class UserErrors
    {
        public static Error NotFound = new Error("User.Found", "The user was not found");
        public static Error InvalidCredentials = new Error("User.InvalidCredentials", 
            "The user credentials were not valid");
    }
}
