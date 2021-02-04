namespace DotnetXian.Community.Jwt.Models
{
    public class JwtTokenResponse
    {
        public string AuthenticationScheme { get; set; }

        public string Token { get; set; }

        public double ExpireInSeconds { get; set; }

        public string UserName { get; set; }
    }
}