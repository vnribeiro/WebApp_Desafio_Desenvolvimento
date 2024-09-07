using WebAppDesafio.API.Infra.Repositorios;
using WebAppDesafio.API.Infra.Repositorios.Interfaces;
using IUnitOfWork = WebAppDesafio.API.Infra.Dados.IUnitOfWork;
using UnitOfWork = WebAppDesafio.API.Infra.Dados.UnitOfWork;

namespace WebAppDesafio.API.Extensions;

public static class ConfiguracaoServicosExtensions
{
    /// <summary>
    /// Adiciona serviços de repositório ao IServiceCollection.
    /// </summary>
    /// <param name="services">O IServiceCollection ao qual os serviços serão adicionados.</param>
    /// <returns>O IServiceCollection com os serviços adicionados.</returns>
    public static IServiceCollection AddRepositorios(this IServiceCollection services)
    {
        // Unidade de trabalho é registrada como um serviço com escopo.
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Registra o repositório como um serviço com escopo.
        services.AddScoped<IChamadoRepositorio, ChamadoRepositorio>();
        services.AddScoped<IDepartamentoRepositorio, DepartamentoRepositorio>();
        return services;
    }

    /// <summary>
    /// Adiciona serviços ao IServiceCollection.
    /// </summary>
    /// <param name="services">O IServiceCollection ao qual os serviços serão adicionados.</param>
    /// <returns>O IServiceCollection com os serviços adicionados.</returns>
    public static IServiceCollection AddServicos(this IServiceCollection services)
    {
        return services;
    }
}