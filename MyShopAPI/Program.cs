using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyShopAPI;
using MyShopAPI.Core.AuthManager;
using MyShopAPI.Core.Configurations;
using MyShopAPI.Core.EmailMananger;
using MyShopAPI.Core.IRepository;
using MyShopAPI.Core.Repository;
using MyShopAPI.CustomMiddlewares;
using MyShopAPI.Data;
using MyShopAPI.Data.Entities;
using MyShopAPI.Services.Email;
using MyShopAPI.Services.Image;
using MyShopAPI.Services.Models;
using MyShopAPI.Services.Monnify;
using MyShopAPI.Services.PayStack;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureDBContext(builder);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddNewtonsoftJson(op =>
    {
        op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        op.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

builder.Services.ConfigureIdentity();

builder.Services.ConfigureAuthentication(builder);

builder.Services.ConfigureSwagger();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailManager, EmailManager>();
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.Configure<SMTPConfig>(builder.Configuration.GetSection("SMTPConfig"));
builder.Services.AddScoped<IMonnifyService, MonnifyService>();
builder.Services.AddScoped<IPayStackService, PayStackService>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddAutoMapper(typeof(MapperInitializer));

builder.Services.AddAuthorization();

builder.Services.AddCors(corsOptions =>
{
    corsOptions.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:3000", "https://916d-105-112-178-131.ngrok-free.app")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        //.WithExposedHeaders("X-Origin");
    });
});

builder.Services.AddHealthChecks();

// Optional: Customize logging level and provider
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // Ensure Console logger is added
builder.Logging.SetMinimumLevel(LogLevel.Information);

//builder.Services.AddIdentityApiEndpoints<Customer>();

var _identityBuilder = new IdentityBuilder(typeof(Customer), typeof(IdentityRole), builder.Services); _identityBuilder.AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
var app = builder.Build();

// Apply migrations on startup
using(var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<CardDecryptionMiddleware>();
app.UseMiddleware<ProductVerificationMiddleware>();
app.UseMiddleware<CartProductVerificationMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapHealthChecks("/health");
app.MapControllers();

app.Run();
