using Microsoft.EntityFrameworkCore;
using ProjetoSara.Data;
using ProjetoSara.Services; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Registrar o DbContext com Oracle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar todos os services para injeção de dependência
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TipoUsuarioService>();
builder.Services.AddScoped<TipoSensorService>();
builder.Services.AddScoped<AlertaService>();
builder.Services.AddScoped<LeituraSensorService>();
builder.Services.AddScoped<LocalizacaoService>();
builder.Services.AddScoped<NivelAlertaService>();
builder.Services.AddScoped<NotificacaoService>();
builder.Services.AddScoped<SensorService>();
builder.Services.AddScoped<StatusNotificacaoService>();

// Adicionar controllers com views para Razor e TagHelpers
builder.Services.AddControllersWithViews();

// Registrar o Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();
