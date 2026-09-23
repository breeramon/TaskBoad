using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoad.Domain.Entidades;

namespace TaskBoad.Infrastructure.Persistencia.Configuracoes;

public class CartaoEtiquetaConfiguracao : IEntityTypeConfiguration<CartaoEtiqueta>
{
    public void Configure(EntityTypeBuilder<CartaoEtiqueta> builder)
    {
        builder.ToTable("cartao_etiquetas");
        builder.HasKey(ce => new { ce.CartaoId, ce.EtiquetaId });

        // Apagar uma etiqueta do quadro tira ela de todos os cartões.
        builder.HasOne(ce => ce.Etiqueta).WithMany()
            .HasForeignKey(ce => ce.EtiquetaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
