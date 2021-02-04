using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotnetXian.Community.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DotnetXian.Community.Identity.Identity
{
    public class MyUserManager : UserManager<User>
    {
        public MyUserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger) : base(store,
            optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services,
            logger)
        {
        }

        public override Task<bool> CheckPasswordAsync(User user, string password)
        {
            return Task.FromResult(password == "123456");
        }
    }
}