using System.Text.Json;
using AnaliseCredito.Application.Analises.Commands;
using AnaliseCredito.Application.Response;
using AnaliseCredito.Application.Validators;
using MediatR;
using AnaliseCredito.Domain.Entities.Analises;
using AnaliseCredito.Domain.Entities.Analises.Interfaces;
using AnaliseCredito.Domain.Entities.Clientes;
using AnaliseCredito.Domain.Entities.Clientes.Interfaces;
using FluentValidation.Results;
using RabbitHole.Interfaces;

namespace AnaliseCredito.Application.Analises.Handlers;

public class AnaliseCreateCommandHandler : IRequestHandler<AnaliseCreateCommand, ResponseResult<Analise>>
{
    private readonly IAnaliseService _analiseService;
    private readonly IClienteService  _clienteService;
    
    private readonly IRabbitPublisher _rabbitPublisher;
    public AnaliseCreateCommandHandler(IAnaliseService analiseService, IClienteService clienteService, IRabbitPublisher rabbitPublisher)
    {
        _analiseService = analiseService;
        _clienteService = clienteService;
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
        
        var cliente = new Cliente(request.NomeCliente, request.Email, request.Telefone,  request.Cpf);
        
        var analise = new Analise();
        analise.AddCliente(cliente);
        
        await _rabbitPublisher.PublishAsync("analise", JsonSerializer.Serialize(analise));

        return ResponseResult.Ok<Analise>("Analise criada");
    }
}