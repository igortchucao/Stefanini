using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Testestefanini.Application;
using Testestefanini.Application.Interfaces;
using Testestefanini.Authenticator;
using Testestefanini.Infrastructure.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var password = await SecretManager.GetSecret();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer($"Server=databaseteste.czoecsskuz3v.sa-east-1.rds.amazonaws.com,1433;Database=stefanini;User ID=admin;Password={password};Encrypt=True;TrustServerCertificate=True;"));

builder.Services.AddScoped<IPedidoApplication, PedidoApplication>();
builder.Services.AddScoped<IProdutosApplication, ProdutosApplication>();
builder.Services.AddScoped<IPedidoApplication, PedidoApplication>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
