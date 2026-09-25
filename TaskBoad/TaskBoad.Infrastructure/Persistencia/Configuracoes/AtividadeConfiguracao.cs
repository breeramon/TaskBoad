using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoad.Domain.Entidades;

namespace TaskBoad.Infrastructure.Persistencia.Configuracoes;

public class AtividadeConfiguracao : IEntityTypeConfiguration<Atividade>
{
    public void Configure(EntityTypeBuilder<Atividade> builder)
    {
        builder.ToTable("atividades");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Tipo)
            .HasConversion<string>()
            .HasMaxLength(40);

        // JSON com os detalhes do evento, guardado no tipo jsonb do Postgres.
        builder.Property(a => a.Dados)
            .HasColumnType("jsonb");

        builder.HasOne<Quadro>().WithMany()
            .HasForeignKey(a => a.QuadroId)
            .OnDelete(DeleteBehavior.Cascade);

        // Se o cartão for apagado, o histórico continua (CartaoId vira null).
        builder.HasOne<Cartao>().WithMany()
            .HasForeignKey(a => a.CartaoId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.Usuario).WithMany()
            .HasForeignKey(a => a.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        // Histórico do quadro, do mais recente para o mais antigo.
        builder.HasIndex(a => new { a.QuadroId, a.CriadoEm }).IsDescending(false, true);
    }
}
