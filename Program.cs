using Microsoft.EntityFrameworkCore;
using Pryaniky.Models;

var builder = WebApplication.CreateBuilder(args);
// Добавление сервисов в контейнер.
builder.Services.AddControllers();
builder.Services.AddDbContext<PryanikyContext>(opt =>
    opt.UseSqlServer("Server=.\\SQLEXPRESS;Database=Pryanyk;Trusted_Connection=true;encrypt=false;")) ;

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