using DNTPersianUtils.Core.IranCities;

using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.Mappping;

using DrMeet.Api.Shared.Persistence.DbContexts.EFCore;
using DrMeet.Api.Shared.Persistence.Repositories;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Api.Shared.Services.Blogs;
using DrMeet.Api.Shared.Services.Centers;
using DrMeet.Api.Shared.Services.CenterType;
using DrMeet.Api.Shared.Services.DoctorOnlineConsultation;
using DrMeet.Api.Shared.Services.DoctorReserveTime;
using DrMeet.Api.Shared.Services.Doctors;
using DrMeet.Api.Shared.Services.DoctorShifts;
using DrMeet.Api.Shared.Services.DoctorTariff;
using DrMeet.Api.Shared.Services.Expertise;
using DrMeet.Api.Shared.Services.HttpService;
using DrMeet.Api.Shared.Services.Insurances;
using DrMeet.Api.Shared.Services.IranCity;
using DrMeet.Api.Shared.Services.IranProvince;
using DrMeet.Api.Shared.Services.JwtService;
using DrMeet.Api.Shared.Services.Licensess;
using DrMeet.Api.Shared.Services.ParsizTeb;
using DrMeet.Api.Shared.Services.Patients;
using DrMeet.Api.Shared.Services.ServicesAvailable;
using DrMeet.Api.Shared.Services.Setting;
using DrMeet.Api.Shared.Services.Sliders;
using DrMeet.Api.Shared.Services.UserService;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using NuGet.Protocol.Core.Types;
using System;
using System.Reflection;
using System.Text;

namespace DrMeet.Api.Shared.ServiceConfigs;

public static class ServiceConfigs
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        
        #region Data
        
        //services.AddSingleton<IMongoClient>(sp =>
        //    new MongoClient(configuration.GetSection("MongoDbSettings:ConnectionString").Value));
        //     services.AddScoped<IMongoDbContext, MongoDbContext>();
        services.AddScoped(typeof(IEFCoreRepository<>), typeof(EFCoreRepository<>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
     //   services.AddScoped<ImportDoctorsService>();

        #endregion

        #region Services
        services.AddScoped<IUserService, UserService>();
        services.AddSingleton<IJwtService, JwtService>();
        services.AddSingleton<IHttpService, HttpService>();
        services.AddSingleton<IParsizTebApiService, ParsizTebApiService>();
        // test
        services.AddScoped<IDoctorShiftService, DoctorShiftService>();
        services.AddScoped<IBlogService, BlogService>();
        services.AddScoped<ISliderService, SliderService>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<ICenterService, DrMeet.Api.Shared.Services.Centers.CenterService>();
        services.AddScoped<ICenterTypeService, CenterTypeService>();
        services.AddScoped<IInsuranceService, InsuranceService>();
        services.AddScoped<ILicensesService, LicensesService>();
        services.AddScoped<ISettingService, SettingService>();
        services.AddScoped<IPatientService, PatientService>();
     //   services.AddScoped< ImportDoctorsService>();
        services.AddScoped<IServicesAvailableService, ServicesAvailableService>();
        services.AddScoped<IExpertiseService, ExpertiseService>();
        services.AddScoped<IIranProvinceService, IranProvinceService>();
        services.AddScoped<IIranCityService, IranCityService>();
        services.AddScoped<IDoctorReserveTimeService, DoctorReserveTimeService>();
        services.AddScoped<IDoctorTariffService, DoctorTariffService>();
        services.AddScoped<IDoctorOnlineConsultationService, DoctorOnlineConsultationService>();

        #endregion


        services.AddMapsterConfiguration();
      
        
        services.AddEndpoints(typeof(Program).Assembly);

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt =>
        {
            opt.CustomSchemaIds(s => s.FullName?.Replace("+", "."));
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        services.AddAuthorization();
         services.Configure<SiteSetting>(options =>
            configuration.GetSection("Setting").Bind(options));


        // services.AddDbContext<ApplicationDbContext>
        //      (opt => opt.UseSqlServer(conf.GetConnectionString("DeafultConnection")));

        #region DbContext
        services.AddDbContext<DrMeetDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DrMeetConnection")));
        #endregion
        // services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        //services.AddAutoMapper(typeof(Program).Assembly); 
        services.AddValidatorsFromAssemblyContaining<Program>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
         {
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = false,
                 ValidateAudience = false,
                 ValidateLifetime = true,
                 ValidateIssuerSigningKey = true,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Setting:SecretKey").Value!))
             };
         });

        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy", policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }
}