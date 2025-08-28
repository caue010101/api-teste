using FluentValidation.AspNetCore;
using StudioIncantare.Dtos;
using System.Text;
using StudioIncantare.Repositories;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Configuração do JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();

// Controllers + FluentValidation
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateContactDto>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dapper context
builder.Services.AddSingleton<StudioIncantare.Infrastructure.DapperContext>();

// Repositórios
builder.Services.AddScoped<StudioIncantare.Repositories.IContactRepository, StudioIncantare.Repositories.ContactRepository>();

builder.Services.AddSingleton<StudioIncantare.Services.JwtService>();

builder.Services.AddScoped<StudioIncantare.Repositories.IUserRepository, StudioIncantare.Repositories.UserRepository>();

builder.Services.AddScoped<StudioIncantare.Repositories.ITeamMemberRepository, StudioIncantare.Repositories.TeamMemberRepository>();
var app = builder.Build();

// Swagger apenas em dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware de autenticação/authorization
app.UseAuthentication();
app.UseAuthorization();

// Mapear controllers
app.MapControllers();

app.Run();
