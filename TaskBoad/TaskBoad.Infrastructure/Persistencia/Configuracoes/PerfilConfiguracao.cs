using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoad.Domain.Entidades;

namespace TaskBoad.Infrastructure.Persistencia.Configuracoes;

public class PerfilConfiguracao : IEntityTypeConfiguration<Perfil>
{
    public void Configure(EntityTypeBuilder<Perfil> builder)
    {
        builder.ToTable("perfis");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Email).HasMaxLength(256).IsRequired();
        builder.Property(p => p.AvatarUrl).HasMaxLength(500);
        builder.Property(p => p.Idioma).HasMaxLength(10).IsRequired();

        builder.HasIndex(p => p.Email).IsUnique();
    }
}
