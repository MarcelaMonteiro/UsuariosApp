using UsuariosApp.Domain.Interfaces.Messages;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Domain.Interfaces.Services;
using UsuariosApp.Domain.Services;
using UsuariosApp.Infra.Data.Repositories;
using UsuariosApp.Infra.Messages.Consumers;
using UsuariosApp.Infra.Messages.Producers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configura��o para os endpoints da API ficarem com o texto em caixa baixa
builder.Services.AddRouting(map => { map.LowercaseUrls = true; });

// Configurando a inje��o de depend�ncia de cada interface do projeto
// Associa��o de cada interface com a classe que a implementa

builder.Services.AddTransient<IUsuarioDomainService, UsuarioDomainService>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<IMessageProducer, MessageProducer>();


//registrar a classe consumer da mensageria
builder.Services.AddHostedService<MessageConsumer>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

//tornando a classe Program p�blica (dar visibilidade
//para que outros projetos possam acessar esta classe)
public partial class Program { }