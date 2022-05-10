using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using VirtualRoulette.Data.Models.DBContext;
using VirtualRoulette.Data.Users;
using VirtualRoulette.Data.Users.Entities;
using System.Configuration;
using VirtualRoulette.Commons.Constant_Strings;

namespace VirtualRoulette.Identity
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VirtualRoulette.Identity", Version = "v1" });
            });

            ExecuteMySQLConnectionstrings(services);

            services.AddMemoryCache();
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password = new PasswordOptions
                {
                    RequireDigit = false,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false,
                    RequireLowercase = false,
                    RequiredLength = 8
                };
            }).AddEntityFrameworkStores<UsersDbContext>()
            .AddDefaultTokenProviders();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VirtualRoulette.Identity v1"));
            }
            UpdateDatabase(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void ExecuteMySQLConnectionstrings(IServiceCollection services)
        {
            string mySqlConnectionStr =
                        ConfigurationManager.ConnectionStrings[ConstantStrings.UsersConnection].ConnectionString;
            //Configuration.GetConnectionString(ConstantStrings.UsersConnection);
            services.AddDbContextPool<UsersDbContext>
                (options => options.UseMySql(mySqlConnectionStr,
                ServerVersion.AutoDetect(mySqlConnectionStr)));

            services.AddDbContextPool<RouletteDBContext>
                (options => options.UseMySql(mySqlConnectionStr,
                ServerVersion.AutoDetect(mySqlConnectionStr)));
        }
        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<UsersDbContext>())
                {
                    context.Database.Migrate();
                }
                using (var context = serviceScope.ServiceProvider.GetService<RouletteDBContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
