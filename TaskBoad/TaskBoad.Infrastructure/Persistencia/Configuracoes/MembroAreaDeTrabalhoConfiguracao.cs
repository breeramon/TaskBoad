using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoad.Domain.Entidades;

namespace TaskBoad.Infrastructure.Persistencia.Configuracoes;

public class MembroAreaDeTrabalhoConfiguracao : IEntityTypeConfiguration<MembroAreaDeTrabalho>
{
    public void Configure(EntityTypeBuilder<MembroAreaDeTrabalho> builder)
    {
        builder.ToTable("membros_area_de_trabalho");
        builder.HasKey(m => new { m.AreaDeTrabalhoId, m.UsuarioId });

        // Enum salvo como texto ("Dono", "Membro"...), mais legível no banco.
        builder.Property(m => m.Papel).HasConversion<string>().HasMaxLength(20);

        builder.HasOne(m => m.Usuario).WithMany()
            .HasForeignKey(m => m.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        // Para listar rápido as áreas de um usuário.
        builder.HasIndex(m => m.UsuarioId);
    }
}
