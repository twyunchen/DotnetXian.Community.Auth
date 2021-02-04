using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DotnetXian.Community.Jwt.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace DotnetXian.Community.Jwt.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("token")]
        public JwtTokenResponse LoginAndGetJwtToken([FromBody] LoginRequest request)
        {
            var now = DateTime.Now;
            var tokenExpiredTime = TimeSpan.FromMinutes(15);

            if (request.Password == "123456")
            {
                throw new Exception("用户名或密码错误");
            }

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: "DemoAuthServer", //令牌颁发者
                claims: new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, "YunchenBai")
                },
                notBefore: now,
                expires: now.Add(tokenExpiredTime),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes("FADE86AB-F38A-43EA-8608-8554329B5D52")),
                    SecurityAlgorithms.HmacSha256)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return new JwtTokenResponse
            {
                Token = token,
                ExpireInSeconds = tokenExpiredTime.TotalSeconds,
                AuthenticationScheme = JwtBearerDefaults.AuthenticationScheme,
                UserName = request.UserName
            };
        }
    }
}