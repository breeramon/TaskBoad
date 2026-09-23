using Microsoft.EntityFrameworkCore;
using TaskBoad.Application.Abstracoes;
using TaskBoad.Domain.Comum;
using TaskBoad.Domain.Entidades;

namespace TaskBoad.Application.Perfis.ObterMeuPerfil;

/// <summary>
/// Devolve o perfil do usuário logado. No primeiro acesso, cria o perfil
/// e a área de trabalho pessoal dele.
/// </summary>
public sealed class ObterMeuPerfilHandler(IAppDbContext db, IUsuarioAtual usuarioAtual)
{
    public async Task<PerfilDto> ExecutarAsync(CancellationToken cancellationToken = default)
    {
        var usuarioId = usuarioAtual.Id;

        var perfil = await db.Perfis.FirstOrDefaultAsync(p => p.Id == usuarioId, cancellationToken);

        if (perfil is null)
        {
            var email = usuarioAtual.Email
                ?? throw new DominioException("perfil.email.obrigatorio", "O token não contém o e-mail do usuário.");

            // Sem nome no cadastro (login por e-mail), usa a parte antes do @.
            var nome = string.IsNullOrWhiteSpace(usuarioAtual.Nome) ? email.Split('@')[0] : usuarioAtual.Nome;

            perfil = new Perfil(usuarioId, nome, email);
            db.Perfis.Add(perfil);

            // O front mostra o nome traduzido para áreas com EhPessoal = true.
            db.AreasDeTrabalho.Add(AreaDeTrabalho.CriarPessoal(usuarioId, "Pessoal"));

            await db.SaveChangesAsync(cancellationToken);
        }

        return new PerfilDto(perfil.Id, perfil.Nome, perfil.Email, perfil.AvatarUrl, perfil.Idioma);
    }
}
