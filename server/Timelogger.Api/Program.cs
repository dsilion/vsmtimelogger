using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Timelogger.Api.Middleware;
using Timelogger.Api.Seed;
using Timelogger.Application;
using Timelogger.Infrastructure;

namespace Timelogger.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure();
        builder.Services.AddControllers();
        builder.Services.AddFluentValidationAutoValidation();

        builder.Services.AddMvcCore().ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = c =>
            {
                var errors = string.Join('\n', c.ModelState.Values.Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage));

                return new BadRequestObjectResult(new
                {
                    error = errors,
                });
            };
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseCors(p => p
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());
            app.UseSwagger();
            app.UseSwaggerUI();
            app.SeedDatabase();
        }

        app.AddGlobalErrorHandler();

        app.MapControllers();

        app.UseHttpsRedirection();

        app.Run();
    }
}