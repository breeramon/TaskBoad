using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace TaskBoad.Api.Autenticacao;

/// Baixa as chaves públicas do Supabase (JWKS) usadas para verificar a assinatura dos tokens.
/// O ConfigurationManager do ASP.NET guarda essas chaves em cache e busca de novo quando o
/// Supabase trocar de chave.
internal sealed class RecuperadorJwksSupabase : IConfigurationRetriever<OpenIdConnectConfiguration>
{
    public async Task<OpenIdConnectConfiguration> GetConfigurationAsync(
        string address, IDocumentRetriever retriever, CancellationToken cancel)
    {
        var json = await retriever.GetDocumentAsync(address, cancel);
        var chaves = new JsonWebKeySet(json);

        var configuracao = new OpenIdConnectConfiguration { JsonWebKeySet = chaves };
        foreach (var chave in chaves.GetSigningKeys())
            configuracao.SigningKeys.Add(chave);

        return configuracao;
    }
}
