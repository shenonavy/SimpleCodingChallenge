using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SimpleCodingChallenge.Business.Actions.Employees;
using SimpleCodingChallenge.Business.Configuration;
using SimpleCodingChallenge.Common.Configuration;
using SimpleCodingChallenge.DataAccess.Database;
using SimpleCodingChallenge.DataAccess.SampleData;
using System;

namespace SimpleCodingChallenge.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; protected set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SimpleCodingChallengeConfiguration>(Configuration);

            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                });

            services.Add(ServiceDescriptor.Singleton(typeof(IOptionsSnapshot<>), typeof(OptionsManager<>)));

            services.AddMediatR(typeof(GetAllEmployeesCommandHandler).Assembly);

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddDbContext<SimpleCodingChallengeDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString(ConnectionStringKeys.SimpleCodingChallengeDatabase));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using var scope = app.ApplicationServices.CreateScope();
            var logger = scope.ServiceProvider.GetService<ILogger<Startup>>();

            try
            {
                var dbContext = scope.ServiceProvider.GetService<SimpleCodingChallengeDbContext>();
                if (dbContext != null)
                {
                    dbContext.Database.Migrate();

                    if (env.IsDevelopment())
                    {
                        dbContext.Initialize().Wait();
                    }
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Database migration/initialization failed");
                throw;
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello from Simple Coding Challenge API!");
                });
            });
        }

        public static class ConnectionStringKeys
        {
            public const string SimpleCodingChallengeDatabase = "SimpleCodingChallengeDatabase";
        }
    }
}
