using Microsoft.EntityFrameworkCore;
using WebAppDesafio.API.Domain.Models;
using WebAppDesafio.API.Infra.Dados;
using WebAppDesafio.API.Infra.Exceptions;
using WebAppDesafio.API.Infra.Repositorios.Interfaces;

namespace WebAppDesafio.API.Infra.Repositorios;

/// <summary>
/// Repositório para gerenciar operações relacionadas a departamentos.
/// </summary>
public class DepartamentoRepositorio : IDepartamentoRepositorio
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="DepartamentoRepositorio"/> com o contexto de banco de dados especificado.
    /// </summary>
    /// <param name="context">O contexto de banco de dados.</param>
    public DepartamentoRepositorio(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtém um departamento pelo ID especificado.
    /// </summary>
    /// <param name="id">O ID do departamento.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. O resultado contém o departamento.</returns>
    /// <exception cref="EntidadeNaoEncontradaException">Lançada se o departamento não for encontrado.</exception>
    public async Task<Departamento> GetByIdAsync(Guid id)
    {
        return await _context.Departamentos.FirstOrDefaultAsync(x => x.Id == id) ??
               throw new EntidadeNaoEncontradaException($"Entidade com o ID {id} não foi encontrada.");
    }

    /// <summary>
    /// Obtém todos os departamentos.
    /// </summary>
    /// <returns>Uma tarefa que representa a operação assíncrona. O resultado contém a lista de departamentos.</returns>
    public async Task<IEnumerable<Departamento>> GetAllAsync()
    {
        return await _context.Departamentos
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>
    /// Adiciona um novo departamento.
    /// </summary>
    /// <param name="departamento">O departamento a ser adicionado.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
    public async Task AddAsync(Departamento departamento)
    {
        await _context.Departamentos.AddAsync(departamento);
    }

    /// <summary>
    /// Atualiza um departamento existente.
    /// </summary>
    /// <param name="departamento">O departamento a ser atualizado.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
    /// <exception cref="EntidadeNaoEncontradaException">Lançada se o departamento não for encontrado.</exception>
    public async Task UpdateAsync(Departamento departamento)
    {
        var entidade = await _context.Departamentos.FirstOrDefaultAsync(x=> x.Id == departamento.Id)! 
                     ?? throw new EntidadeNaoEncontradaException($"Entidade com o ID {departamento.Id} não foi encontrada.");

        // Atualiza a descrição do departamento
        entidade.Atualizar(departamento.Descricao);

        _context.Departamentos.Update(entidade);
    }

    /// <summary>
    /// Remove um departamento pelo ID especificado.
    /// </summary>
    /// <param name="id">O ID do departamento a ser removido.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
    /// <exception cref="EntidadeNaoEncontradaException">Lançada se o departamento não for encontrado.</exception>
    public async Task DeleteAsync(Guid id)
    {
        var entidade = await _context.Departamentos.FindAsync(id);

        if (entidade == null)
            throw new EntidadeNaoEncontradaException($"Entidade com o ID {id} não foi encontrada.");

        _context.Departamentos.Remove(entidade);
    }
}