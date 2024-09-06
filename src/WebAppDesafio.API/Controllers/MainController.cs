using Microsoft.AspNetCore.Mvc;

namespace WebAppDesafio.API.Controllers;

public abstract class MainController : ControllerBase
{
    /// <summary>
    /// Recupera as mensagens de erro do estado do modelo.
    /// </summary>
    /// <returns>Uma coleção enumerável de mensagens de erro do estado do modelo.</returns>
    protected IEnumerable<string> GetModelStateErros()
    {
        return ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage);
    }

    /// <summary>
    /// Recupera a versão da API solicitada a partir do contexto HTTP.
    /// </summary>
    /// <returns>A versão da API solicitada como uma string, ou null se nenhuma versão for especificada.</returns>
    protected string? GetApiVersion()
    {
        return HttpContext.GetRequestedApiVersion()?.ToString();
    }
}