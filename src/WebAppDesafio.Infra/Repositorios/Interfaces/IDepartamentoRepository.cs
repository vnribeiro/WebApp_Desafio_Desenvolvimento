using WebAppDesafio.Dominio.Models;

namespace WebAppDesafio.Infra.Repositorios.Interfaces;

public interface IDepartamentoRepository : IRepositorio<Departamento>
{
    Task<Departamento> GetByIdAsync(Guid id);
    Task<IEnumerable<Departamento>> GetAllAsync();
    Task AddAsync(Departamento departamento);
}