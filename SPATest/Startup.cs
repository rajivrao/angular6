using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrototypeApi;
using PrototypeApi.DbModels;
using IdentityServer4.AccessTokenValidation;

namespace SPATest
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
            services.AddDbContext<ApiContext>(options =>
                    options.UseInMemoryDatabase(databaseName: "Add_writes_to_database"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
               .AddIdentityServerAuthentication(options =>
               {
                   options.Authority = "https://demo.identityserver.io";
                   options.RequireHttpsMetadata = true;

                   options.ApiName = "api";
               });

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            // Seed database
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApiContext>();
                SeedData(context);                
            }

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        private static void SeedData(ApiContext context)
        {
            context.Products.Add(new Product() { Name = "A001", Price = 99.99M });
            context.Products.Add(new Product() { Name = "A002", Price = 89.99M });
            context.Products.Add(new Product() { Name = "A003", Price = 99.99M });
            context.Products.Add(new Product() { Name = "A004", Price = 19.99M });
            context.Products.Add(new Product() { Name = "A005", Price = 39.99M });
            context.Products.Add(new Product() { Name = "A006", Price = 59.99M });
            context.Products.Add(new Product() { Name = "A007", Price = 69.99M });
            context.Products.Add(new Product() { Name = "A008", Price = 55.99M });
            context.Products.Add(new Product() { Name = "A009", Price = 67.99M });
            context.Products.Add(new Product() { Name = "A010", Price = 23.99M });
            context.Products.Add(new Product() { Name = "A011", Price = 22.99M });
            context.Products.Add(new Product() { Name = "A012", Price = 12.99M });
            context.Products.Add(new Product() { Name = "A013", Price = 67.99M });
            context.Products.Add(new Product() { Name = "A014", Price = 67.99M });
            context.Products.Add(new Product() { Name = "A015", Price = 45.99M });
            context.Products.Add(new Product() { Name = "A016", Price = 12.99M });
            context.Products.Add(new Product() { Name = "A017", Price = 7.99M });
            context.Products.Add(new Product() { Name = "A018", Price = 9.99M });
            context.Products.Add(new Product() { Name = "A019", Price = 19.99M });
            context.Products.Add(new Product() { Name = "A020", Price = 20.99M });
            context.Products.Add(new Product() { Name = "B001", Price = 99.99M });
            context.Products.Add(new Product() { Name = "B002", Price = 89.99M });
            context.Products.Add(new Product() { Name = "B003", Price = 99.99M });
            context.Products.Add(new Product() { Name = "B004", Price = 19.99M });
            context.Products.Add(new Product() { Name = "B005", Price = 39.99M });
            context.Products.Add(new Product() { Name = "B006", Price = 59.99M });
            context.Products.Add(new Product() { Name = "B007", Price = 69.99M });
            context.Products.Add(new Product() { Name = "B008", Price = 55.99M });
            context.Products.Add(new Product() { Name = "B009", Price = 67.99M });
            context.Products.Add(new Product() { Name = "B010", Price = 23.99M });

            context.SaveChanges();
        }
    }
}
