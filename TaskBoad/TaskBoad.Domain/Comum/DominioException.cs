namespace TaskBoad.Domain.Comum;
public class DominioException(string codigo, string mensagem) : Exception(mensagem)
{
    public string Codigo { get; } = codigo;
}
