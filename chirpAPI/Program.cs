using chirpAPI.model;
using chirpAPI.Services.Services;
using chirpAPI.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Serilog;

namespace chirpAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                                .ReadFrom.Configuration(builder.Configuration)
                                .CreateLogger();

            builder.Host.UseSerilog();


            builder.Services.AddSwaggerGen(options =>             
            {
                options.SwaggerDoc("v3", new OpenApiInfo
                {
                    Title = "Cinguettio API",
                    Version = "v3",
                    Description = "API for Cinguettio, a social media platform for sharing chirps."
                });
            });


            // Add services to the container.
            builder.Services.AddDbContext<CinguettioContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddControllers();

            builder.Services.AddScoped<IChirpsService, VasylChirpsService>();

            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAllOrigins",
            //        builder => builder.AllowAnyOrigin()
            //                          .AllowAnyMethod()
            //                          .AllowAnyHeader());
            //});

            var app = builder.Build();

            app.UseSwagger(c =>
            {
                c.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;
            });
            app.UseSwaggerUI(c =>
            {   
                c.SwaggerEndpoint("v3/swagger.json", "Cinguettio API V1");
            }); 

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            //app.UseAuthorization();

            //app.UseCors("AllowAllOrigins");

            app.MapControllers();

            app.Run();
        }
    }
}
