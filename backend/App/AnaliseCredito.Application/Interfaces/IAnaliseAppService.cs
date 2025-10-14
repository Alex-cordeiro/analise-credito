using AnaliseCredito.Application.Analises.Commands;
using AnaliseCredito.Application.Response;
using AnaliseCredito.Domain.Entities.Analises;

namespace AnaliseCredito.Application.Interfaces;

public interface IAnaliseAppService
{
    
    Task<ResponseResult<Analise>> CriateAnalise(AnaliseCreateCommand command);
}