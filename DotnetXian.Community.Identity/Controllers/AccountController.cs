using System.Threading.Tasks;
using DotnetXian.Community.Identity.Identity;
using DotnetXian.Community.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotnetXian.Community.Identity.Controllers
{
    public class AccountController : Controller
    {
        // private readonly UserManager<User> _userManager;
        private readonly MyUserManager _userManager;
        
        public AccountController(MyUserManager userManager)
        {
            _userManager = userManager;
        }

        // public AccountController(UserManager<User> userManager)
        // {
        //      _userManager = userManager;
        // 

        [HttpPost("/auth/login")]
        public async Task<string> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            var loginResult = await _userManager.CheckPasswordAsync(user, request.Password);

            return loginResult ? "登录成功" : "用户名或密码错误";
        }
    }
}