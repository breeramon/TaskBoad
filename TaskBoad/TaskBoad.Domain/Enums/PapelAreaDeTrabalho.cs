namespace TaskBoad.Domain.Enums;

/// Papel do usuário dentro de uma área de trabalho. Vale para todos os quadros dela.
/// Os valores crescem com o nível de permissão, para permitir comparações (papel >= Membro).
public enum PapelAreaDeTrabalho
{
    Leitor = 1,   // só visualiza
    Membro = 2,   // cria e edita quadros, listas e cartões
    Admin = 3,    // também gerencia membros
    Dono = 4      // criador; único e não pode sair
}

public static class PapelAreaDeTrabalhoExtensoes
{
    public static bool PodeEditar(this PapelAreaDeTrabalho papel) => papel >= PapelAreaDeTrabalho.Membro;

    public static bool PodeGerenciarMembros(this PapelAreaDeTrabalho papel) => papel >= PapelAreaDeTrabalho.Admin;
}
