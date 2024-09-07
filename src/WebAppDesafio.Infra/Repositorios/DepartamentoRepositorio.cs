using Microsoft.EntityFrameworkCore;
using WebAppDesafio.Dominio.Models;
using WebAppDesafio.Infra.Dados;
using WebAppDesafio.Infra.Exceptions;
using WebAppDesafio.Infra.Repositorios.Interfaces;

namespace WebAppDesafio.Infra.Repositorios;

public class DepartamentoRepositorio : IDepartamentoRepositorio
{
    private readonly AppDbContext _context;

    public DepartamentoRepositorio(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Departamento>> GetAllAsync()
    {
        return await _context.Departamentos
            .AsNoTracking()
            .ToListAsync();
    }
}