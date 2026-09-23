using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TaskBoad.Domain.Comum;

namespace TaskBoad.Api.Erros;

/// <summary>
/// Converte exceções conhecidas em respostas HTTP padronizadas (ProblemDetails).
/// O campo "codigo" é o que o front usa para traduzir a mensagem (i18n).
/// </summary>
internal sealed class TratadorDeExcecoes(IProblemDetailsService problemDetails) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext contexto, Exception excecao, CancellationToken cancellationToken)
    {
        var (status, codigo, titulo) = excecao switch
        {
            DominioException e => (StatusCodes.Status400BadRequest, e.Codigo, e.Message),
            DbUpdateConcurrencyException => (StatusCodes.Status409Conflict, "concorrencia.conflito",
                "Este item foi alterado por outra pessoa. Recarregue e tente de novo."),
            _ => (0, "", "")
        };

        // Exceção desconhecida: deixa o ASP.NET responder 500 (e registrar no log).
        if (status == 0) return false;

        contexto.Response.StatusCode = status;
        return await problemDetails.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = contexto,
            Exception = excecao,
            ProblemDetails =
            {
                Status = status,
                Title = titulo,
                Extensions = { ["codigo"] = codigo }
            }
        });
    }
}
