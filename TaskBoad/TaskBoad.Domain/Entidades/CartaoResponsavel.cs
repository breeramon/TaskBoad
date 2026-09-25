namespace TaskBoad.Domain.Entidades;

/// Ligação N:N entre Cartão e o usuário responsável. Chave composta (CartaoId + UsuarioId).
public class CartaoResponsavel
{
    public Guid CartaoId { get; private set; }
    public Guid UsuarioId { get; private set; }

    public Perfil Usuario { get; private set; } = null!;

    private CartaoResponsavel() { } // usado pelo EF Core

    internal CartaoResponsavel(Guid cartaoId, Guid usuarioId)
    {
        CartaoId = cartaoId;
        UsuarioId = usuarioId;
    }
}
