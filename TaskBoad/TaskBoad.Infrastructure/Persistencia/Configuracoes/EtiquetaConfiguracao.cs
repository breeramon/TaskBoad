using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoad.Domain.Entidades;

namespace TaskBoad.Infrastructure.Persistencia.Configuracoes;

public class EtiquetaConfiguracao : IEntityTypeConfiguration<Etiqueta>
{
    public void Configure(EntityTypeBuilder<Etiqueta> builder)
    {
        builder.ToTable("etiquetas");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Nome)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(e => e.Cor)
            .HasMaxLength(7)
            .IsRequired();
    }
}
