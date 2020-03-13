using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.IO;
using CedFCIC.Models;

namespace CedFCIC
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<ReCAPTCHASettings>(Configuration.GetSection("GooglereCAPTCHA"));
            services.AddTransient<reCAPTCHAService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);  //You can set Time   
            });
            services.AddSingleton<RequestHandler>();
            services.AddHttpContextAccessor();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "My First API",
                    Description = "My First ASP.NET Core 2.0 Web API",
                    TermsOfService = new Uri("http://www.cedeira.com.ar//"),
                    Contact = new OpenApiContact() { Name = "Pablo Conde", Email = "pjeconde@yahoo.com.ar", Url = new Uri("http://www.cedeira.com.ar//") }
                });
            });

            var pathDocComentarios = Path.Combine(Directory.GetCurrentDirectory() + "\\CedFCIC.xml");

            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(pathDocComentarios);
            });

            string ambiente = Configuration.GetValue<string>("AppSettings:Ambiente");
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
          

            //Entidades.Sesion sesion = (Entidades.Sesion)Session["Sesion"];
            //Con un cnnstr utilizado con context -- > "DefaultConnection": "Server=192.168.1.99;Database=CedAC;Trusted_Connection=false;user id={0};password={1}",
            //string cnnStr = String.Format(Configuration.GetConnectionString("DefaultConnection"), usuarioDB.GetUserName(), usuarioDB.GetPassword());
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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
