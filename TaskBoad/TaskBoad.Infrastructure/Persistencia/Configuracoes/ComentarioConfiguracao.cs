using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoad.Domain.Entidades;

namespace TaskBoad.Infrastructure.Persistencia.Configuracoes;

public class ComentarioConfiguracao : IEntityTypeConfiguration<Comentario>
{
    public void Configure(EntityTypeBuilder<Comentario> builder)
    {
        builder.ToTable("comentarios");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Texto).HasMaxLength(5_000).IsRequired();

        builder.HasOne(c => c.Autor).WithMany()
            .HasForeignKey(c => c.AutorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Comentários de um cartão, em ordem de criação.
        builder.HasIndex(c => new { c.CartaoId, c.CriadoEm });
    }
}
