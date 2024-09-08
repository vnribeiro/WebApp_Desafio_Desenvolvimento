using System.Text.Json;
using System.Text;

namespace WebAppDesafio.MVC.Infra.Clients;

/// <summary>
/// Classe utilitária para conversão de objetos para JSON e vice-versa.
/// </summary>
public abstract class BaseClient
{
    /// <summary>
    /// Converte um objeto para uma string JSON.
    /// </summary>
    /// <param name="obj">O objeto a ser convertido para JSON.</param>
    /// <returns>Um <see cref="StringContent"/> contendo a representação JSON do objeto.</returns>
    protected StringContent ConverterParaJson(object obj)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        return new StringContent(JsonSerializer.Serialize(obj, options), Encoding.UTF8, "application/json");
    }

    /// <summary>
    /// Converte uma string JSON para um objeto do tipo especificado.
    /// </summary>
    /// <typeparam name="T">O tipo do objeto a ser desserializado.</typeparam>
    /// <param name="json">A string JSON a ser convertida para um objeto.</param>
    /// <returns>O objeto desserializado do tipo especificado.</returns>
    protected T? ConverterParaObj<T>(string json)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        return JsonSerializer.Deserialize<T>(json, options);
    }
}