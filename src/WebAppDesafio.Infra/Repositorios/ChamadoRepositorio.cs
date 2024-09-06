using Microsoft.EntityFrameworkCore;
using WebAppDesafio.Dominio.Models;
using WebAppDesafio.Infra.Dados;
using WebAppDesafio.Infra.Exceptions;
using WebAppDesafio.Infra.Repositorios.Interfaces;

namespace WebAppDesafio.Infra.Repositorios;

public class ChamadoRepositorio : IChamadoRepository
{
    private readonly AppDbContext _context;

    public ChamadoRepositorio(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Chamado> GetByIdAsync(Guid id)
    {
        return await _context.Chamados
            .Include(x => x.Departamento)
            .FirstOrDefaultAsync(x => x.Id == id) ??
               throw new EntidadeNaoEncontradaException($"Entidade com o ID {id} não foi encontrada.");
    }

    public async Task<IEnumerable<Chamado>> GetAllAsync()
    {
        return await _context.Chamados
            .Include(x => x.Departamento)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Chamado chamado)
    {
        await _context.Chamados.AddAsync(chamado);
    }
}