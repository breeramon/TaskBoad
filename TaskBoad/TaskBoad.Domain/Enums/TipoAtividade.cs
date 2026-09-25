namespace TaskBoad.Domain.Enums;

/// Tipos de evento registrados no histórico do quadro.
public enum TipoAtividade
{
    QuadroCriado = 1,
    QuadroAtualizado,
    QuadroArquivado,
    ListaCriada,
    ListaAtualizada,
    ListaMovida,
    ListaArquivada,
    CartaoCriado,
    CartaoAtualizado,
    CartaoMovido,
    CartaoConcluido,
    CartaoArquivado,
    EtiquetaAplicada,
    ResponsavelAtribuido,
    ComentarioAdicionado,
    MembroAdicionado,
    MembroRemovido
}
