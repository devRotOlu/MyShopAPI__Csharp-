using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MyShopAPI;
using MyShopAPI.Core.AuthManager;
using MyShopAPI.Core.Configurations;
using MyShopAPI.Core.EmailMananger;
using MyShopAPI.Core.IRepository;
using MyShopAPI.Core.Repository;
using MyShopAPI.CustomMiddlewares;
using MyShopAPI.Data;
using MyShopAPI.Services.Email;
using MyShopAPI.Services.Image;
using MyShopAPI.Services.Models;
using MyShopAPI.Services.Monnify;
using MyShopAPI.Services.PayStack;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();
var environment = builder.Environment;

// Service registration
builder.Services.ConfigureDBContext(builder);
builder.Services.ConfigureIdentity();


builder.Services.ConfigureAuthentication(builder);

if (environment.IsDevelopment())
{
    builder.Services.ConfigureSwagger();
}

builder.Services.AddControllers().AddNewtonsoftJson(op =>
{
    op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    op.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
});

// Other services
builder.Services.AddAutoMapper(typeof(MapperInitializer));
builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("https://maishop.netlify.app", "http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

// Health checks
builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy("App is running"))
    .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!, name: "PostgreSQL");

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IMonnifyService, MonnifyService>();
builder.Services.AddScoped<IPayStackService, PayStackService>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<IEmailManager, EmailManager>();


// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);

var app = builder.Build();

// Apply migrations
using (var scope = app.Services.CreateScope())
{
    if (environment.IsProduction())
    {
        var db = scope.ServiceProvider.GetRequiredService<PostgresDatabaseContext>();
        db.Database.Migrate();
    }
    else
    {
        var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        db.Database.Migrate();
    }
}

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseWhen(ctx => !ctx.Request.Path.StartsWithSegments("/health"), branch =>
{
    branch.UseMiddleware<ExceptionMiddleware>();
    branch.UseMiddleware<CardDecryptionMiddleware>();
    branch.UseMiddleware<ProductVerificationMiddleware>();
    branch.UseMiddleware<CartProductVerificationMiddleware>();
});

app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Welcome to my API!");

app.MapControllers();
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new
            {
                component = e.Key,
                status = e.Value.Status.ToString(),
                error = e.Value.Exception?.Message
            }),
            totalDuration = report.TotalDuration.TotalMilliseconds + "ms"
        });
        await context.Response.WriteAsync(result);
    }
});

app.Run();
