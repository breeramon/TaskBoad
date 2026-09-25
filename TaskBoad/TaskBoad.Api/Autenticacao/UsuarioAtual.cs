using System.Security.Claims;
using System.Text.Json;
using TaskBoad.Application.Abstracoes;

namespace TaskBoad.Api.Autenticacao;

/// Lê os dados do usuário a partir dos claims do token da requisição atual.
internal sealed class UsuarioAtual(IHttpContextAccessor acessor) : IUsuarioAtual
{
    private ClaimsPrincipal? Principal => acessor.HttpContext?.User;

    public bool EstaAutenticado => Principal?.Identity?.IsAuthenticated == true;

    public Guid Id =>
        Guid.TryParse(Principal?.FindFirstValue("sub"), out var id)
            ? id
            : throw new InvalidOperationException("Não há usuário autenticado nesta requisição.");

    public string? Email => Principal?.FindFirstValue("email");

    /// O Supabase coloca os dados do cadastro no claim "user_metadata" (um JSON).
    /// No login com Google vem "full_name" ou "name".
    public string? Nome
    {
        get
        {
            var metadados = Principal?.FindFirstValue("user_metadata");
            if (string.IsNullOrWhiteSpace(metadados)) return null;

            try
            {
                using var json = JsonDocument.Parse(metadados);
                foreach (var campo in new[] { "full_name", "name" })
                {
                    if (json.RootElement.TryGetProperty(campo, out var valor) &&
                        valor.ValueKind == JsonValueKind.String &&
                        !string.IsNullOrWhiteSpace(valor.GetString()))
                        return valor.GetString();
                }
            }
            catch (JsonException)
            {
                // Metadados em formato inesperado: segue sem nome.
            }

            return null;
        }
    }
}
