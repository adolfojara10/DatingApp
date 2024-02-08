using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors();

//add controllers
builder.Services.AddControllers();

var app = builder.Build();

//this line is to allow cors. it means the browser acn show what it gets from the backend
//this happens due to the fact that the backend and frontend are hearing in different ports
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));
app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();


app.Run();
