using System.Text;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAplicationServices(builder.Configuration); //ApplicationServiceExtensions.cs

builder.Services.AddIdentityServices(builder.Configuration); //IdentityServiceExtensions.cs







//add controllers
builder.Services.AddControllers();

var app = builder.Build();


//this line is to allow cors. it means the browser acn show what it gets from the backend
//this happens due to the fact that the backend and frontend are hearing in different ports
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200", "http://localhost:4200"));
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();
