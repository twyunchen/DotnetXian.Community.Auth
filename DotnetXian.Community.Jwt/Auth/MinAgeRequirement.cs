using Microsoft.AspNetCore.Authorization;

namespace DotnetXian.Community.Jwt.Auth
{
    public class MinAgeRequirement : IAuthorizationRequirement
    {
        public MinAgeRequirement(int age)
        {
            Age = age;
        }

        public int Age { get; set; }
    }
}