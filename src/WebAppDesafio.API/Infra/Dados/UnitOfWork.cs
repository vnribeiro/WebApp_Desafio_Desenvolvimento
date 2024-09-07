namespace WebAppDesafio.API.Infra.Dados;

public class UnitOfWork : API.Infra.Dados.IUnitOfWork
{
    private readonly API.Infra.Dados.AppDbContext _context;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="UnitOfWork"/> com o DbContext especificado.
    /// </summary>
    /// <param name="context">O DbContext a ser utilizado por esta unidade de trabalho.</param>
    public UnitOfWork(API.Infra.Dados.AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Confirma todas as alterações feitas neste contexto no banco de dados de forma assíncrona.
    /// </summary>
    /// <returns>Uma tarefa que representa a operação assíncrona de salvamento. O resultado da tarefa contém true se as alterações foram confirmadas com sucesso; caso contrário, false.</returns>
    public async Task<bool> CommitAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Libera os recursos do DbContext.
    /// </summary>
    public void Dispose()
    {
        _context.Dispose();
    }
}