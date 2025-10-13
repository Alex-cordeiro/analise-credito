using AnaliseCredito.Domain.Entities.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnaliseCredito.Data.Mappings;

public class ClienteMap : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");
        BaseMap.BaseProperties(builder);

        builder.Property(x => x.NomeCliente).IsRequired(true).HasMaxLength(250);
        builder.Property(x => x.Cpf).IsRequired(true).HasMaxLength(11);
        builder.Property(x => x.Bairro).HasMaxLength(250);
        builder.Property(x => x.Email).IsRequired(true).HasMaxLength(150);
        builder.Property(x => x.Logradouro).IsRequired(true).HasMaxLength(250);
        builder.Property(x => x.Telefone).IsRequired(true).HasMaxLength(250);
    }
}