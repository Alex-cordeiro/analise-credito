using AnaliseCredito.Application.Response;
using AnaliseCredito.Domain.Entities.Analises;
using MediatR;

namespace AnaliseCredito.Application.Analises.Commands;

public class AnaliseCreateCommand : AnaliseCommand, IRequest<ResponseResult<Analise>> { }