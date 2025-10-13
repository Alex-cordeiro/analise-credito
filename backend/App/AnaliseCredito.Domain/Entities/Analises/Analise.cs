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
    public Guid ClienteId { get; set; }

    public EAnaliseStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Cliente Cliente { get; set; } = default;
}