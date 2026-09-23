using TaskBoad.Domain.Comum;

namespace TaskBoad.Domain.Entidades;

/// <summary>Cartão (tarefa) dentro de uma lista.</summary>
public class Cartao : EntidadeBase
{
    public const int TamanhoMaximoTitulo = 200;
    public const int TamanhoMaximoDescricao = 10_000;

    private readonly List<CartaoEtiqueta> _etiquetas = [];
    private readonly List<CartaoResponsavel> _responsaveis = [];
    private readonly List<Checklist> _checklists = [];
    private readonly List<Comentario> _comentarios = [];

    public Guid ListaId { get; private set; }
    public string Titulo { get; private set; } = null!;
    public string? Descricao { get; private set; }
    public double Posicao { get; private set; }
    public DateTime? Prazo { get; private set; }
    public DateTime? ConcluidoEm { get; private set; }
    public string? CorDaCapa { get; private set; }
    public bool Arquivado { get; private set; }
    public Guid CriadoPorId { get; private set; }

    // Calculada: não vira coluna no banco (não tem setter).
    public bool Concluido => ConcluidoEm is not null;

    public Lista Lista { get; private set; } = null!;
    public IReadOnlyCollection<CartaoEtiqueta> Etiquetas => _etiquetas.AsReadOnly();
    public IReadOnlyCollection<CartaoResponsavel> Responsaveis => _responsaveis.AsReadOnly();
    public IReadOnlyCollection<Checklist> Checklists => _checklists.AsReadOnly();
    public IReadOnlyCollection<Comentario> Comentarios => _comentarios.AsReadOnly();

    private Cartao() { } // usado pelo EF Core

    public Cartao(Guid listaId, string titulo, double posicao, Guid criadoPorId)
    {
        ListaId = listaId;
        Titulo = Validar.TextoObrigatorio(titulo, TamanhoMaximoTitulo, "cartao.titulo");
        Posicao = Validar.Posicao(posicao, "cartao.posicao");
        CriadoPorId = criadoPorId;
    }

    public void AlterarTitulo(string titulo) =>
        Titulo = Validar.TextoObrigatorio(titulo, TamanhoMaximoTitulo, "cartao.titulo");

    public void AlterarDescricao(string? descricao) =>
        Descricao = Validar.TextoOpcional(descricao, TamanhoMaximoDescricao, "cartao.descricao");

    public void DefinirPrazo(DateTime? prazo) => Prazo = prazo?.ToUniversalTime();

    public void AlterarCorDaCapa(string? cor) =>
        CorDaCapa = Validar.CorOpcional(cor, "cartao.cor_da_capa");

    public void Concluir() => ConcluidoEm ??= DateTime.UtcNow;

    public void Reabrir() => ConcluidoEm = null;

    /// <summary>Move para outra lista (ou reordena na mesma) usando posição fracionária.</summary>
    public void MoverPara(Guid listaId, double posicao)
    {
        ListaId = listaId;
        Posicao = Validar.Posicao(posicao, "cartao.posicao");
    }

    // Os métodos abaixo exigem que a coleção correspondente tenha sido carregada (Include no EF Core).

    public void AplicarEtiqueta(Guid etiquetaId)
    {
        if (_etiquetas.Any(e => e.EtiquetaId == etiquetaId)) return;
        _etiquetas.Add(new CartaoEtiqueta(Id, etiquetaId));
    }

    public void RemoverEtiqueta(Guid etiquetaId) =>
        _etiquetas.RemoveAll(e => e.EtiquetaId == etiquetaId);

    public void AtribuirResponsavel(Guid usuarioId)
    {
        if (_responsaveis.Any(r => r.UsuarioId == usuarioId)) return;
        _responsaveis.Add(new CartaoResponsavel(Id, usuarioId));
    }

    public void RemoverResponsavel(Guid usuarioId) =>
        _responsaveis.RemoveAll(r => r.UsuarioId == usuarioId);

    public void Arquivar() => Arquivado = true;

    public void Restaurar() => Arquivado = false;
}
