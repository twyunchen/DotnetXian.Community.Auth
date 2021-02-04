using DotnetXian.Community.Jwt.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using JwtBearerHandler = DotnetXian.Community.Jwt.Auth.JwtBearerHandler;
using JwtBearerOptions = DotnetXian.Community.Jwt.Auth.JwtBearerOptions;

namespace DotnetXian.Community.Jwt
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "DotnetXian.Community.Jwt", Version = "v1"});
            });

            // 注册认证相关服务
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                // .AddJwtBearer()
                .AddScheme<JwtBearerOptions, JwtBearerHandler>("Bearer", config => { });

            services.AddSingleton<IAuthorizationHandler, MinAgeAuthorizationHandler>();

            // 注册授权服务
            services.AddAuthorization(config =>
            {
                config.AddPolicy(PolicyNames.AtLeast18,
                    policy => { policy.Requirements.Add(new MinAgeRequirement(18)); });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotnetXian.Community.Jwt v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            // 将授权服务加入到Request Middleware中
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}