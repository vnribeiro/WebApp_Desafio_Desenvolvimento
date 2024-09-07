namespace WebAppDesafio.API.Infra.Dados;

public interface IUnitOfWork : IDisposable
{
    Task<bool> CommitAsync();
}

