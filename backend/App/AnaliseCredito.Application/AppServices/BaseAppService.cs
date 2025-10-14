using AnaliseCredito.Application.Response;
using AnaliseCredito.Data.UOW;

namespace AnaliseCredito.Application.AppServices;

public class BaseAppService
{
    public readonly IUnitOfWork _unitOfWork;
    public ResponseResult ResponseResult { get; private set; }
    
    public BaseAppService(IUnitOfWork unitOfWork)
    {
        ResponseResult = new();
        _unitOfWork = unitOfWork;
    }

    public async Task CommitAsync()
    {
        await _unitOfWork.CommitAsync();
    }

    public void Commit()
    {
        _unitOfWork.Commit();
    }
}
