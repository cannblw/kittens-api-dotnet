using System.Text;
using KittensApi.Adapters;
using KittensApi.Config;
using KittensApi.Database;
using KittensApi.Domain;
using KittensApi.Middlewares;
using KittensApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

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

// Authentication
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var key = Encoding.ASCII.GetBytes(appSettings.Authentication.JwtSecret);

        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            RequireExpirationTime = false
        };
    });

builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
    }).AddEntityFrameworkStores<AppDbContext>();


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