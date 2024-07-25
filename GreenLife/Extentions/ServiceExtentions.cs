using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Contracts;
using Service;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Asp.Versioning;
using GreenLife.Presentation.Controllers;
using FluentValidation.AspNetCore;
using FluentValidation;
using Entities.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GreenLife.Extentions
{
    public static class ServiceExtentions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
                                                  services.AddCors(options =>
                                                  {
                                                      options.AddPolicy("CorsPolicy", builder =>
                                                      builder.AllowAnyOrigin()
                                                      .AllowAnyMethod()
                                                      .AllowAnyHeader()
                                                      .WithExposedHeaders("X-Pagination"));
                                                  });
        public static void ConfigureIISIntegration(this IServiceCollection services) =>
                                            services.Configure<IISOptions>(options => { });

        public static void ConfigureLoggerService(this IServiceCollection services) => services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
                                           services.AddDbContext<RepositoryContext>(opts =>
                                           opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
                                    services.AddScoped<IRepositoryManager, RepositoryManager>();
        public static void ConfigureServiceManager(this IServiceCollection services) =>
                                         services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<DoctorValidator>();
            services.AddFluentValidationAutoValidation();
        }

        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
            }).AddMvc(opt =>
            {
                opt.Conventions.Controller<DoctorController>()
                   .HasApiVersion(new ApiVersion(1, 0));

            });
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = "GreenLife_2024";
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Green Life API",
                    Version = "v1"
                });

            });
        }

    }
}
