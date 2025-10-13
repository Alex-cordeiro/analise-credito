using AnaliseCredito.Domain.Entities.Clientes;
using MediatR;

namespace AnaliseCredito.Application.Analises.Commands;

public abstract class AnaliseCommand : IRequest<Cliente>
{
    
    public string NomeCliente { get; set; } = null!;
    public string Logradouro { get; set; } = null!;
    public string Bairro { get; set; } = null!;
    public int? Numero { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string Email { get; set; } = null!;
    public string Telefone { get; set; } = null!;
    public string Cpf { get; set; } = null!;
}