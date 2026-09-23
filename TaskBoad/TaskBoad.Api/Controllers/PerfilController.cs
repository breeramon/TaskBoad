using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBoad.Application.Perfis;
using TaskBoad.Application.Perfis.ObterMeuPerfil;

namespace TaskBoad.Api.Controllers;

[ApiController]
[Route("api/v1/perfil")]
[Authorize] // exige um token válido do Supabase
public class PerfilController(ObterMeuPerfilHandler obterMeuPerfil) : ControllerBase
{
    /// <summary>Perfil do usuário logado. No primeiro acesso, cria o perfil e a área pessoal.</summary>
    [HttpGet]
    public async Task<ActionResult<PerfilDto>> Obter(CancellationToken cancellationToken) =>
        Ok(await obterMeuPerfil.ExecutarAsync(cancellationToken));
}
