using TaskBoad.Domain.Comum;

namespace TaskBoad.Domain.Entidades;

/// <summary>Quadro Kanban. Pertence a uma área de trabalho e contém listas e etiquetas.</summary>
public class Quadro : EntidadeBase
{
    private readonly List<Lista> _listas = [];
    private readonly List<Etiqueta> _etiquetas = [];

    public Guid AreaDeTrabalhoId { get; private set; }
    public string Titulo { get; private set; } = null!;
    public string? CorDeFundo { get; private set; }
    public bool Arquivado { get; private set; }
    public Guid CriadoPorId { get; private set; }

    public AreaDeTrabalho AreaDeTrabalho { get; private set; } = null!;
    public IReadOnlyCollection<Lista> Listas => _listas.AsReadOnly();
    public IReadOnlyCollection<Etiqueta> Etiquetas => _etiquetas.AsReadOnly();

    private Quadro() { } // usado pelo EF Core

    public Quadro(Guid areaDeTrabalhoId, string titulo, Guid criadoPorId, string? corDeFundo = null)
    {
        AreaDeTrabalhoId = areaDeTrabalhoId;
        Titulo = Validar.TextoObrigatorio(titulo, 100, "quadro.titulo");
        CorDeFundo = Validar.CorOpcional(corDeFundo, "quadro.cor_de_fundo");
        CriadoPorId = criadoPorId;
    }

    public void Renomear(string titulo) =>
        Titulo = Validar.TextoObrigatorio(titulo, 100, "quadro.titulo");

    public void AlterarCorDeFundo(string? cor) =>
        CorDeFundo = Validar.CorOpcional(cor, "quadro.cor_de_fundo");

    public void Arquivar() => Arquivado = true;

    public void Restaurar() => Arquivado = false;
}
