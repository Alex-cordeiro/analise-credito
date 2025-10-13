namespace AnaliseCredito.Application.Commands;

public class CriarAnaliseCommand
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string NomeCliente { get; set; } = null!;
    public string Logradouro { get; set; } = null!;
    public string Bairro { get; set; } = null!;
    public int Numero { get; set; }
    public string Email { get; set; } = null!;
    public string Telefone { get; set; } = null!;
    public string Cpf { get; set; } = null!;
}