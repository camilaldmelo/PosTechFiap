using Application.Interface.Presenters;
using Application.Interface.UseCases;
using Application.Presenters;
using Application.Presenters.AutoMapper;
using Application.UseCases;
using Domain.Interface.Gateways;
using Infra.DB;
using Infra.Gateways;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
// Add services to the container.

//Presenters
builder.Services.AddScoped<IPedidoPresenter, PedidoPresenter>();
builder.Services.AddScoped<IProdutoPresenter, ProdutoPresenter>();
builder.Services.AddScoped<ICategoriaPresenter, CategoriaPresenter>();
builder.Services.AddScoped<IAcompanhamentoPresenter, AcompanhamentoPresenter>();
builder.Services.AddScoped<IClientePresenter, ClientePresenter>();

//UseCases
builder.Services.AddScoped<IPedidoUseCase, PedidoUseCase>();
builder.Services.AddScoped<IProdutoUseCase, ProdutoUseCase>();
builder.Services.AddScoped<ICategoriaUseCase, CategoriaUseCase>();
builder.Services.AddScoped<IAcompanhamentoUseCase, AcompanhamentoUseCase>();
builder.Services.AddScoped<IClienteUseCase, ClienteUseCase>();
builder.Services.AddScoped<IPagamentoUseCase, PagamentoUseCase>();

//Repositórios
builder.Services.AddScoped<RepositoryDB>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IPedidoGateway, PedidoGateway>();
builder.Services.AddScoped<IProdutosPedidoGateway, ProdutosPedidoGateway>();
builder.Services.AddScoped<IProdutoGateway, ProdutoGateway>();
builder.Services.AddScoped<ICategoriaGateway, CategoriaGateway>();
builder.Services.AddScoped<IAcompanhamentoGateway, AcompanhamentoGateway>();
builder.Services.AddScoped<IClienteGateway, ClienteGateway>();
builder.Services.AddScoped<iPagamentoGateway, PagamentoGateway>();


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



