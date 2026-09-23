using Microsoft.EntityFrameworkCore;
using TaskBoad.Domain.Entidades;

namespace TaskBoad.Application.Abstracoes;

/// <summary>
/// Acesso ao banco visto pela Application. Os casos de uso dependem desta interface,
/// e a Infrastructure fornece a implementação (AppDbContext).
/// </summary>
public interface IAppDbContext
{
    DbSet<Perfil> Perfis { get; }
    DbSet<AreaDeTrabalho> AreasDeTrabalho { get; }
    DbSet<MembroAreaDeTrabalho> MembrosAreaDeTrabalho { get; }
    DbSet<Quadro> Quadros { get; }
    DbSet<Lista> Listas { get; }
    DbSet<Cartao> Cartoes { get; }
    DbSet<Etiqueta> Etiquetas { get; }
    DbSet<CartaoEtiqueta> CartaoEtiquetas { get; }
    DbSet<CartaoResponsavel> CartaoResponsaveis { get; }
    DbSet<Checklist> Checklists { get; }
    DbSet<ItemChecklist> ItensChecklist { get; }
    DbSet<Comentario> Comentarios { get; }
    DbSet<Atividade> Atividades { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
