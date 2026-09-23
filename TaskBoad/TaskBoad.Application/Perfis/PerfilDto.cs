namespace TaskBoad.Application.Perfis;

/// <summary>Dados do perfil devolvidos pela API (nunca devolvemos a entidade direto).</summary>
public sealed record PerfilDto(Guid Id, string Nome, string Email, string? AvatarUrl, string Idioma);
