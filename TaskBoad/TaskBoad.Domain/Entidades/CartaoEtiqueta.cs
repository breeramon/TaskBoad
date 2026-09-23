namespace TaskBoad.Domain.Entidades;

/// <summary>Ligação N:N entre Cartão e Etiqueta. Chave composta (CartaoId + EtiquetaId).</summary>
public class CartaoEtiqueta
{
    public Guid CartaoId { get; private set; }
    public Guid EtiquetaId { get; private set; }

    public Etiqueta Etiqueta { get; private set; } = null!;

    private CartaoEtiqueta() { } // usado pelo EF Core

    internal CartaoEtiqueta(Guid cartaoId, Guid etiquetaId)
    {
        CartaoId = cartaoId;
        EtiquetaId = etiquetaId;
    }
}
