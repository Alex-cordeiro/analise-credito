using AnaliseCredito.Api;
using AnaliseCredito.Application;
using AnaliseCredito.Application.Configuration;
using AnaliseCredito.Data;
using AnaliseCredito.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();
    config.SetMinimumLevel(LogLevel.Debug);
});

//Configuração de ambiente e conexões
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
IConfiguration config = (IConfiguration)builder.Configuration.AddJsonFile($"appsettings.{env}.json");
ConfigurationInfo.ConnectionString = config.GetConnectionString("Default") ?? string.Empty;
ConfigurationInfo.RabbitMqHost = config.GetSection("RabbitMq:Host").Value ?? string.Empty;
ConfigurationInfo.RabbitMqPort = Convert.ToInt32(config.GetSection("RabbitMq:Port").Value);
ConfigurationInfo.RabbitMqUserName = config.GetSection("RabbitMq:UserName").Value ?? string.Empty;
ConfigurationInfo.RabbitMqPassword = config.GetSection("RabbitMq:Password").Value ??  string.Empty;


// Add services to the container.
builder.Services.AddDataDependencies(ConfigurationInfo.ConnectionString);
//Adiciona serviços do Dominio
builder.Services.AddDomainServices();
//Add Validators
builder.Services.AddValidators();

//Adiciona serviço RabbitMQ
builder.Services.AddRabbitIntegration(
    ConfigurationInfo.RabbitMqHost, 
    ConfigurationInfo.RabbitMqPort,  
ConfigurationInfo.RabbitMqUserName, 
    ConfigurationInfo.RabbitMqPassword);


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
