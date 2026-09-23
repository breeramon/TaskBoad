using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoad.Domain.Entidades;

namespace TaskBoad.Infrastructure.Persistencia.Configuracoes;

public class ItemChecklistConfiguracao : IEntityTypeConfiguration<ItemChecklist>
{
    public void Configure(EntityTypeBuilder<ItemChecklist> builder)
    {
        builder.ToTable("itens_checklist");
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Texto).HasMaxLength(500).IsRequired();
    }
}
