using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoad.Domain.Entidades;

namespace TaskBoad.Infrastructure.Persistencia.Configuracoes;

public class ChecklistConfiguracao : IEntityTypeConfiguration<Checklist>
{
    public void Configure(EntityTypeBuilder<Checklist> builder)
    {
        builder.ToTable("checklists");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Titulo).HasMaxLength(100).IsRequired();

        builder.Ignore(c => c.TotalDeItens);
        builder.Ignore(c => c.ItensConcluidos);

        builder.HasMany(c => c.Itens).WithOne()
            .HasForeignKey(i => i.ChecklistId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
