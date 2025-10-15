using AnaliseCredito.Api;
using AnaliseCredito.Application;
using AnaliseCredito.Application.Configuration;
using AnaliseCredito.Data;
using AnaliseCredito.Domain;
using AnaliseCredito.Worker;

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
ConfigurationInfo.RabbitMqHost = config.GetSection("RabbiMQ:Host").Value ?? string.Empty;
ConfigurationInfo.RabbitMqPort = Convert.ToInt32(config.GetSection("RabbiMQ:Port").Value);
ConfigurationInfo.RabbitMqUserName = config.GetSection("RabbiMQ:UserName").Value ?? string.Empty;
ConfigurationInfo.RabbitMqPassword = config.GetSection("RabbiMQ:Password").Value ??  string.Empty;


// Add services to the container.
builder.Services.AddDataDependencies(ConfigurationInfo.ConnectionString);
builder.Services.MigrateData();

//Adiciona serviços do Dominio
builder.Services.AddDomainServices();

//Add AppServices
builder.Services.AddAppServices();

//Add Validators
builder.Services.AddValidators();


//Adiciona serviço RabbitMQ para publisher
builder.Services.AddRabbitIntegration(
    ConfigurationInfo.RabbitMqHost, 
    ConfigurationInfo.RabbitMqPort,  
ConfigurationInfo.RabbitMqUserName, 
    ConfigurationInfo.RabbitMqPassword);

//Adiciona background services
builder.Services.AddRabbitWorker(ConfigurationInfo.RabbitMqHost,
    ConfigurationInfo.RabbitMqPort,
    ConfigurationInfo.RabbitMqUserName,
    ConfigurationInfo.RabbitMqPassword);
builder.Services.AddHostedServices();

//Add MediatR
builder.Services.AddMediatRDependence();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (env == "Development")
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAny", policy =>
        {
            policy.AllowAnyOrigin();
            policy.WithOrigins("http://localhost")
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAny");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
