using AnaliseCredito.Domain.Entities.Base;
using AnaliseCredito.Domain.Entities.Clientes.Interfaces;

namespace AnaliseCredito.Domain.Entities.Clientes.Service;

public class ClienteService : BaseService<Cliente>, IClienteService
{
    private IBaseDomainRepository<Cliente> _repository;
    public ClienteService(IBaseDomainRepository<Cliente> baseRepository) : base(baseRepository)
    {
        _repository = baseRepository;
    }
}