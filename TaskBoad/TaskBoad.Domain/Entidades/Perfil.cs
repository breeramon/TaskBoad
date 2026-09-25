using TaskBoad.Domain.Comum;

namespace TaskBoad.Domain.Entidades;

// Dados públicos do usuário. Login e senha ficam no Supabase Auth;
// o Id do perfil é o mesmo Id do usuário lá (auth.users.id).
public class Perfil : EntidadeBase
{
    public static readonly string[] IdiomasSuportados = ["pt-BR", "en"];

    public string Nome { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string? AvatarUrl { get; private set; }
    public string Idioma { get; private set; } = "pt-BR";

    private Perfil() { } // usado pelo EF Core

    public Perfil(Guid id, string nome, string email, string? idioma = null)
    {
        if (id == Guid.Empty)
            throw new DominioException("perfil.id.obrigatorio", "O Id do usuário é obrigatório.");

        Id = id;
        Nome = Validar.TextoObrigatorio(nome, 100, "perfil.nome");
        Email = Validar.TextoObrigatorio(email, 256, "perfil.email").ToLowerInvariant();

        if (idioma is not null)
            AlterarIdioma(idioma);
    }

    public void AtualizarDados(string nome, string? avatarUrl)
    {
        Nome = Validar.TextoObrigatorio(nome, 100, "perfil.nome");
        AvatarUrl = Validar.TextoOpcional(avatarUrl, 500, "perfil.avatar_url");
    }

    public void AlterarIdioma(string idioma)
    {
        if (!IdiomasSuportados.Contains(idioma))
            throw new DominioException("perfil.idioma.invalido", $"O idioma '{idioma}' não é suportado.");

        Idioma = idioma;
    }
}
