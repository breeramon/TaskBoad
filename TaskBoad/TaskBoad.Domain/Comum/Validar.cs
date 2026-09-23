using System.Text.RegularExpressions;

namespace TaskBoad.Domain.Comum;

/// <summary>Validações reutilizadas pelas entidades.</summary>
internal static class Validar
{
    private static readonly Regex RegexCor = new("^#[0-9A-Fa-f]{6}$", RegexOptions.Compiled);

    public static string TextoObrigatorio(string? valor, int tamanhoMaximo, string campo)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new DominioException($"{campo}.obrigatorio", $"O campo '{campo}' é obrigatório.");

        var texto = valor.Trim();
        if (texto.Length > tamanhoMaximo)
            throw new DominioException($"{campo}.muito_longo",
                $"O campo '{campo}' aceita no máximo {tamanhoMaximo} caracteres.");

        return texto;
    }

    public static string? TextoOpcional(string? valor, int tamanhoMaximo, string campo) =>
        string.IsNullOrWhiteSpace(valor) ? null : TextoObrigatorio(valor, tamanhoMaximo, campo);

    public static string Cor(string? cor, string campo)
    {
        if (cor is null || !RegexCor.IsMatch(cor))
            throw new DominioException($"{campo}.invalida", $"A cor '{cor}' deve estar no formato #RRGGBB.");

        return cor.ToUpperInvariant();
    }

    public static string? CorOpcional(string? cor, string campo) =>
        string.IsNullOrWhiteSpace(cor) ? null : Cor(cor, campo);

    public static double Posicao(double posicao, string campo)
    {
        if (!double.IsFinite(posicao) || posicao <= 0)
            throw new DominioException($"{campo}.invalida", "A posição deve ser um número maior que zero.");

        return posicao;
    }
}
