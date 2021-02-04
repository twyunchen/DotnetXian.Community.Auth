using DotnetXian.Community.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotnetXian.Community.Identity.Store
{
    public class AuthDbContext : IdentityDbContext<User, Role, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Server=localhost;Database=IdentitySample;uid=postgres;pwd=123321;Integrated Security=True");
        }
    }
}