namespace TaskBoad.Domain.Comum;
public abstract class EntidadeBase
{
    // Guid versão 7 é ordenável por data de criação, o que deixa os índices do Postgres mais eficientes.
    public Guid Id { get; protected set; } = Guid.CreateVersion7();
    public DateTime CriadoEm { get; protected set; } = DateTime.UtcNow;
    public DateTime? AtualizadoEm { get; protected set; }
}
