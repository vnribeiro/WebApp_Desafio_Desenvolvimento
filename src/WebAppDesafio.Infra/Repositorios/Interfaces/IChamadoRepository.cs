using WebAppDesafio.Dominio.Models;

namespace WebAppDesafio.Infra.Repositorios.Interfaces;

public interface IChamadoRepository : IRepositorio<Chamado>
{
    Task<Chamado> GetByIdAsync(Guid id);
    Task<IEnumerable<Chamado>> GetAllAsync();
    Task AddAsync(Chamado chamado);
}