using WebAppDesafio.MVC.Infra.Clients;

namespace WebAppDesafio.MVC.Extensions;

public static class ConfiguracaoAplicacaoExtensions
{
    private const string BaseUrl = "ApiSettings:BaseUrl";

    /// <summary>
    /// Configura as configurações de ambiente para a aplicação.
    /// </summary>
    /// <param name="builder">O construtor da aplicação web a ser configurado.</param>
    /// <returns>O construtor da aplicação web atualizado.</returns>
    public static WebApplicationBuilder ConfigurarAmbiente(this WebApplicationBuilder builder)
    {
        builder.Configuration
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables();

        if (builder.Environment.IsDevelopment())
            builder.Configuration.AddUserSecrets<Program>();

        return builder;
    }

    /// <summary>
    /// Configura os clientes HTTP para acessar as APIs de Departamento e Chamado.
    /// </summary>
    /// <param name="service">A coleção de serviços para adicionar os clientes HTTP.</param>
    /// <returns>A coleção de serviços com os clientes HTTP configurados.</returns>
    public static WebApplicationBuilder ConfigurarClients(this WebApplicationBuilder builder)
    {
        var baseUrl = builder.Configuration[BaseUrl];

        // Configura o client para acessar a API de Departamento
        builder.Services.AddHttpClient<DepartamentoClient>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        // Configura o client para acessar a API de chamado
        builder.Services.AddHttpClient<ChamadoClient>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        return builder;
    }
}
