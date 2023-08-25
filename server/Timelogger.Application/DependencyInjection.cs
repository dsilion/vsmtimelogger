using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;
using IMapper = Timelogger.Application.Common.IMapper;
using Mapper = Timelogger.Application.Common.Mapper;

namespace Timelogger.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IMapper, Mapper>();
        services.AddScoped(_ => new MapperConfiguration(cfg => cfg.AddMaps(typeof(Mapper).Assembly))
            .CreateMapper());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(p => p.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }

}