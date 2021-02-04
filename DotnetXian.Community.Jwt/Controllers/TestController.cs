using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using DotnetXian.Community.Jwt.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetXian.Community.Jwt.Controllers
{
    public class TestController : Controller
    {
        [HttpGet("userinfo")]
        public string GetCurrentUser()
        {
            var uniqueNameClaims =
                HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.UniqueName);

            return uniqueNameClaims?.Value ?? "没有用户信息";
        }

        [Authorize(Roles = "User")]
        [HttpGet("auth-test")]
        public string TestAuth()
        {
            return "授权成功，允许用户请求";
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("auth-test-role")]
        public string TestAuthRole()
        {
            return "角色验证成功，授权成功，允许用户请求";
        }

        [Authorize(Policy = PolicyNames.AtLeast18)]
        [HttpGet("auth-test-age")]
        public string TestAuthAge()
        {
            return "已满18岁，授权成功，允许用户请求";
        }
    }
}