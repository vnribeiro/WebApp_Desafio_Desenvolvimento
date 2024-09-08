using WebAppDesafio.API.Domain.Models;

namespace WebAppDesafio.API.Infra.Repositorios.Interfaces;

public interface IChamadoRepositorio : IRepositorio<Chamado>
{
    Task<Chamado> GetByIdAsync(Guid id);
    Task<IEnumerable<Chamado>> GetAllAsync();
    Task AddAsync(Chamado chamado);
    Task UpdateAsync(Chamado chamado);
    Task DeleteAsync(Guid id);
}