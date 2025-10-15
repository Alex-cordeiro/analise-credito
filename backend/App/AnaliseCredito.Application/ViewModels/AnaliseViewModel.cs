using AnaliseCredito.Domain.Enums;

namespace AnaliseCredito.Application.ViewModels;

public class AnaliseViewModel
{
    public string AnaliseStatusDescricao  { get; set; }
    public decimal LimiteLiberado { get; set; }
    public EAnaliseStatus Status { get; set; }
}