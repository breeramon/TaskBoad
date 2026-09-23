using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoad.Domain.Entidades;

namespace TaskBoad.Infrastructure.Persistencia.Configuracoes;

public class CartaoConfiguracao : IEntityTypeConfiguration<Cartao>
{
    public void Configure(EntityTypeBuilder<Cartao> builder)
    {
        builder.ToTable("cartoes");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Titulo).HasMaxLength(Cartao.TamanhoMaximoTitulo).IsRequired();
        builder.Property(c => c.Descricao).HasMaxLength(Cartao.TamanhoMaximoDescricao);
        builder.Property(c => c.CorDaCapa).HasMaxLength(7);

        // Propriedade calculada no C#: não vira coluna.
        builder.Ignore(c => c.Concluido);

        builder.Property<uint>("Versao").HasColumnName("xmin").IsRowVersion();

        builder.HasOne<Perfil>().WithMany()
            .HasForeignKey(c => c.CriadoPorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Etiquetas).WithOne()
            .HasForeignKey(ce => ce.CartaoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Responsaveis).WithOne()
            .HasForeignKey(cr => cr.CartaoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Checklists).WithOne()
            .HasForeignKey(ch => ch.CartaoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Comentarios).WithOne()
            .HasForeignKey(co => co.CartaoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(c => new { c.ListaId, c.Posicao });
    }
}
