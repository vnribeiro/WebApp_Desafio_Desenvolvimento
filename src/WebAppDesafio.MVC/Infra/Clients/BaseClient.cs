using System.Text.Json;
using System.Text;

namespace WebAppDesafio.MVC.Infra.Clients;

public abstract class BaseClient
{
    protected StringContent ConverterParaJson(object obj)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        return new StringContent(JsonSerializer.Serialize(obj, options), Encoding.UTF8, "application/json");
    }

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