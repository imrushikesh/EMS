using EMS.Service.Interface;
using EMS.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using EMS.Models;
using EMS.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace EMS.Services.StartUpServices
{
    public static class RegisterUserCreatedServices
    {

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "JWt Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Require JWT Tokens",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EMS_API", Version = "v1" });
                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {jwtSecurityScheme, Array.Empty<string>() }
                });
            });
            return services;
        }
        public static void JwtServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtUtils, JwtUtils>();
           
        }

        public static void AddAllRequiredServices(this IServiceCollection services)
        {
            services.AddScoped<IResponse,Response>();
            services.AddScoped<IRepository<TblUser>, UserRepository>();
            services.AddScoped<IUser, UserRepository>();
            services.AddScoped<IAuthentication, AuthenticationService>();
            services.AddScoped<IRepository<TblEmployee>, EmployeeRepository>();

        }
    }
}
