using AnaliseCredito.Application.Response;
using AnaliseCredito.Domain.Entities.Analises;
using MediatR;

namespace AnaliseCredito.Application.Analises.Commands;

public class AnaliseProcessamentoCommand : AnaliseCommand, IRequest<ResponseResult<Analise>>{}
