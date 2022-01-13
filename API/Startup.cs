using System;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using API.Filters;
using Infrastructure.Persistence;
using Infrastructure.Repositories;

namespace API
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
            services.AddControllers(config =>
            {
                config.Filters.Add(new ExceptionFilter());
            });

            services.AddCors(options =>
                {
                    options.AddPolicy("Cors",
                        builder => builder.WithOrigins("https://localhost:5001S"));
                }
            );


            services.AddSwaggerGen(s => {
                s.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "Rock, Paper & Scissors", 
                    Version ="v1",
                    Description = "API Example",
                    Contact = new OpenApiContact {
                        Name ="Mani Escareno",
                        Email = "bigmander@gmail.com",
                        Url = new Uri ("https://localhost:8100")
                    }
                } );
            });

            services.AddDbContext<GameDbContext>();

            services.AddTransient<IGameRepository, GameRepository>();

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("Cors");

            app.UseSwagger();
            app.UseSwaggerUI(s => {
                s.SwaggerEndpoint("/swagger/v1/swagger.json","Rock, Paper & Scissors");
                s.RoutePrefix = String.Empty;
            });

            app.UseHttpsRedirection();


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<GameDbContext>();
                dbContext.Database.EnsureCreated();
            }
        }
    }
}
