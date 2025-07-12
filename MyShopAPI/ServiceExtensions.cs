using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyShopAPI.Data;
using MyShopAPI.Data.Entities;
using System.Text;

namespace MyShopAPI
{
    public static class ServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services) 
        {
            services.AddIdentity<Customer, IdentityRole>(option =>
            {
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequireDigit = true;
                option.Password.RequireLowercase = true;
                option.Password.RequireUppercase = true;

                option.User.RequireUniqueEmail = true;
                option.SignIn.RequireConfirmedEmail = true;
                option.SignIn.RequireConfirmedAccount = true;
            });
        }

        public static void ConfigureDBContext(this IServiceCollection services,WebApplicationBuilder builder) 
        {
            services.AddDbContext<DatabaseContext>(option =>
            {
                var environment = builder.Environment.EnvironmentName;

                if (environment == "Development")
                    option.UseSqlServer(builder.Configuration.GetConnectionString("sqlconnection"));
                else option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));

                option.ConfigureWarnings(warnings => warnings.Log(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
            });
        }

        public static void ConfigureAuthentication(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                  .AddCookie(options =>
                    {
                        options.Cookie.Name = "accessToken";
                    })
               .AddJwtBearer(options =>
               {
                     var jwtSettings = builder.Configuration.GetSection("Jwt");
                     var key = jwtSettings.GetSection("key").Value;
                    options.IncludeErrorDetails = true;
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = jwtSettings.GetSection("Issuer").Value,
                        ValidateLifetime = true,
                        ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)),
                        RequireExpirationTime = true,
                    };

                    // to implement httponly cookie JSON Web Token
                    options.Events = new JwtBearerEvents
                    {
                         OnMessageReceived = ctx =>
                        {
                            if (ctx.Request.Cookies.ContainsKey("accessToken"))
                            {
                                ctx.Token = ctx.Request.Cookies["accessToken"];
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen( c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                     Description = @"JWT Authorization bearer using the Bearer scheme. Enter 'Bearer' [space] and then enter your token in the text input below. Example: 'Bearer 1234abcde' ",
                     Name = "Authorization",
                    In = ParameterLocation.Header,
                    //    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                        Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer",
                                },
                                Scheme = "Oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header
                        },
                      new List<string>()
                     }
                });

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyShopAPI", Version = "v1" });
            });
        }
    }
}
