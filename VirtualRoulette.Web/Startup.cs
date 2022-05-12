using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using VirtualRoulette.Data.Models.DBContext;
using System.Configuration;
using VirtualRoulette.Commons.Constant_Strings;
using VirtualRoulette.Common.Abstractions.Repositories;
using VirtualRoulette.Repository.Repositories;

namespace VirtualRoulette.Web
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VirtualRoulette.Web", Version = "v1" });
            });


            string mySqlConnectionStr =
                        //ConfigurationManager.ConnectionStrings[ConstantStrings.UsersConnection].ConnectionString;
                        Configuration.GetConnectionString(ConstantStrings.UsersConnection);
            ExecuteMySQLConnectionstrings(services, mySqlConnectionStr);

            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            AddRepositoriesAndServices(services);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VirtualRoulette.Web v1"));
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

        private static void ExecuteMySQLConnectionstrings(IServiceCollection services, string mySqlConnectionStr)
        {
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
                var context = serviceScope.ServiceProvider.GetService<RouletteDBContext>();
                context.Database.Migrate();

            }
        }
        private static void AddRepositoriesAndServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, RouletteDBContext>();

            services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IPersonService, PersonService>();

            //services.AddScoped<IPhoneRepository, PhoneRepository>();
            //services.AddScoped<IPhoneService, PhoneService>();

            //services.AddScoped<IRelationRepository, RelationRepository>();
            //services.AddScoped<IRelationService, RelationService>();
        }
    }
}
