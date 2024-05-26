using FacturacionMagnetron.Infrastructure.Extensions;
using FacturacionMagnetron.Application.Extensions;
using FacturacionMagnetron.Api.Middleware;
using Microsoft.AspNetCore.HttpsPolicy;

var builder = WebApplication.CreateBuilder(args);

//se agrega para ver los logs
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//se inyecta la conexion a base de datos y los servicios
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddServices2();
builder.Services.AddTransient<ErrorHandlerMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
