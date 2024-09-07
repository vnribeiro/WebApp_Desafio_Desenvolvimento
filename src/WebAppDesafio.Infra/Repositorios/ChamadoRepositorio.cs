using Microsoft.EntityFrameworkCore;
using WebAppDesafio.Dominio.Models;
using WebAppDesafio.Infra.Dados;
using WebAppDesafio.Infra.Exceptions;
using WebAppDesafio.Infra.Repositorios.Interfaces;

namespace WebAppDesafio.Infra.Repositorios;

public class ChamadoRepositorio : IChamadoRepositorio
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

    public async Task UpdateAsync(Chamado chamado)
    {
       var result = await _context.Chamados.FindAsync(chamado.Id);

        if (result == null)
            throw new EntidadeNaoEncontradaException($"Entidade com o ID {chamado.Id} não foi encontrada.");

        _context.Chamados.Update(chamado);
    }

    public async Task DeleteAsync(Guid id)
    {
        var result = await _context.Chamados.FindAsync(id);

        if (result == null)
            throw new EntidadeNaoEncontradaException($"Entidade com o ID {id} não foi encontrada.");

        _context.Chamados.Remove(result);
    }
}