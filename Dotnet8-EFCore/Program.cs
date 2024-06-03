using Dotnet8_EFCore.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LocationContext>(opt => opt.UseSqlServer("server=DESKTOP-F22LNIE;database=Location;Trusted_Connection=True;TrustServerCertificate=True"));
builder.Services.AddScoped<ILocationDb, LocationDb>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
