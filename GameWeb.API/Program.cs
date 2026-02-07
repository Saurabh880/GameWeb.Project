using Asp.Versioning;
using GameWeb.API.DbContextRepo;
using GameWeb.API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

using System;
using System.Reflection;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddMemoryCache();

//Adding content type
builder.Services.AddControllers(options =>
    {
        options.Filters.Add(new ProducesAttribute("application/json"));
        options.Filters.Add(new ConsumesAttribute("application/json"));
    })
    .AddXmlSerializerFormatters();

builder.Services.AddApiVersioning(config =>
{
    config.ApiVersionReader = new UrlSegmentApiVersionReader();
    
    config.AssumeDefaultVersionWhenUnspecified = true; //This will assume the default version when no version is specified in the request
    config.DefaultApiVersion = new ApiVersion(1, 0);//This will set the default version to 1.0
    config.ReportApiVersions = true; //This will add the api version information to the response headers
}).AddApiExplorer(
    options =>
    {
        options.SubstituteApiVersionInUrl = true;
        options.GroupNameFormat = "'v'VVV";//This will format the version in the URL as v1, v2, etc.
    }); ;

builder.Services.AddDbContext<GameDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnGame")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.AllowAnyOrigin()
              .WithMethods("GET", "POST", "PUT");
    });
});

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", limiterOptions =>
    {
        limiterOptions.PermitLimit = 10;
        limiterOptions.Window = TimeSpan.FromSeconds(10);
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        limiterOptions.QueueLimit = 10;
    });
});


// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Version = "1.0",
        Title = "Game Web API"
    });
    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Version = "2.0",
        Title = "Game Web API"
    });
});//generates OpenAPI specifications


builder.Services.AddScoped<CacheMemRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setUpAction =>
    {
        setUpAction.SwaggerEndpoint("/swagger/v1/swagger.json", "1.0");
        setUpAction.SwaggerEndpoint("/swagger/v2/swagger.json", "2.0");
        setUpAction.RoutePrefix = string.Empty; // /This will set the Swagger UI to be served at the root of the application
    });

}

app.UseHsts();
// Routing must come before authentication/authorization
app.UseRouting();

// CORS should be placed after routing but before authentication
app.UseCors("AllowSpecificOrigin");

// Authentication first, then Authorization
app.UseAuthentication();
app.UseAuthorization();

// Rate limiting or other custom middleware
app.UseRateLimiter();

try
{
    app.MapControllers();
}
catch (ReflectionTypeLoadException ex)
{
    foreach (var loaderEx in ex.LoaderExceptions)
    {
        Console.WriteLine(loaderEx?.Message);
    }
    throw;
}

app.Run();