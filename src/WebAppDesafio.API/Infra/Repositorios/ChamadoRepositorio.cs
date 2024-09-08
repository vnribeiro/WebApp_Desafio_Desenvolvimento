using Microsoft.EntityFrameworkCore;
using WebAppDesafio.API.Domain.Models;
using WebAppDesafio.API.Infra.Dados;
using WebAppDesafio.API.Infra.Exceptions;
using WebAppDesafio.API.Infra.Repositorios.Interfaces;

namespace WebAppDesafio.API.Infra.Repositorios;

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
       var entidade =  await _context.Chamados
           .Include(x => x.Departamento)
           .AsNoTracking()
           .FirstOrDefaultAsync(x => x.Id == chamado.Id) 
                       ?? throw new EntidadeNaoEncontradaException($"Entidade com o ID {chamado.Id} não foi encontrada.");

        entidade.Atualizar(chamado.Assunto, 
            chamado.Solicitante, 
            chamado.Departamento, 
            chamado.DataAbertura);

        _context.Chamados.Update(chamado);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entidade = await _context.Chamados.FindAsync(id);

        if (entidade == null)
            throw new EntidadeNaoEncontradaException($"Entidade com o ID {id} não foi encontrada.");

        _context.Chamados.Remove(entidade);
    }
}