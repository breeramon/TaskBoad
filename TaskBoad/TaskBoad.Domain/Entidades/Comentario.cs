using TaskBoad.Domain.Comum;

namespace TaskBoad.Domain.Entidades;

/// Comentário de um usuário em um cartão.
public class Comentario : EntidadeBase
{
    public Guid CartaoId { get; private set; }
    public Guid AutorId { get; private set; }
    public string Texto { get; private set; } = null!;
    public DateTime? EditadoEm { get; private set; }

    public Perfil Autor { get; private set; } = null!;

    private Comentario() { } // usado pelo EF Core

    public Comentario(Guid cartaoId, Guid autorId, string texto)
    {
        CartaoId = cartaoId;
        AutorId = autorId;
        Texto = Validar.TextoObrigatorio(texto, 5_000, "comentario.texto");
    }

    /// <summary>Só o autor pode editar o próprio comentário.</summary>
    public void Editar(Guid usuarioId, string texto)
    {
        if (usuarioId != AutorId)
            throw new DominioException("comentario.somente_autor", "Só o autor pode editar este comentário.");

        Texto = Validar.TextoObrigatorio(texto, 5_000, "comentario.texto");
        EditadoEm = DateTime.UtcNow;
    }
}
