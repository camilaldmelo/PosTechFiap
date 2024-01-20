using Application.AutoMapper;
using Application.Interface.Presenters;
using Application.Presenters;
using Domain.Interface.Repositories;
using Domain.Interface.Services;
using Domain.UseCases;
using Infra.DB;
using Infra.Gateways;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
// Add services to the container.

//UseCases
builder.Services.AddScoped<IPedidoPresenters, PedidoPresenters>();
builder.Services.AddScoped<IProdutoPresenters, ProdutoPresenters>();
builder.Services.AddScoped<ICategoriaPresenters, CategoriaPresenters>();
builder.Services.AddScoped<IAcompanhamentoPresenters, AcompanhamentoPresenters>();
builder.Services.AddScoped<IClienteUseCase, ClientePresenters>();
builder.Services.AddScoped<IPagamentoUseCase, PagamentoPresenters>();

//Domínio
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IAcompanhamentoService, AcompanhamentoService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IPagamentoService, PagamentoService>();

//Repositórios
builder.Services.AddScoped<RepositoryBase>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IPedidoRepository, PedidoGateways>();
builder.Services.AddScoped<IProdutosPedidoGateways, ProdutosPedidoGateways>();
builder.Services.AddScoped<IProdutoRepository, ProdutoGateways>();
builder.Services.AddScoped<ICategoriaGateways, CategoriaGateways>();
builder.Services.AddScoped<IAcompanhamentoRepository, AcompanhamentoGateways>();
builder.Services.AddScoped<IClienteGateways, ClienteRepository>();
builder.Services.AddScoped<IPagamentoRepository, PagamentoGateways>();


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



