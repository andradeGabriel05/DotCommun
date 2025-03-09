using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Application.Services;
using Domain.Interfaces.Repositories;
using Infra.Data;
using Infra.Hubs;
using Infra.Hubs.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

//database
builder.Services.AddDbContext<UserDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserDbContext") ?? 
                         throw new InvalidOperationException("Connection string is missing")));

//jwt
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    }; 
    options.RequireHttpsMetadata = false;
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            // Se a requisição for para o SignalR, captura o token da query string
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chatHub"))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

//cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("front", builder => 
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddSingleton<IUserIdProvider, UsernameUserIdProvider>();
builder.Services.AddSignalR();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseCors("front");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/chatHub");

app.MapControllers();



app.Run();