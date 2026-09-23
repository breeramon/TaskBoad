using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace TaskBoad.Infrastructure.Persistencia;

/// <summary>
/// Converte os nomes do C# (PascalCase) para snake_case no banco:
/// AreaDeTrabalhoId -> area_de_trabalho_id. Assim não é preciso usar aspas nas consultas SQL.
/// </summary>
internal static class ConvencaoSnakeCase
{
    public static void Aplicar(ModelBuilder modelBuilder)
    {
        foreach (var entidade in modelBuilder.Model.GetEntityTypes())
        {
            var tabela = entidade.GetTableName();
            if (tabela is not null)
                entidade.SetTableName(ParaSnakeCase(tabela));

            foreach (var propriedade in entidade.GetProperties())
            {
                // A coluna de concorrência (xmin) é do sistema do Postgres e já tem o nome certo.
                if (propriedade.IsConcurrencyToken) continue;

                var coluna = propriedade.GetColumnName();
                if (coluna is not null)
                    propriedade.SetColumnName(ParaSnakeCase(coluna));
            }

            foreach (var chave in entidade.GetKeys())
            {
                var nome = chave.GetName();
                if (nome is not null) chave.SetName(ParaSnakeCase(nome));
            }

            foreach (var fk in entidade.GetForeignKeys())
            {
                var nome = fk.GetConstraintName();
                if (nome is not null) fk.SetConstraintName(ParaSnakeCase(nome));
            }

            foreach (var indice in entidade.GetIndexes())
            {
                var nome = indice.GetDatabaseName();
                if (nome is not null) indice.SetDatabaseName(ParaSnakeCase(nome));
            }
        }
    }

    public static string ParaSnakeCase(string nome) =>
        Regex.Replace(nome, "([a-z0-9])([A-Z])", "$1_$2").ToLowerInvariant();
}
