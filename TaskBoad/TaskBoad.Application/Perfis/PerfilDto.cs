namespace TaskBoad.Application.Perfis;

/// Dados do perfil devolvidos pela API (nunca devolvemos a entidade direto).
public sealed record PerfilDto(Guid Id, string Nome, string Email, string? AvatarUrl, string Idioma);
