using Microsoft.AspNetCore.ResponseCompression;

using ConwaysGameOfLife.Common.API.Settings;
using ConwaysGameOfLife.Server.REST.Providers;

namespace ConwaysGameOfLife.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddResponseCompression((options) =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                JSONSerializeOptions.SetSerializationOptions(options.JsonSerializerOptions);
            });
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title="ConwaysGameOfLife.Board",
                    Description = "Board service"
                });
            });

            builder.Services.AddSingleton<GameProvider>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
