using WebAppDesafio.API.Domain.Models;

namespace WebAppDesafio.API.Infra.Repositorios.Interfaces;

public interface IDepartamentoRepositorio : IRepositorio<Departamento>
{
    Task<Departamento> GetByIdAsync(Guid id);
    Task<IEnumerable<Departamento>> GetAllAsync(); 
    Task AddAsync(Departamento departamento);
    Task UpdateAsync(Departamento departamento);
    Task DeleteAsync(Guid id);
}