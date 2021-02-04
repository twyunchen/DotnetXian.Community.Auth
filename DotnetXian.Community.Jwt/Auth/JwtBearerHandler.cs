using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace DotnetXian.Community.Jwt.Auth
{
    public class JwtBearerHandler : AuthenticationHandler<JwtBearerOptions>
    {
        public JwtBearerHandler(IOptionsMonitor<JwtBearerOptions> options, ILoggerFactory logger, UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string authorization = Request.Headers[HeaderNames.Authorization];

            if (string.IsNullOrEmpty(authorization))
            {
                return AuthenticateResult.NoResult();
            }

            string token = string.Empty;

            if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = authorization.Substring("Bearer ".Length).Trim();
            }

            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.NoResult();
            }

            // 验证JWT Token的正确性、过期时间等等
            if (ValidateJwtToken(token))
            {
                var principal = new ClaimsPrincipal();

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, "YunchenBai"),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(ClaimTypes.DateOfBirth, DateTime.Now.AddYears(-19).ToShortDateString())
                };
                var identity = new ClaimsIdentity(claims, "Bearer");
                principal.AddIdentity(identity);

                return AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name));
            }

            return AuthenticateResult.Fail("JWT Token验证失败");
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = (int) HttpStatusCode.Unauthorized;
            Response.BodyWriter.WriteAsync(Encoding.UTF8.GetBytes("请先登录"));
            return Task.CompletedTask;
        }

        protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = (int) HttpStatusCode.Forbidden;
            Response.BodyWriter.WriteAsync(Encoding.UTF8.GetBytes("你没有权限"));
            return Task.CompletedTask;
        }

        private bool ValidateJwtToken(string token)
        {
            return true;
        }
    }
}