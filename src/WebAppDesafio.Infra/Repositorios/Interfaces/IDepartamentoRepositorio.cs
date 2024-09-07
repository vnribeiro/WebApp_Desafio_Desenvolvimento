using WebAppDesafio.Dominio.Models;

namespace WebAppDesafio.Infra.Repositorios.Interfaces;

public interface IDepartamentoRepositorio : IRepositorio<Departamento>
{
    Task<IEnumerable<Departamento>> GetAllAsync();
}