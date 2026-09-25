using TaskBoad.Domain.Comum;

namespace TaskBoad.Domain.Entidades;

/// Item de um checklist. Criado apenas pelo próprio Checklist.
public class ItemChecklist : EntidadeBase
{
    public Guid ChecklistId { get; private set; }
    public string Texto { get; private set; } = null!;
    public bool Concluido { get; private set; }
    public double Posicao { get; private set; }

    private ItemChecklist() { } // usado pelo EF Core

    internal ItemChecklist(Guid checklistId, string texto, double posicao)
    {
        ChecklistId = checklistId;
        Texto = Validar.TextoObrigatorio(texto, 500, "item_checklist.texto");
        Posicao = Validar.Posicao(posicao, "item_checklist.posicao");
    }

    public void EditarTexto(string texto) =>
        Texto = Validar.TextoObrigatorio(texto, 500, "item_checklist.texto");

    public void Marcar(bool concluido) => Concluido = concluido;

    public void Mover(double novaPosicao) =>
        Posicao = Validar.Posicao(novaPosicao, "item_checklist.posicao");
}
