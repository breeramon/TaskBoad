namespace TaskBoad.Domain.Enums;

/// <summary>Tipos de evento registrados no histórico do quadro.</summary>
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
