namespace TaskBoad.Application.Abstracoes;

/// Quem está fazendo a requisição, lido do JWT do Supabase.
/// A Api fornece a implementação; os casos de uso só enxergam esta interface.
public interface IUsuarioAtual
{
    bool EstaAutenticado { get; }

    // Id do usuário no Supabase (claim "sub"). Lança erro se não houver usuário logado.
    Guid Id { get; }

    string? Email { get; }

    // Nome vindo do cadastro (ex.: nome da conta Google), se existir.
    string? Nome { get; }
}
