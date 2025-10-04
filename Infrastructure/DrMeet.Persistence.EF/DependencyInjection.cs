
using Diet.Application.Interface;
using Diet.Persistence.EF.Repository;
using DrMeet.Framework.Core.Interface;
using DrMeet.Persistence.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace DrMeet.Persistence.EF;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
          .AddPersistence(configuration);
        
        return services;
    }

    private static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddDbContext<DrMeetDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("DientConnection")));
 
        services.AddScoped<IUnitOfWork, UnitOfWork>();




        services.Scan(scan => scan
       .FromAssemblyOf<UnitOfWork>() // یا هر Repository دیگر که در همان پروژه است
       .AddClasses(c => c.AssignableTo<IService>())
           .AsImplementedInterfaces()
           .WithScopedLifetime());

        return services;
    }

}
