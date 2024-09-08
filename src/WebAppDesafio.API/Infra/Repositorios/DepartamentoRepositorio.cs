using Microsoft.EntityFrameworkCore;
using WebAppDesafio.API.Domain.Models;
using WebAppDesafio.API.Infra.Dados;
using WebAppDesafio.API.Infra.Exceptions;
using WebAppDesafio.API.Infra.Repositorios.Interfaces;

namespace WebAppDesafio.API.Infra.Repositorios;

public class DepartamentoRepositorio : IDepartamentoRepositorio
{
    private readonly AppDbContext _context;

    public DepartamentoRepositorio(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Departamento> GetByIdAsync(Guid id)
    {
        return await _context.Departamentos.FirstOrDefaultAsync(x => x.Id == id) ??
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

    public async Task UpdateAsync(Departamento departamento)
    {
        var entidade = await _context.Departamentos.FirstOrDefaultAsync(x=> x.Id == departamento.Id)! 
                     ?? throw new EntidadeNaoEncontradaException($"Entidade com o ID {departamento.Id} não foi encontrada.");

        // Atualiza a descrição do departamento
        entidade.Atualizar(departamento.Descricao);

        _context.Departamentos.Update(entidade);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entidade = await _context.Departamentos.FindAsync(id);

        if (entidade == null)
            throw new EntidadeNaoEncontradaException($"Entidade com o ID {id} não foi encontrada.");

        _context.Departamentos.Remove(entidade);
    }
}