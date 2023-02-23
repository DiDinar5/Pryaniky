using Microsoft.EntityFrameworkCore;
using Pryaniky.Models;

var builder = WebApplication.CreateBuilder(args);
// Добавление сервисов в контейнер.
builder.Services.AddControllers();
builder.Services.AddDbContext<PryanikyContext>(opt =>
    opt.UseInMemoryDatabase("PryanikyDB"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Настройка конвейера HTTP-запросов.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();