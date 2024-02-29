using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAplicationServices(builder.Configuration); //ApplicationServiceExtensions.cs

builder.Services.AddIdentityServices(builder.Configuration); //IdentityServiceExtensions.cs


//add controllers
builder.Services.AddControllers();

var app = builder.Build();

//añadir middleware
app.UseMiddleware<ExceptionMiddleware>();


//this line is to allow cors. it means the browser acn show what it gets from the backend
//this happens due to the fact that the backend and frontend are hearing in different ports
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200", "http://localhost:4200"));
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

//to create the database with the seed data. the db should be dropped before running the app with this
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
}
catch (Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "an error has occured");
}

app.Run();
