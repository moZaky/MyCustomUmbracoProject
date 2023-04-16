using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using MyCustomUmbracoProject.Interfaces;
using MyCustomUmbracoProject.Services;
using System.Diagnostics;

namespace MyCustomUmbracoProject
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="webHostEnvironment">The web hosting environment.</param>
        /// <param name="config">The configuration.</param>
        /// <remarks>
        /// Only a few services are possible to be injected here https://github.com/dotnet/aspnetcore/issues/9337.
        /// </remarks>
        public Startup(IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _env = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <remarks>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940.
        /// </remarks>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddUmbraco(_env, _config)
                .AddBackOffice()
                .AddWebsite()
                .AddComposers()
                .Build();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist/umbraco-app";
            });
            services.AddScoped<IContentService, ContentService>();
        }

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The web hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            app.UseUmbraco()
                .WithMiddleware(u =>
                {
                    u.UseBackOffice();
                    u.UseWebsite();
                })
                .WithEndpoints(u =>
                {
                    u.UseInstallerEndpoints();
                    u.UseBackOfficeEndpoints();
                    u.UseWebsiteEndpoints();
                });
            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                // FileProvider = new PhysicalFileProvider(
                //    Path.Combine(env.ContentRootPath, "wwwroot/dist/client-app")),

                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
                               //   Path.Combine(env.ContentRootPath, "wwwroot/dist/umbraco-app")),
                               Path.Combine(env.ContentRootPath, "ClientApp/dist/umbraco-app")),
                RequestPath = "",
                OnPrepareResponse = ctx =>
                {
                    // Set cache control header to allow caching of static files
                    ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=31536000,immutable");
                }
            });
            app.UseEndpoints(endpoints =>
           {
               endpoints.MapControllers();

               endpoints.MapControllerRoute(
               name: "Default",
               pattern: "",
               defaults: new { controller = "Home", action = "Index" });

               //endpoints.MapFallbackToController("Index", "Home");
               endpoints.MapControllerRoute(
                               name: "default",
                               pattern: "{controller}/{action=Index}/{id?}");

               endpoints.MapFallbackToController("Index", "Home");
           });


            app.UseSpa(spa =>
            {

                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {

                    spa.UseReactDevelopmentServer(npmScript: "start-dev");
                }
                else
                {

                    spa.UseReactDevelopmentServer(npmScript: "start-prod");

                }
            });




        }
    }
}
