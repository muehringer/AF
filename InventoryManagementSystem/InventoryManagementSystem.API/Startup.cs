using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using InventoryManagementSystem.Application.ObjectMappers;
using InventoryManagementSystem.Infrastructure.Configurations;
using InventoryManagementSystem.IoC;

namespace InventoryManagementSystem.API
{
    public class Startup
    {
        public IConfigurationRoot configurationRoot { get; }

        public Startup(IHostingEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            
            configurationRoot = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // MVC and Fluent Validation
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            // Swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc(configurationRoot["SwaggerVersion"].ToString(),
                    new OpenApiInfo {
                        Title = configurationRoot["SwaggerTitle"].ToString(),
                        Version = configurationRoot["SwaggerVersion"].ToString(),
                        Description = configurationRoot["SwaggerDescription"].ToString(), 
                        Contact = new OpenApiContact { Name = configurationRoot["SwaggerContactName"].ToString(),
                        Email = configurationRoot["SwaggerContactEmail"].ToString(),
                        Url = new Uri(configurationRoot["SwaggerContactUrl"].ToString()) }
                    });
            });

            // Authentication JWT - Json Web Token
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer", options => {
                var sec = Encoding.UTF8.GetBytes(configurationRoot["SecretKey"].ToString());

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidAudience = "The name of audience",
                    ValidateIssuer = false,
                    ValidIssuer = "The name of issuer",

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(sec),
                    ValidateLifetime = true,
                };
            });

            // Cors
            services.AddCors();

            // Keys Configuration
            services.AddOptions();
            services.Configure<KeysConfig>(configurationRoot);

            // AutoMapper
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Inject dependency native
            var ioc = new InjectorContainer();
            services = ioc.GetScope(services);

            services.AddSingleton<IConfiguration>(configurationRoot);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Data compression
            services.AddResponseCompression();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment environment)
        {
            if (environment.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseAuthentication();

            // Cors
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", configurationRoot["SwaggerTitle"].ToString());
            });

            // Data compression
            app.UseResponseCompression();
        }
    }
}
