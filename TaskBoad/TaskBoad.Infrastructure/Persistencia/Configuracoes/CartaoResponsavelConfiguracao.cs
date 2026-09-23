using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoad.Domain.Entidades;

namespace TaskBoad.Infrastructure.Persistencia.Configuracoes;

public class CartaoResponsavelConfiguracao : IEntityTypeConfiguration<CartaoResponsavel>
{
    public void Configure(EntityTypeBuilder<CartaoResponsavel> builder)
    {
        builder.ToTable("cartao_responsaveis");
        builder.HasKey(cr => new { cr.CartaoId, cr.UsuarioId });

        builder.HasOne(cr => cr.Usuario).WithMany()
            .HasForeignKey(cr => cr.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        // Para a tela "cartões atribuídos a mim".
        builder.HasIndex(cr => cr.UsuarioId);
    }
}
