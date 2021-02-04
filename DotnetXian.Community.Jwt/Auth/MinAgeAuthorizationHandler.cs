using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DotnetXian.Community.Jwt.Auth
{
    public class MinAgeAuthorizationHandler : AuthorizationHandler<MinAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            MinAgeRequirement requirement)
        {
            var dateOfBirthClaim = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.DateOfBirth);

            if (dateOfBirthClaim != null)
            {
                if (DateTime.TryParse(dateOfBirthClaim.Value, out DateTime dateOfBirth))
                {
                    if (dateOfBirth.AddYears(requirement.Age) < DateTime.Now)
                    {
                        context.Succeed(requirement);
                    }
                }
            }
            
            return Task.CompletedTask;
        }
    }
}