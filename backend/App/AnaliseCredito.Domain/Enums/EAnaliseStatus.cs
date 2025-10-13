using System.ComponentModel;

namespace AnaliseCredito.Domain.Enums;

public enum EAnaliseStatus
{
    [Description("Em Análise")] EmAnalise = 1,
    [Description("Recusado")] Recusado = 2,
    [Description("Aprovado")] Aprovado = 3
}