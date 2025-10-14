using AnaliseCredito.Domain.Entities.Analises;
using AnaliseCredito.Domain.Entities.Base;

namespace AnaliseCredito.Domain.Entities.Clientes;

public class Cliente : BaseEntity
{

    public Cliente(string nomeCliente, string email, string telefone, string cpf, decimal renda)
    {
        NomeCliente = nomeCliente;
        Email = email;
        Telefone = telefone;
        Cpf = cpf;
        Renda = renda;
    }

    public void AddEndereco(string logradouro, int numero, string bairro, string cidade, string estado, decimal renda)
    {
        Logradouro = logradouro;
        Numero = numero;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
        Renda = renda;
    }
    
    public string NomeCliente { get; private set; } = null!;
    public string Logradouro { get; private set; } = null!;
    public int Numero { get; private set; }
    public string Cidade { get; private set; }
    public string Estado { get; private set; }
    public string Bairro { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Telefone { get; private set; } = null!;
    public string Cpf { get; private set; } = null!;
    public decimal Renda { get; private set; }
    public IList<Analise> Analises { get; set; } = new List<Analise>();
}