using TaskBoad.Domain.Comum;
using TaskBoad.Domain.Enums;

namespace TaskBoad.Domain.Entidades;

/// Registro do histórico do quadro ("Ana moveu o cartão X de A fazer para Fazendo").
/// Dados guarda detalhes em JSON (vira jsonb no Postgres), ex.: {"deLista":"...","paraLista":"..."}.
public class Atividade : EntidadeBase
{
    public Guid QuadroId { get; private set; }
    public Guid? CartaoId { get; private set; }
    public Guid UsuarioId { get; private set; }
    public TipoAtividade Tipo { get; private set; }
    public string? Dados { get; private set; }

    public Perfil Usuario { get; private set; } = null!;

    private Atividade() { } // usado pelo EF Core

    public Atividade(Guid quadroId, Guid usuarioId, TipoAtividade tipo, Guid? cartaoId = null, string? dados = null)
    {
        QuadroId = quadroId;
        UsuarioId = usuarioId;
        Tipo = tipo;
        CartaoId = cartaoId;
        Dados = dados;
    }
}
