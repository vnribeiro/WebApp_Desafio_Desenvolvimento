using WebAppDesafio.MVC.Infra.Clients;

namespace WebAppDesafio.MVC.Extensions;

public static class ConfiguracaoAplicacaoExtensions
{
    public static IServiceCollection ConfigurarClients(this IServiceCollection service)
    {
        // Configura o client para acessar a API de Departamento
        service.AddHttpClient<DepartamentoClient>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7044/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        // Configura o client para acessar a API de chamado
        service.AddHttpClient<ChamadoClient>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7044/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        return service;
    }
}
