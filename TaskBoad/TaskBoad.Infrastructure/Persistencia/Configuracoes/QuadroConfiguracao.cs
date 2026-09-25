using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoad.Domain.Entidades;

namespace TaskBoad.Infrastructure.Persistencia.Configuracoes;

public class QuadroConfiguracao : IEntityTypeConfiguration<Quadro>
{
    public void Configure(EntityTypeBuilder<Quadro> builder)
    {
        builder.ToTable("quadros");
        builder.HasKey(q => q.Id);

        builder.Property(q => q.Titulo)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(q => q.CorDeFundo)
            .HasMaxLength(7);

        builder.HasOne<Perfil>().WithMany()
            .HasForeignKey(q => q.CriadoPorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(q => q.Listas).WithOne(l => l.Quadro)
            .HasForeignKey(l => l.QuadroId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(q => q.Etiquetas).WithOne()
            .HasForeignKey(e => e.QuadroId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
