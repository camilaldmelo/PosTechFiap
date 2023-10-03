using Application.AutoMapper;
using Application.Interface;
using Application.UseCases;
using Domain.Interface.Repositories;
using Domain.Interface.Services;
using Domain.Services;
using Infra.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
// Add services to the container.

//UseCases
builder.Services.AddScoped<IPedidoUseCase, PedidoUseCase>();

//Domínio
builder.Services.AddScoped<IPedidoService, PedidoService>();

//Repositórios
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IProdutosPedidoRepository, ProdutosPedidoRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API.API",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Test",
            Email = "Test@dominio.com.br",
            Url = new Uri("https://miro.com/app/board/uXjVMqYSzbg=/?share_link_id=124875092732")

        }
    });

    //var xmlFile = "API.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
