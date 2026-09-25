using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using TaskBoad.Application.Abstracoes;

namespace TaskBoad.Api.Autenticacao;

public static class AutenticacaoSupabase
{
    // Configura a API para aceitar os tokens (JWT) emitidos pelo Supabase Auth.
    // A API não faz login: só confere se o token é válido e de quem ele é.
    public static IServiceCollection AddAutenticacaoSupabase(this IServiceCollection services, IConfiguration configuracao)
    {
        var urlSupabase = configuracao["Supabase:Url"];
        if (string.IsNullOrWhiteSpace(urlSupabase))
            throw new InvalidOperationException("Configure 'Supabase:Url' no appsettings.json.");

        // Ex.: https://abcd.supabase.co/auth/v1 — é o valor do claim "iss" dos tokens.
        var emissor = $"{urlSupabase.TrimEnd('/')}/auth/v1";

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opcoes =>
            {
                opcoes.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                    $"{emissor}/.well-known/jwks.json",
                    new RecuperadorJwksSupabase(),
                    new HttpDocumentRetriever { RequireHttps = true });

                // Mantém os nomes originais dos claims ("sub", "email") em vez de convertê-los.
                opcoes.MapInboundClaims = false;

                opcoes.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = emissor,
                    ValidateAudience = true,
                    ValidAudience = "authenticated",   // tokens de usuários logados no Supabase
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(30),
                    NameClaimType = "sub"
                };
            });

        services.AddAuthorization();
        services.AddHttpContextAccessor();
        services.AddScoped<IUsuarioAtual, UsuarioAtual>();

        return services;
    }
}
