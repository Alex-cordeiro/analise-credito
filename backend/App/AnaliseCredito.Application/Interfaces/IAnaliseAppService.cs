using AnaliseCredito.Application.Analises.Commands;
using AnaliseCredito.Application.Analises.Queries;
using AnaliseCredito.Application.Response;
using AnaliseCredito.Application.ViewModels;
using AnaliseCredito.Domain.Entities.Analises;

namespace AnaliseCredito.Application.Interfaces;

public interface IAnaliseAppService
{
    
    Task<ResponseResult<Analise>> CriateAnalise(AnaliseCreateCommand command);
    Task<ResponseResult<AnaliseViewModel>> ConsultaAnalise(AnalisePesquisaQuery query);
}