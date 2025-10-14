namespace AnaliseCredito.Application.Interfaces;

public interface IValidadorPropostaAppService
{
    public Task<int> CalculaScorePorRenda(decimal renda, string cpf);
}