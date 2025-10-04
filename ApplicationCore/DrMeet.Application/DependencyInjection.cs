//using Diet.Application.Interface;
//using Diet.Application.Service;
//using Diet.Framework.Core.Authentication;
//using Diet.Framework.Core.Bus;
//using Diet.Framework.Core.Utility;
//using FluentValidation;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;


//namespace DrMeet.Application;

//public static class DependencyInjection
//{
//    public static IServiceCollection AddApplication(
//        this IServiceCollection services,
//        ConfigurationManager configuration)
//    {
         
//        var assembly = typeof(DependencyInjection).Assembly;

//        services.Scan(scan => scan
//            .FromAssemblies(assembly)
//            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
//                .AsImplementedInterfaces()
//                .WithScopedLifetime()

//            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
//                .AsImplementedInterfaces()
//                .WithScopedLifetime()

//            .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
//                .AsImplementedInterfaces()
//                .WithScopedLifetime()
//        );

//        services.AddScoped<DietService>();
//        services.AddSingleton<IPDFService, PDFService>();
//        services.AddScoped<IEncrypter, Encrypter>();
//        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
//        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
//        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
//        return services;
//    }
//}
