using Festivos.API.Models;
using Festivos.Application.Services;
using Festivos.Domain.Repository;
using Festivos.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IFestivoRepository, FestivoRepository>();
builder.Services.AddScoped<FestivosService>();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FestivosContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



