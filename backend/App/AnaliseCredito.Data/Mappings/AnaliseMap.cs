using AnaliseCredito.Domain.Entities.Analises;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnaliseCredito.Data.Mappings;

public class AnaliseMap : IEntityTypeConfiguration<Analise>
{
    public void Configure(EntityTypeBuilder<Analise> builder)
    {
        builder.ToTable("Analises");
        BaseMap.BaseProperties(builder);
        builder.Property(x => x.Status).IsRequired();
        
        builder.HasOne(x => x.Cliente)
               .WithMany(x => x.Analises)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);
    }
}