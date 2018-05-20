using System;
using System.IO;
using AutoMapper;
using log4net;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SV.Application;
using SV.Common.Filters;
using SV.Common.Helpers;
using SV.Common.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace SV.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public static ILoggerRepository LoggerRepository { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //初始化log4net
            LoggerRepository = LogManager.CreateRepository("NETCoreRepository");
            Log4NetHelper.SetConfig(LoggerRepository, "Config\\log4net.config");
            
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            DbContextOption.CommandString = Configuration.GetConnectionString("CommandDB");
            DbContextOption.QueryString = Configuration.GetConnectionString("QueryDB");

            services.AddMvc(option =>
            {
                option.Filters.Add(new GlobalExceptionFilter());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "SoulVoice API",
                    Description = "For SoulVoice Web&&App",
                });
            });

            services.AddAutoMapper();
          
            return AutofacService.InitIoC(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SoulVoice API V1");
            });
        }
    }

}
