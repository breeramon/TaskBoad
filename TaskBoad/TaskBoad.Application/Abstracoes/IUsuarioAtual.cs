namespace TaskBoad.Application.Abstracoes;

/// <summary>
/// Quem está fazendo a requisição, lido do JWT do Supabase.
/// A Api fornece a implementação; os casos de uso só enxergam esta interface.
/// </summary>
public interface IUsuarioAtual
{
    bool EstaAutenticado { get; }

    /// <summary>Id do usuário no Supabase (claim "sub"). Lança erro se não houver usuário logado.</summary>
    Guid Id { get; }

    string? Email { get; }

    /// <summary>Nome vindo do cadastro (ex.: nome da conta Google), se existir.</summary>
    string? Nome { get; }
}
