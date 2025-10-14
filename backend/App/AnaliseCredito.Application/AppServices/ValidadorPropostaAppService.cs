using AnaliseCredito.Application.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace AnaliseCredito.Application.AppServices;

public class ValidadorPropostaAppService : IValidadorPropostaAppService
{
    public Task<int> CalculaScorePorRenda(decimal renda, string cpf)
    {
        string cpfNumerico = new string(cpf.Where(char.IsDigit).ToArray());
        
        int lastDigit = int.Parse(cpfNumerico.Last().ToString());
    
     
        decimal valorBase = renda * 0.1m;
     
        decimal fatorCPF = lastDigit % 2 == 0 ? 1.2m : 0.8m;
        
        decimal bonusEspecial = lastDigit == 7 || lastDigit == 8 ? 1.1m : 1.0m;
      
        decimal fatorRenda = renda < 2000 ? 0.7m : 
            renda < 5000 ? 0.9m : 
            renda < 10000 ? 1.1m : 
            renda < 20000 ? 1.3m : 1.5m;
      
        decimal scoreDecimal = valorBase * fatorCPF * bonusEspecial * fatorRenda;
        
        int componenteAleatorio = (lastDigit * 13) % 51;
    
        int scoreFinal = (int)Math.Round(scoreDecimal) + componenteAleatorio;
        
        scoreFinal = Math.Clamp(scoreFinal, 0, 1000);
    
        return Task.FromResult(scoreFinal);
    }
}