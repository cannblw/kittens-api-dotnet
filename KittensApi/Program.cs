using KittensApi.Adapters;
using KittensApi.Config;
using KittensApi.Database;
using KittensApi.Middlewares;
using KittensApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

const string settingsRoot = "Settings";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Override conventions
builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

// Configuration
var appSettings = new AppSettings();
var config = builder.Configuration.GetSection(settingsRoot);
config.Bind(appSettings);
builder.Services.AddSingleton(appSettings);

// Database
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(appSettings.Database.ConnectionString));

// Services
builder.Services.AddScoped<IImageProcessor, ImageProcessor>();
builder.Services.AddHttpClient<ICatsService, CatsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandler>();

app.MapControllers();

app.Run();