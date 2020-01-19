using Innocv.Common.Dates;
using Innocv.UserTechTest.Api.ErrorHandling;
using Innocv.UserTechTest.Bus.Services;
using Innocv.UserTechTest.Persistence.Base;
using Innocv.UserTechTest.Persistence.InMemory;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Innocv.UserTechTest.Api
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
            services.AddScoped<UserDbContext>(x =>
            {
                var options = new DbContextOptionsBuilder<UserDbContext>()
                    .UseInMemoryDatabase(databaseName: "ProductionDb")
                    .Options;
                return new UserDbContext(options);
            });

            services.AddScoped<IUserRepository, UserRepositoryInMemory>();
            services.AddScoped<IDateTimeProvider, DefaultDateTimeProvider>();
            services.AddMediatR(typeof(GetUsersRequest));

            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "User TechTest Api",
                    Description = "A simple API for Innocv",
                    Contact = new OpenApiContact
                    {
                        Name = "Javier Roman",
                        Email = "jjromanc@gmail."
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            // The logger that I'm using is the console log, that is simplified, in general I use nlog or log4net.
            if (env.IsDevelopment())
            {
                logger.LogInformation("Here we going to test this!!");
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureExceptionHandler(logger);

            app.UseHttpsRedirection();

            app.UseRouting();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "User TechTest API V1");
            });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}