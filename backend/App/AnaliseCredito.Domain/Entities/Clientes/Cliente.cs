using AnaliseCredito.Domain.Entities.Analises;
using AnaliseCredito.Domain.Entities.Base;

namespace AnaliseCredito.Domain.Entities.Clientes;

public class Cliente : BaseEntity
{
    public string NomeCliente { get; private set; } = null!;
    public string Logradouro { get; private set; } = null!;
    public string Bairro { get; private set; } = null!;
    public int Numero { get; private set; }
    public string Email { get; private set; } = null!;
    public string Telefone { get; private set; } = null!;
    public string Cpf { get; private set; } = null!;
    
    public IList<Analise> Analises { get; set; } = new List<Analise>();
}