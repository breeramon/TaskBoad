using TaskBoad.Domain.Comum;

namespace TaskBoad.Domain.Entidades;

/// Etiqueta colorida definida no quadro e aplicada aos cartões dele.
public class Etiqueta : EntidadeBase
{
    public Guid QuadroId { get; private set; }
    public string Nome { get; private set; } = null!;
    public string Cor { get; private set; } = null!;

    private Etiqueta() { } // usado pelo EF Core

    public Etiqueta(Guid quadroId, string nome, string cor)
    {
        QuadroId = quadroId;
        Nome = Validar.TextoObrigatorio(nome, 50, "etiqueta.nome");
        Cor = Validar.Cor(cor, "etiqueta.cor");
    }

    public void Editar(string nome, string cor)
    {
        Nome = Validar.TextoObrigatorio(nome, 50, "etiqueta.nome");
        Cor = Validar.Cor(cor, "etiqueta.cor");
    }
}
