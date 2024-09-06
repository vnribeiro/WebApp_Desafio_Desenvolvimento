namespace WebAppDesafio.Infra.Dados;

public interface IUnitOfWork : IDisposable
{
    Task<bool> CommitAsync();
}

