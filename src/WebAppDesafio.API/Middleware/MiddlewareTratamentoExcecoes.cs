using System.Net;
using System.Text.Json;

namespace WebAppDesafio.API.Middleware;

public class MiddlewareTratamentoExcecoes
{
    private readonly RequestDelegate _next;
    private readonly ILogger<MiddlewareTratamentoExcecoes> _logger;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="MiddlewareTratamentoExcecoes"/>.
    /// </summary>
    /// <param name="next">O próximo middleware no pipeline de requisição.</param>
    /// <param name="logger">O logger para registrar exceções.</param>
    public MiddlewareTratamentoExcecoes(RequestDelegate next,
        ILogger<MiddlewareTratamentoExcecoes> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Invoca o middleware para lidar com o contexto HTTP.
    /// </summary>
    /// <param name="context">O contexto HTTP.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu uma exceção não tratada.");

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new
            {
                Success = false,
                Message = "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde."
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await response.WriteAsync(jsonResponse);
        }
    }
}
