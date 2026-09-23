using TaskBoad.Domain.Enums;

namespace TaskBoad.Domain.Entidades;

/// <summary>
/// Ligação N:N entre Perfil e AreaDeTrabalho, com o papel do usuário.
/// Chave composta (AreaDeTrabalhoId + UsuarioId), por isso não herda EntidadeBase.
/// Só é criada/alterada pela própria AreaDeTrabalho.
/// </summary>
public class MembroAreaDeTrabalho
{
    public Guid AreaDeTrabalhoId { get; private set; }
    public Guid UsuarioId { get; private set; }
    public PapelAreaDeTrabalho Papel { get; private set; }
    public DateTime EntrouEm { get; private set; }

    public Perfil Usuario { get; private set; } = null!;

    private MembroAreaDeTrabalho() { } // usado pelo EF Core

    internal MembroAreaDeTrabalho(Guid areaDeTrabalhoId, Guid usuarioId, PapelAreaDeTrabalho papel)
    {
        AreaDeTrabalhoId = areaDeTrabalhoId;
        UsuarioId = usuarioId;
        Papel = papel;
        EntrouEm = DateTime.UtcNow;
    }

    internal void AlterarPapel(PapelAreaDeTrabalho papel) => Papel = papel;
}
