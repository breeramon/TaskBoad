using TaskBoad.Domain.Comum;
using TaskBoad.Domain.Enums;

namespace TaskBoad.Domain.Entidades;

public class AreaDeTrabalho : EntidadeBase
{
    private readonly List<MembroAreaDeTrabalho> _membros = [];
    private readonly List<Quadro> _quadros = [];

    public string Nome { get; private set; } = null!;
    public string? Descricao { get; private set; }
    public bool EhPessoal { get; private set; }
    public Guid DonoId { get; private set; }

    public IReadOnlyCollection<MembroAreaDeTrabalho> Membros => _membros.AsReadOnly();
    public IReadOnlyCollection<Quadro> Quadros => _quadros.AsReadOnly();

    private AreaDeTrabalho() { } // usado pelo EF Core

    public AreaDeTrabalho(string nome, Guid donoId, string? descricao = null, bool ehPessoal = false)
    {
        Nome = Validar.TextoObrigatorio(nome, 100, "area_de_trabalho.nome");
        Descricao = Validar.TextoOpcional(descricao, 500, "area_de_trabalho.descricao");
        DonoId = donoId;
        EhPessoal = ehPessoal;

        // Quem cria já entra como Dono.
        _membros.Add(new MembroAreaDeTrabalho(Id, donoId, PapelAreaDeTrabalho.Dono));
    }

    public static AreaDeTrabalho CriarPessoal(Guid donoId, string nome) =>
        new(nome, donoId, ehPessoal: true);

    public void Editar(string nome, string? descricao)
    {
        Nome = Validar.TextoObrigatorio(nome, 100, "area_de_trabalho.nome");
        Descricao = Validar.TextoOpcional(descricao, 500, "area_de_trabalho.descricao");
    }

    public PapelAreaDeTrabalho? PapelDe(Guid usuarioId) =>
        _membros.FirstOrDefault(m => m.UsuarioId == usuarioId)?.Papel;

    public MembroAreaDeTrabalho AdicionarMembro(Guid usuarioId, PapelAreaDeTrabalho papel)
    {
        if (EhPessoal)
            throw new DominioException("area_de_trabalho.pessoal_sem_membros",
                "A área de trabalho pessoal não aceita outros membros.");

        if (papel == PapelAreaDeTrabalho.Dono)
            throw new DominioException("area_de_trabalho.dono_unico", "A área de trabalho só pode ter um dono.");

        if (_membros.Any(m => m.UsuarioId == usuarioId))
            throw new DominioException("area_de_trabalho.membro_ja_existe", "Este usuário já é membro.");

        var membro = new MembroAreaDeTrabalho(Id, usuarioId, papel);
        _membros.Add(membro);
        return membro;
    }

    public void AlterarPapel(Guid usuarioId, PapelAreaDeTrabalho novoPapel)
    {
        if (novoPapel == PapelAreaDeTrabalho.Dono)
            throw new DominioException("area_de_trabalho.dono_unico", "A área de trabalho só pode ter um dono.");

        var membro = ObterMembro(usuarioId);
        if (membro.Papel == PapelAreaDeTrabalho.Dono)
            throw new DominioException("area_de_trabalho.papel_do_dono_fixo", "O papel do dono não pode ser alterado.");

        membro.AlterarPapel(novoPapel);
    }

    public void RemoverMembro(Guid usuarioId)
    {
        var membro = ObterMembro(usuarioId);
        if (membro.Papel == PapelAreaDeTrabalho.Dono)
            throw new DominioException("area_de_trabalho.dono_nao_pode_sair", "O dono não pode ser removido.");

        _membros.Remove(membro);
    }

    private MembroAreaDeTrabalho ObterMembro(Guid usuarioId) =>
        _membros.FirstOrDefault(m => m.UsuarioId == usuarioId)
        ?? throw new DominioException("area_de_trabalho.membro_nao_encontrado", "Membro não encontrado.");
}
