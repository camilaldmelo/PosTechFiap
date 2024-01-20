using Application.AutoMapper;
using Application.Interface.Presenters;
using Application.Interface.UseCases;
using Application.Presenters;
using Application.UseCases;
using Domain.Interface.Gateways;
using Domain.Interface.Repositories;
using Infra.DB;
using Infra.Gateways;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
// Add services to the container.

//Presenters
builder.Services.AddScoped<IPedidoPresenters, PedidoPresenters>();
builder.Services.AddScoped<IProdutoPresenters, ProdutoPresenters>();
builder.Services.AddScoped<ICategoriaPresenters, CategoriaPresenters>();
builder.Services.AddScoped<IAcompanhamentoPresenters, AcompanhamentoPresenters>();
builder.Services.AddScoped<IClientePresenters, ClientePresenters>();
builder.Services.AddScoped<IPagamentoPresenters, PagamentoPresenters>();

//UseCases
builder.Services.AddScoped<IPedidoUseCases, PedidoUseCases>();
builder.Services.AddScoped<IProdutoUseCases, ProdutoUseCases>();
builder.Services.AddScoped<ICategoriaUseCases, CategoriaUseCases>();
builder.Services.AddScoped<IAcompanhamentoUseCases, AcompanhamentoUseCases>();
builder.Services.AddScoped<IClienteUseCases, ClienteUseCases>();
builder.Services.AddScoped<IPagamentoUseCases, PagamentoUseCases>();

//Repositórios
builder.Services.AddScoped<RepositoryDB>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IPedidoGateways, PedidoGateways>();
builder.Services.AddScoped<IProdutosPedidoGateways, ProdutosPedidoGateways>();
builder.Services.AddScoped<IProdutoGateways, ProdutoGateways>();
builder.Services.AddScoped<ICategoriaGateways, CategoriaGateways>();
builder.Services.AddScoped<IAcompanhamentoGateways, AcompanhamentoGateways>();
builder.Services.AddScoped<IClienteGateways, ClienteGateways>();
builder.Services.AddScoped<IPagamentoGateways, PagamentoGateways>();


//HealthCheck
builder.Services.AddHealthChecks();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NuGET Burger",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Miro",
            Url = new Uri("https://miro.com/app/board/uXjVMqYSzbg=/?share_link_id=124875092732")

        }
    });

    var xmlFile = "API.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseReDoc(c =>
{
    c.DocumentTitle = "REDOC API Documentation";
    c.SpecUrl = "/swagger/v1/swagger.json";
});

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions()
{
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
    },
});

app.Run();



