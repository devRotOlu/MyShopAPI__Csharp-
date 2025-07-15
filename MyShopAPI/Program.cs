using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MyShopAPI;
using MyShopAPI.Core.Configurations;
using MyShopAPI.CustomMiddlewares;
using MyShopAPI.Data;
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
builder.Services.ConfigureSwagger();

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
        builder.WithOrigins("http://localhost:3000", "https://916d-105-112-178-131.ngrok-free.app")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

// Health checks
builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy("App is running"))
    .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!, name: "PostgreSQL");

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);

var app = builder.Build();

// Apply migrations
using (var scope = app.Services.CreateScope())
{
    try
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
    catch (Exception ex)
    {
        Console.WriteLine($"Migration error: {ex.Message}");
    }
}

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    Console.WriteLine("Running in production — skipping HTTPS redirection.");
}

app.UseCors("CorsPolicy");

app.UseWhen(ctx => !ctx.Request.Path.StartsWithSegments("/health"), branch =>
{
    branch.UseMiddleware<ExceptionMiddleware>();
    branch.UseMiddleware<CardDecryptionMiddleware>();
    branch.UseMiddleware<ProductVerificationMiddleware>();
    branch.UseMiddleware<CartProductVerificationMiddleware>();
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        try
        {
            context.Response.ContentType = "application/json";
            var result = JsonSerializer.Serialize(new
            {
                status = report.Status.ToString(),
                checks = report.Entries.Select(e => new {
                    component = e.Key,
                    status = e.Value.Status.ToString(),
                    error = e.Value.Exception?.Message
                }),
                totalDuration = report.TotalDuration.TotalMilliseconds + "ms"
            });
            await context.Response.WriteAsync(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Health check response error: {ex.Message}");
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("{ \"error\": \"Health check response failed.\" }");
        }
    }
});

app.Run();
