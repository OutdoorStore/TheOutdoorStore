using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Data;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Ecommerce_App.Models.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ecommerce_App
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Registering our DbContext

            // Products DB:
            services.AddDbContext<StoreDbContext>(options =>
            {
                // Install-Package Microsoft.EntityFrameworkCore.SqlServer
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            // Users DB:
            services.AddDbContext<UserDbContext>(options =>
            {
                // Install-Package Microsoft.EntityFrameworkCore.SqlServer
                options.UseSqlServer(Configuration.GetConnectionString("UserConnection"));
            });

            // Uses Asp.Net Identity Framework
            services.AddIdentity<Customer, IdentityRole>()
                    .AddEntityFrameworkStores<UserDbContext>()
                    .AddDefaultTokenProviders();
            // Policies for Authorizaton
            services.AddAuthorization(options => options.AddPolicy("AdminOnly", policy => policy.RequireRole(ApplicationRoles.Admin)));

            // MAPPING - register the Dependency Injection Services:
            services.AddTransient<IProductsService, ProductsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
