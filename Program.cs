using Microsoft.EntityFrameworkCore;
using PIM_SistemaDeChamados_API.Data;

var builder = WebApplication.CreateBuilder(args);

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Banco de Dados (Azure + Localhost:5018)
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ CORS liberado para qualquer origem (WPF precisa disso)
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

// ✅ Usa HTTP e ativa CORS
app.UseCors("DevAll");
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

// (Evita erro: "Failed to determine the http port for redirect")
app.Run("http://localhost:5018");
