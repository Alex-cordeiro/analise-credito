using AnaliseCredito.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnaliseCredito.Data.Mappings;

public static class BaseMap
{
    public static void BaseProperties<T>(EntityTypeBuilder<T> builder) where T : BaseEntity
    {
        builder.HasKey(x => x.Id);
    }
}
