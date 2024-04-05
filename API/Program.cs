using API.Data;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adiciona servi�os ao container.
builder.Services.AddDbContext<AppDbContext>(Options =>
{
    Options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Liberando oo CORS para receber de solicita��es qualquer origem
#region [Cors]
builder.Services.AddCors();
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region [Cors]
app.UseCors(c =>
{
    c.AllowAnyHeader();//Qualquer cabe�alho
    c.AllowAnyMethod();//Qualquer m�todo HTTP
    c.AllowAnyOrigin();//Qualquer origem
});
#endregion

app.UseAuthorization();

app.MapControllers();

app.Run();
