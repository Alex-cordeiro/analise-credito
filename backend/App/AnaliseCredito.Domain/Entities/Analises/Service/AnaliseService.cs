using AnaliseCredito.Domain.Entities.Analises.Interfaces;
using AnaliseCredito.Domain.Entities.Base;

namespace AnaliseCredito.Domain.Entities.Analises.Service;

public class AnaliseService : BaseService<Analise>, IAnaliseService
{
    private readonly IBaseDomainRepository<Analise> _repository;
    public AnaliseService(IBaseDomainRepository<Analise> baseRepository) : base(baseRepository)
    {
        _repository = baseRepository;
    }
}