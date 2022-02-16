using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Cinerva.Services.Common.Properties;
using Cinerva.Data;
using Microsoft.EntityFrameworkCore;
using Cinerva.Services.Common.Cities;
using Cinerva.Services.Common.PropertyTypes;
using Cinerva.Services.Common.Users;
using Microsoft.AspNetCore.Http;
using Cinerva.Web.Controllers.ActionFilter;
using Serilog;
using Azure.Storage.Blobs;
using Cinerva.Services.Common.Blob;
using Cinerva.Services.Common.PropertyImages;
using Cinerva.Web.MiddleWare;

namespace Cinerva.Web
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
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connStr = configuration.GetConnectionString("Cinerva");
            services.AddDbContext<CinervaDBContext>(args => args.UseSqlServer(connStr));
            services.AddSingleton(x => new BlobServiceClient(configuration.GetConnectionString("AzureBlobsConnectionString")));
            services.AddSingleton<IBlobService, BlobService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IPropertyImagesService, PropertyImagesService>();
            services.AddScoped<IPropertyTypeService, PropertyTypeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<UserAgentActionFilter>();
            services.AddSingleton(Log.Logger);
            services.AddSingleton(Configuration);
            services.AddControllersWithViews();
        
            services.AddMvc(options =>
            {
                options.Filters.AddService<UserAgentActionFilter>();
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseLogMiddleware();
            // doesn't work yet
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
