using AnaliseCredito.Application.Response;
using AnaliseCredito.Application.ViewModels;
using MediatR;

namespace AnaliseCredito.Application.Analises.Queries;

public class AnalisePesquisaQuery : IRequest<ResponseResult<AnaliseViewModel>>
{
    public string Cpf { get; set; }
    public AnalisePesquisaQuery(string cpf)
    {
        Cpf  = cpf;
    }
}