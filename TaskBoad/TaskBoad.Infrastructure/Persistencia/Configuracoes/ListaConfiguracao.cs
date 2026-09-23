using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoad.Domain.Entidades;

namespace TaskBoad.Infrastructure.Persistencia.Configuracoes;

public class ListaConfiguracao : IEntityTypeConfiguration<Lista>
{
    public void Configure(EntityTypeBuilder<Lista> builder)
    {
        builder.ToTable("listas");
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Titulo).HasMaxLength(100).IsRequired();

        // Concorrência otimista: usa a coluna de sistema xmin do Postgres.
        // Se dois usuários alterarem a mesma lista ao mesmo tempo, o segundo recebe erro em vez de sobrescrever.
        builder.Property<uint>("Versao").HasColumnName("xmin").IsRowVersion();

        builder.HasMany(l => l.Cartoes).WithOne(c => c.Lista)
            .HasForeignKey(c => c.ListaId)
            .OnDelete(DeleteBehavior.Cascade);

        // A tela do quadro sempre busca as listas de um quadro em ordem.
        builder.HasIndex(l => new { l.QuadroId, l.Posicao });
    }
}
