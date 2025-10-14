using AnaliseCredito.Application.Analises.Commands;
using AnaliseCredito.Application.Interfaces;
using AnaliseCredito.Application.Response;
using AnaliseCredito.Data.UOW;
using AnaliseCredito.Domain.Entities.Analises;
using MediatR;

namespace AnaliseCredito.Application.AppServices;

public class AnaliseAppService : BaseAppService, IAnaliseAppService
{
    private readonly IMediator _mediator;
    public AnaliseAppService(IUnitOfWork unitOfWork, IMediator mediator) : base(unitOfWork)
    {
        _mediator = mediator;
    }

    public async Task<ResponseResult<Analise>> CriateAnalise(AnaliseCreateCommand command)
    {
        var result = await _mediator.Send(command);
    
        // Debug info
        Console.WriteLine($"Tipo do result: {result?.GetType()}");
        Console.WriteLine($"Tipo genérico esperado: {typeof(ResponseResult<Analise>)}");
        Console.WriteLine($"São do mesmo tipo? {result?.GetType() == typeof(ResponseResult<Analise>)}");
        return (ResponseResult<Analise>)result;
    }
}