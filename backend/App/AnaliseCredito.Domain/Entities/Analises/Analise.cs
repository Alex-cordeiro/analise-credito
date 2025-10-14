using AnaliseCredito.Domain.Entities.Base;
using AnaliseCredito.Domain.Entities.Clientes;
using AnaliseCredito.Domain.Enums;

namespace AnaliseCredito.Domain.Entities.Analises;

public class Analise : BaseEntity
{
    public Analise()
    {
        CreatedAt = DateTime.UtcNow;
        Status = EAnaliseStatus.EmAnalise;
    }

    public void AddCliente(Cliente cliente)
    {
        Cliente = cliente;
    }

    public void AddComentarioAnalise(string comentarioAnalise)
    {
        ComentarioAnalise = comentarioAnalise;
    }

    public void AddLimiteCredito(decimal limiteCredito)
    {
        LimiteCredito = limiteCredito;
    }
    public Guid ClienteId { get; private set; }

    public EAnaliseStatus Status { get; private set; }

    public decimal LimiteCredito { get; set; }

    public DateTime CreatedAt { get; private set; }

    public string ComentarioAnalise { get; private set; }

    public Cliente Cliente { get; private set; } = default;
}