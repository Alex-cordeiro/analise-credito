using AnaliseCredito.Domain.Entities.Base;
using AnaliseCredito.Domain.Entities.Clientes;
using AnaliseCredito.Domain.Enums;

namespace AnaliseCredito.Domain.Entities.Analises;

public class Analise : BaseEntity
{
    public Guid ClienteId { get; set; }

    public EAnaliseStatus Status { get; private set; }

    public Cliente Cliente { get; set; } = new Cliente();
}