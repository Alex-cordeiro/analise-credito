using AnaliseCredito.Application.Analises.Commands;
using AnaliseCredito.Application.Interfaces;
using AnaliseCredito.Application.Response;
using AnaliseCredito.Data.UOW;
using AnaliseCredito.Domain.Entities.Analises;
using AnaliseCredito.Domain.Entities.Analises.Interfaces;
using AnaliseCredito.Domain.Entities.Clientes;
using AnaliseCredito.Domain.Enums;
using AnaliseCredito.Utils.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AnaliseCredito.Application.Analises.Handlers;

public class AnaliseProcessamentoCommandHandler : IRequestHandler<AnaliseProcessamentoCommand, ResponseResult<Analise>>
{
    private readonly IAnaliseService _analiseService;
    private IValidadorPropostaAppService _validadorPropostaAppService;
    private ILogger<AnaliseProcessamentoCommandHandler> _logger;


    public AnaliseProcessamentoCommandHandler(IAnaliseService analiseService,
        IValidadorPropostaAppService validadorPropostaAppService,
        ILogger<AnaliseProcessamentoCommandHandler> logger)
    {
        _analiseService = analiseService;
        _validadorPropostaAppService = validadorPropostaAppService;
        _logger = logger;
    }

    public async Task<ResponseResult<Analise>> Handle(AnaliseProcessamentoCommand request, CancellationToken cancellationToken)
    {
        Analise analise = null;

        try
        {
            if (request == null)
            {
                _logger.LogError("Request não pode ser nulo");
                throw new ArgumentNullException(nameof(request));
            }
            
            if (string.IsNullOrWhiteSpace(request.Cpf))
            {
                _logger.LogError("CPF não pode ser vazio ou nulo");
                throw new ArgumentException("CPF é obrigatório", nameof(request.Cpf));
            }

            _logger.LogInformation("Iniciando processamento de análise para CPF: {Cpf}", request.Cpf.MascararCpf());
            
            int resultScore;
            try
            {
                resultScore = await _validadorPropostaAppService.CalculaScorePorRenda(request.Renda, request.Cpf);
                _logger.LogDebug("Score calculado: {Score} para CPF: {Cpf}", resultScore, request.Cpf.MascararCpf());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao calcular score para CPF: {Cpf}. Renda: {Renda}",
                    request.Cpf.MascararCpf(), request.Renda);
                throw new ApplicationException("Falha no cálculo do score", ex);
            }
            
                var cliente = new Cliente(request.NomeCliente, request.Email, request.Telefone, request.Cpf,
                    request.Renda);
                
                cliente.AddEndereco(request.Logradouro, request.Numero, request.Bairro, request.Cidade, request.Estado);
                analise = new Analise();
                analise.AddCliente(cliente);
                
                switch (resultScore)
                {
                    case (> 700):
                        analise.AddComentarioAnalise("Score alto - será adicionado um limite premium");
                        analise.AddLimiteCredito(800.00m);
                        analise.AtualizaStatus(EAnaliseStatus.Aprovado);
                        _logger.LogInformation("Limite premium concedido para score: {Score}", resultScore);
                        break;
                    case (>= 501):
                        analise.AddComentarioAnalise("Score mínimo requerido - será adicionado um limite base");
                        analise.AddLimiteCredito(300.00m);
                        analise.AtualizaStatus(EAnaliseStatus.Aprovado);
                        _logger.LogInformation("Limite base concedido para score: {Score}", resultScore);
                        break;
                    default:
                        analise.AddComentarioAnalise("Score muito abaixo - análise necessária");
                        analise.AddLimiteCredito(0m);
                        analise.AtualizaStatus(EAnaliseStatus.Recusado);
                        _logger.LogWarning("Score abaixo do mínimo: {Score}. Limite negado.", resultScore);
                        break;
                }
                
            try
            {
          
                _logger.LogDebug("Transação iniciada para análise ID: {AnaliseId}", analise.Id);

                await _analiseService.Create(analise);
                

                _logger.LogInformation("Análise processada com sucesso ID: {AnaliseId}. Score: {Score}",
                    analise.Id, resultScore);

                return ResponseResult.Ok<Analise>("Mensagem processada com sucesso");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante persistência da análise ID: {AnaliseId}", analise?.Id);
                throw new ApplicationException("Falha na persistência da análise", ex);
            }
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Erro de validação no processamento da análise");
            throw;
        }
        catch (ApplicationException ex)
        {
 
            _logger.LogError(ex, "Erro de aplicação no processamento da análise");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Erro inesperado no processamento da análise para CPF: {Cpf}",
                request?.Cpf.MascararCpf() ?? "N/A");
            throw new ApplicationException("Erro inesperado no processamento da análise", ex);
        }
    }
}