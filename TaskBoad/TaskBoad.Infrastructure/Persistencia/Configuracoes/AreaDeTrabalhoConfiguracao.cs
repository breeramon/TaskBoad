using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoad.Domain.Entidades;

namespace TaskBoad.Infrastructure.Persistencia.Configuracoes;

public class AreaDeTrabalhoConfiguracao : IEntityTypeConfiguration<AreaDeTrabalho>
{
    public void Configure(EntityTypeBuilder<AreaDeTrabalho> builder)
    {
        builder.ToTable("areas_de_trabalho");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.Descricao)
            .HasMaxLength(500);

        // Dono: não deixa apagar um perfil que ainda é dono de uma área.
        builder.HasOne<Perfil>().WithMany()
            .HasForeignKey(a => a.DonoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Membros e quadros são apagados junto com a área.
        builder.HasMany(a => a.Membros).WithOne()
            .HasForeignKey(m => m.AreaDeTrabalhoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.Quadros).WithOne(q => q.AreaDeTrabalho)
            .HasForeignKey(q => q.AreaDeTrabalhoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
