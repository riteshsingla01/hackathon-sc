using System;
using System.IO;
using System.Reflection;
using AlsacWebApiCore.Controllers;
using AlsacWebApiCore.Middleware;
using AlsacWebApiCore.Infrastructure;
using ConstituentSearch.Repositories;
using ConstituentSearch.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace ConstituentSearch
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .MinimumLevel.ControlledBy(LogLevelControl.Switch)
                .CreateLogger();
            LogLevelControl.SetSwitchToInitialLogLevel(configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(
                    options => options.AddPolicy("Everything",
                        builder => builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
                    )
                )
                .AddResponseCaching();

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ConstituentSearch", Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<ISearchRepository, SearchRepository>();
            services.AddScoped<IConnectionFactory, ConnectionFactory>();

            VersionController.GetVersion = Delegates.Version.GetVersion;
            ConfigController.GetConfig = Delegates.Config.GetConfig;
            HealthController.GetHealth = Delegates.Health.GetHealth;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            app.UseMiddleware<TraceMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMiddleware<StackifyMiddleware.RequestTracerMiddleware>();
            }

            app.UseCors("Everything");

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "ConstituentSearch v1");
            });
        }
    }
}
