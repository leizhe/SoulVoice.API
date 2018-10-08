using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using IdentityModel;
using log4net;
using log4net.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SV.Application;
using SV.API.Jwt;
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

	        #region Auth 
	        //将appsettings.json中的JwtSettings部分文件读取到JwtSettings中，这是给其他地方用的
	        services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

	        //由于初始化的时候我们就需要用，所以使用Bind的方式读取配置
	        //将配置绑定到JwtSettings实例中
	        var jwtSettings = new JwtSettings();
	        Configuration.Bind("JwtSettings", jwtSettings);

	        services.AddAuthentication(x =>
		        {
			        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		        })
		        .AddJwtBearer(o =>
		        {
			        o.TokenValidationParameters = new TokenValidationParameters
			        {
						NameClaimType = JwtClaimTypes.Name,
						RoleClaimType = JwtClaimTypes.Role,

						//Token颁发机构
						ValidIssuer = jwtSettings.Issuer,
				        //颁发给谁
				        ValidAudience = jwtSettings.Audience,
				        //这里的key要进行加密，需要引用Microsoft.IdentityModel.Tokens
				        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))

				        /***********************************TokenValidationParameters的参数默认值***********************************/
				        // RequireSignedTokens = true,
				        // SaveSigninToken = false,
				        // ValidateActor = false,
				        // 将下面两个参数设置为false，可以不验证Issuer和Audience，但是不建议这样做。
				        // ValidateAudience = true,
				        // ValidateIssuer = true, 
				        // ValidateIssuerSigningKey = false,
				        // 是否要求Token的Claims中必须包含Expires
				        // RequireExpirationTime = true,
				        // 允许的服务器时间偏移量
				        // ClockSkew = TimeSpan.FromSeconds(300),
				        // 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
				        // ValidateLifetime = true
			        };
		        });
	        #endregion

			services.AddMvc(option =>
            {
                option.Filters.Add(new GlobalExceptionFilter());
            });


	      
			services.AddSwaggerGen(c =>
            {
	            //c.OperationFilter<SwaggerFileUploadFilter>();//增加文件过滤处理
	            //var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } }, };
	            //c.AddSecurityRequirement(security);//添加一个必须的全局安全信息，和AddSecurityDefinition方法指定的方案名称要一致
	            //c.AddSecurityRequirement(security);
				c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "SoulVoice API",
                    Description = "For SoulVoice Web&&App",
                });

	            //var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } }, };
	            //c.(security);
				c.AddSecurityDefinition("Bearer", new ApiKeyScheme
				{
					
					Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
					Name = "Authorization",
					In = "header",
					Type = "apiKey"
				});
				//c.OperationFilter<HttpHeaderFilter>();
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

	        app.UseAuthentication();

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
