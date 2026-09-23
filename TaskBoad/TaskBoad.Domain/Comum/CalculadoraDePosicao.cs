namespace TaskBoad.Domain.Comum;

/// <summary>
/// Ordenação por posição fracionária: listas, cartões e itens guardam um double.
/// Para mover um item entre dois outros, basta usar a média das posições vizinhas,
/// sem precisar renumerar os demais.
/// </summary>
public static class CalculadoraDePosicao
{
    public const double Intervalo = 1000;

    /// <summary>Posição para inserir no fim da coleção.</summary>
    public static double NoFinal(double? ultimaPosicao) => (ultimaPosicao ?? 0) + Intervalo;

    /// <summary>Posição entre dois vizinhos (null = não existe vizinho daquele lado).</summary>
    public static double Entre(double? anterior, double? proxima) => (anterior, proxima) switch
    {
        (null, null) => Intervalo,
        (null, double p) => p / 2,
        (double a, null) => a + Intervalo,
        (double a, double p) => (a + p) / 2
    };

    /// <summary>Indica que os vizinhos estão próximos demais e a coleção deve ser renumerada.</summary>
    public static bool PrecisaRebalancear(double? anterior, double? proxima) =>
        anterior is double a && proxima is double p && p - a < 0.0001;
}
