using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CashGiftDiary.Web.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using CashGiftDiary.Web.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CashGiftDiary.Web.Middlewares;
using Microsoft.Extensions.Options;

namespace CashGiftDiary.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<ICostRepository, CostRepository>();
            services.AddScoped<ICashGiftInRepository, CashGiftInRepository>();
            services.AddScoped<ICashGiftOutRepository, CashGiftOutRepository>();
            services.AddSingleton(Configuration);
            services.AddDbContext<DiaryDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("Default"));
            });
        }

        private static readonly string secretKey = "xuyongjie1128-18867101652";

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var options = new TokenProviderOptions
            {
                Audience = "cashgiftdiaryclient",
                Issuer = "xuyongjie",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
            };

            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options));
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = options.Audience,
                    ValidateIssuer = true,
                    ValidIssuer = options.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey=signingKey
                }
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "default",
                template: "api/{controller}/{action}/{id?}");
            });
        }
    }
}
