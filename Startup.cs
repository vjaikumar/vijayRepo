using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExploreLondon
{
    public class Startup
    {
        public readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //depedny injection done here
            services.AddTransient<FeatureToggles>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,FeatureToggles features)
        {
            app.UseExceptionHandler("/error.html");

            if (features.DeveloperException)
            {
                app.UseDeveloperExceptionPage();

            }
            //////if (env.IsDevelopment())
            //////{
            //////    app.UseDeveloperExceptionPage();
            //////}

        //  app.UseRouting();
            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Path.Value.StartsWith("/hello"))
            //    {
            //        await context.Response.WriteAsync("Hello ASP.NET Core!");
            //    }

            //    await next();
            //});


            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync(" How are you?");
            //});

            ////app.UseEndpoints(endpoints =>
            ////{
            ////    endpoints.MapGet("/", async context =>
            ////    {
            ////        await context.Response.WriteAsync("Hello  Krish!");
            ////    });
            ////});
            ///

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("invalid"))
                    throw new Exception("ERROR!");

                await next();
            });

            app.UseFileServer();

           

            //app.UseEndpoints(endpoints =>
            //{
            //   // endpoints.MapHub<ChatHub>("/chat");
            //    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            //});

            app.UseStaticFiles();
        }
    }
}
