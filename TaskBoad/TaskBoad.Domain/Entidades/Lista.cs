using TaskBoad.Domain.Comum;

namespace TaskBoad.Domain.Entidades;

/// Coluna do quadro (ex.: "A fazer", "Fazendo", "Feito").
public class Lista : EntidadeBase
{
    private readonly List<Cartao> _cartoes = [];

    public Guid QuadroId { get; private set; }
    public string Titulo { get; private set; } = null!;
    public double Posicao { get; private set; }
    public bool Arquivada { get; private set; }

    public Quadro Quadro { get; private set; } = null!;
    public IReadOnlyCollection<Cartao> Cartoes => _cartoes.AsReadOnly();

    private Lista() { } // usado pelo EF Core

    /// <param name="posicao">Calculada com <see cref="CalculadoraDePosicao"/> (normalmente NoFinal).</param>
    public Lista(Guid quadroId, string titulo, double posicao)
    {
        QuadroId = quadroId;
        Titulo = Validar.TextoObrigatorio(titulo, 100, "lista.titulo");
        Posicao = Validar.Posicao(posicao, "lista.posicao");
    }

    public void Renomear(string titulo) =>
        Titulo = Validar.TextoObrigatorio(titulo, 100, "lista.titulo");

    public void Mover(double novaPosicao) =>
        Posicao = Validar.Posicao(novaPosicao, "lista.posicao");

    public void Arquivar() => Arquivada = true;

    public void Restaurar() => Arquivada = false;
}
