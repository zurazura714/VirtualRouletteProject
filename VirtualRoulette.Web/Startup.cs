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


            //var a = Configuration.GetConnectionString("UsersConnection");
            var b = ConfigurationManager.ConnectionStrings["UsersConnection"].ConnectionString;
            //var text = @"data source=(localdb)mssqllocaldb;initial catalog=usTest;integrated security=True;MultipleActiveResultSets=True";
            //if (b == text)
            //{

            //}
            services.AddDbContext<RouletteDBContext>(options =>
                options.UseSqlServer(b));
            //services.AddDbContext<RouletteDBContext>(options =>
            //   options.UseSqlServer(ConfigurationManager.ConnectionStrings["UsersConnection"].ConnectionString.ToString()));

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

            using (var serviceScope = app.ApplicationServices
             .GetRequiredService<IServiceScopeFactory>()
             .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<RouletteDBContext>())
                {
                    context.Database.Migrate();
                }
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
