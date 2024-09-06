using Microsoft.EntityFrameworkCore;
using WebAppDesafio.Dominio.Models;
using WebAppDesafio.Infra.Dados;
using WebAppDesafio.Infra.Exceptions;
using WebAppDesafio.Infra.Repositorios.Interfaces;

namespace WebAppDesafio.Infra.Repositorios;

public class DepartamentoRepositorio : IDepartamentoRepository
{
    private readonly AppDbContext _context;

    public DepartamentoRepositorio(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Departamento> GetByIdAsync(Guid id)
    {
        return await _context.Departamentos
            .FirstOrDefaultAsync(x => x.Id == id) ??
               throw new EntidadeNaoEncontradaException($"Entidade com o ID {id} não foi encontrada.");
    }

    public async Task<IEnumerable<Departamento>> GetAllAsync()
    {
        return await _context.Departamentos
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Departamento departamento)
    {
        await _context.Departamentos.AddAsync(departamento);
    }
}