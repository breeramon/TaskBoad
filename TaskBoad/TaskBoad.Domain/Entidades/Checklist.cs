using TaskBoad.Domain.Comum;

namespace TaskBoad.Domain.Entidades;

/// <summary>Lista de verificação dentro de um cartão (um cartão pode ter várias).</summary>
public class Checklist : EntidadeBase
{
    private readonly List<ItemChecklist> _itens = [];

    public Guid CartaoId { get; private set; }
    public string Titulo { get; private set; } = null!;
    public double Posicao { get; private set; }

    public IReadOnlyCollection<ItemChecklist> Itens => _itens.AsReadOnly();

    public int TotalDeItens => _itens.Count;
    public int ItensConcluidos => _itens.Count(i => i.Concluido);

    private Checklist() { } // usado pelo EF Core

    public Checklist(Guid cartaoId, string titulo, double posicao)
    {
        CartaoId = cartaoId;
        Titulo = Validar.TextoObrigatorio(titulo, 100, "checklist.titulo");
        Posicao = Validar.Posicao(posicao, "checklist.posicao");
    }

    public void Renomear(string titulo) =>
        Titulo = Validar.TextoObrigatorio(titulo, 100, "checklist.titulo");

    /// <summary>Adiciona um item no fim. Exige a coleção Itens carregada.</summary>
    public ItemChecklist AdicionarItem(string texto)
    {
        var ultima = _itens.Count == 0 ? (double?)null : _itens.Max(i => i.Posicao);
        var item = new ItemChecklist(Id, texto, CalculadoraDePosicao.NoFinal(ultima));
        _itens.Add(item);
        return item;
    }

    public void RemoverItem(Guid itemId) => _itens.RemoveAll(i => i.Id == itemId);
}
