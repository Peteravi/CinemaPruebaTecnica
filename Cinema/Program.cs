using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Repositories;
using Cinema.Infrastructure.Repositories.Interfaces;
using Cinema.Application.Services;
using Cinema.Application.Services.Interfaces;
using Cinema.Middlewares;
using Cinema.Application.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// =======================
// CONFIGURAR CONEXIÓN A POSTGRESQL
// =======================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// =======================
// INYECTAR REPOSITORIOS
// =======================
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
builder.Services.AddScoped<ICarteleraRepository, CarteleraRepository>();
builder.Services.AddScoped<IButacaRepository, ButacaRepository>();

// =======================
// INYECTAR SERVICIOS
// =======================
builder.Services.AddScoped<IReservaService, ReservaService>();

// =======================
// AUTOMAPPER
// =======================
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// =======================
// CONFIGURAR CORS (Permitir frontend en React)
// =======================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // puerto del frontend de Vite
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// =======================
// SWAGGER Y CONTROLADORES
// =======================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Cine Reservas API",
        Version = "v1",
        Description = "API RESTful para gestionar reservas de butacas en cartelera de cine"
    });
});

// =======================
// CONSTRUIR LA APP
// =======================
var app = builder.Build();

// =======================
// MIDDLEWARES
// =======================

app.UseMiddleware<CustomExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// HABILITAR CORS
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
