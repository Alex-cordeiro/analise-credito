using AnaliseCredito.Application.Analises.Commands;
using AnaliseCredito.Application.Analises.Queries;
using AnaliseCredito.Application.Interfaces;
using AnaliseCredito.Application.Response;
using AnaliseCredito.Application.ViewModels;
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

    public async Task<ResponseResult<Analise>> CriateAnalise(AnaliseCreateCommand command) => await _mediator.Send(command);

    public Task<ResponseResult<AnaliseViewModel>> ConsultaAnalise(AnalisePesquisaQuery query) => _mediator.Send(query);
}