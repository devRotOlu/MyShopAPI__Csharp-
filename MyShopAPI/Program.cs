using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyShopAPI.Core.AuthManager;
using MyShopAPI.Core.Configurations;
using MyShopAPI.Core.EmailMananger;
using MyShopAPI.Core.IRepository;
using MyShopAPI.Core.Repository;
using MyShopAPI.customMiddlewares;
using MyShopAPI.Data;
using MyShopAPI.Data.Entities;
using MyShopAPI.Services.Email;
using MyShopAPI.Services.Image;
using MyShopAPI.Services.Models;
using MyShopAPI.Services.Monnify;
using MyShopAPI.Services.PayPal;
using MyShopAPI.Services.RSA;
using Newtonsoft.Json.Converters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DatabaseContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("sqlconnection"));
    option.ConfigureWarnings(warnings => warnings.Log(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddNewtonsoftJson(op =>
    {
        op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        op.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

builder.Services.AddIdentity<Customer, IdentityRole>(option =>
{
    option.Password.RequireNonAlphanumeric = true;
    option.Password.RequireDigit = true;
    option.Password.RequireLowercase = true;
    option.Password.RequireUppercase = true;

    option.User.RequireUniqueEmail = true;
    option.SignIn.RequireConfirmedEmail = true;
    option.SignIn.RequireConfirmedAccount = true;
});

builder.Services.AddAuthentication(options =>
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



builder.Services.AddSwaggerGen(
    c =>
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
    }
);


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailManager, EmailManager>();
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.Configure<SMTPConfig>(builder.Configuration.GetSection("SMTPConfig"));
builder.Services.AddScoped<IPayPalService, PayPalService>();
builder.Services.AddScoped<IMonnifyService, MonnifyService>();
builder.Services.AddTransient<IRSAService, RSAService>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddAutoMapper(typeof(MapperInitializer));

builder.Services.AddSingleton(x =>
{
    return new PayPalClient
    {
        BaseUrl = builder.Configuration["PayPal:BaseURI"]!,
        ClientId = builder.Configuration["PayPal:ClientID"]!,
        ClientSecret = builder.Configuration["PayPal:SecretKey"]!,
        Mode = builder.Configuration["PayPal:Mode"]!,
    };
});

/// new
builder.Services.AddAuthorization();

builder.Services.AddCors(corsOptions =>
{
    corsOptions.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

//builder.Services.AddIdentityApiEndpoints<Customer>();

var _identityBuilder = new IdentityBuilder(typeof(Customer), typeof(IdentityRole), builder.Services); _identityBuilder.AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseMiddleware<ConfirmPurchaseMiddleware>();
//app.UseMiddleware<CardDecryptionMiddleware>();
app.UseMiddleware<ProductVerificationMiddleware>();
app.UseMiddleware<CartProductVerificationMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
