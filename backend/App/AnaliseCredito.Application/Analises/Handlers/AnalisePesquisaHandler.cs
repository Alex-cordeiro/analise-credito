using AnaliseCredito.Application.Analises.Queries;
using AnaliseCredito.Application.Response;
using AnaliseCredito.Application.ViewModels;
using AnaliseCredito.Domain.Entities.Analises.Interfaces;
using AnaliseCredito.Domain.Entities.Clientes.Interfaces;
using AnaliseCredito.Domain.Enums;
using AnaliseCredito.Utils.Helpers;
using MediatR;

namespace AnaliseCredito.Application.Analises.Handlers;

public class AnalisePesquisaHandler : IRequestHandler<AnalisePesquisaQuery, ResponseResult<AnaliseViewModel>>
{
    private readonly IClienteService _clienteService;
    private readonly IAnaliseService _analiseService;

    public AnalisePesquisaHandler(IClienteService clienteService, IAnaliseService analiseService)
    {
        _clienteService  =  clienteService;
        _analiseService = analiseService;
    }
    
    public async Task<ResponseResult<AnaliseViewModel>> Handle(AnalisePesquisaQuery request, CancellationToken cancellationToken)
    {
        ResponseResult<AnaliseViewModel> result = new ResponseResult<AnaliseViewModel>();
        
        var cliente = _clienteService.GetAllAsync(true, x => x.Cpf == CPFHelper.RemovePontuacao(request.Cpf), ["Analises"]).FirstOrDefault();
        if (cliente == null)
            return  result.Fail<AnaliseViewModel>("Documento não existe");

        if (cliente.Analises == null)
            return result.Fail<AnaliseViewModel>("Refaça a analise");
        
        var ultimaAnalise = cliente.Analises.MaxBy(x => x.CreatedAt.Date);
        var analiseModel = new AnaliseViewModel()
        {
            AnaliseStatusDescricao = EnumHelper.GetDescription(ultimaAnalise.Status),
            LimiteLiberado = ultimaAnalise.LimiteCredito,
            Status = ultimaAnalise.Status
        };
        
        if (ultimaAnalise.Status == EAnaliseStatus.EmAnalise)
            return result.Ok("Pedido em analise, volte em alguns instantes...", analiseModel);
        

        return result.Ok("Analise Realizada", analiseModel);
    }
}