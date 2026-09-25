using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TaskBoad.Domain.Comum;

namespace TaskBoad.Infrastructure.Persistencia.Interceptadores;

/// Roda antes de cada SaveChanges e preenche AtualizadoEm de toda entidade alterada.
/// Assim nenhum caso de uso precisa lembrar de fazer isso.
public sealed class AuditoriaInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        PreencherDatas(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        PreencherDatas(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void PreencherDatas(DbContext? contexto)
    {
        if (contexto is null) return;

        var agora = DateTime.UtcNow;
        foreach (var entrada in contexto.ChangeTracker.Entries<EntidadeBase>())
        {
            if (entrada.State == EntityState.Modified)
                entrada.Property(e => e.AtualizadoEm).CurrentValue = agora;
        }
    }
}
