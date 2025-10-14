using System.Text.Json;
using AnaliseCredito.Application.Analises.Commands;
using AnaliseCredito.Application.Response;
using AnaliseCredito.Application.Validators;
using AnaliseCredito.Domain.Entities.Analises;
using AnaliseCredito.Domain.Entities.Analises.Interfaces;
using AnaliseCredito.Domain.Entities.Clientes.Interfaces;
using FluentValidation.Results;
using MediatR;
using RabbitHole.Interfaces;

namespace AnaliseCredito.Application.Analises.Handlers;

public class AnaliseCreateCommandHandler : IRequestHandler<AnaliseCreateCommand, ResponseResult<Analise>>
{

    private readonly IRabbitPublisher _rabbitPublisher;
    public AnaliseCreateCommandHandler(IRabbitPublisher rabbitPublisher)
    {
        _rabbitPublisher = rabbitPublisher;
    }
    public async Task<ResponseResult<Analise>> Handle(AnaliseCreateCommand request, CancellationToken cancellationToken)
    {
        ResponseResult<Analise> response = new();
        AnaliseCreateCommandValidator validator =  new AnaliseCreateCommandValidator();
        ValidationResult validatorResult = validator.Validate(request);

        if (!validatorResult.IsValid)
        {
            var errorList = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            return ResponseResult.RequestError<Analise>("Ocorreram erros", errorList);
        }
        

        var serializerOptions = new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        var textCommand = JsonSerializer.Serialize(request, serializerOptions);
        
        await _rabbitPublisher.PublishAsync("analise", request);

        return ResponseResult.Ok<Analise>("Analise criada");
    }
}