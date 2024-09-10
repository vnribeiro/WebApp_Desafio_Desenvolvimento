using Microsoft.EntityFrameworkCore;
using WebAppDesafio.API.Domain.Models;
using WebAppDesafio.API.Infra.Dados;
using WebAppDesafio.API.Infra.Exceptions;
using WebAppDesafio.API.Infra.Repositorios.Interfaces;

namespace WebAppDesafio.API.Infra.Repositorios;

/// <summary>
/// Repositório para gerenciar operações relacionadas a chamados.
/// </summary>
public class ChamadoRepositorio : IChamadoRepositorio
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="ChamadoRepositorio"/> com o contexto de banco de dados especificado.
    /// </summary>
    /// <param name="context">O contexto de banco de dados.</param>
    public ChamadoRepositorio(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtém um chamado pelo ID especificado.
    /// </summary>
    /// <param name="id">O ID do chamado.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. O resultado contém o chamado.</returns>
    /// <exception cref="EntidadeNaoEncontradaException">Lançada se o chamado não for encontrado.</exception>
    public async Task<Chamado> GetByIdAsync(Guid id)
    {
        return await _context.Chamados
            .Include(x => x.Departamento)
            .FirstOrDefaultAsync(x => x.Id == id) ??
               throw new EntidadeNaoEncontradaException($"Entidade com o ID {id} não foi encontrada.");
    }

    /// <summary>
    /// Busca solicitantes com base em um nome parcial.
    /// </summary>
    /// <param name="nome">Nome parcial do solicitante para a pesquisa.</param>
    /// <returns>Uma lista de nomes de solicitantes que correspondem ao nome parcial fornecido.</returns>
    public async Task<IEnumerable<string>> GetSolicitanteAsync(string nome)
    {
        return await _context.Chamados
            .Where(x => x.Solicitante.ToUpper()
                .Contains(nome.ToUpper()))
            .Select(x => x.Solicitante)
            .Distinct()
            .ToListAsync();
    }

    /// <summary>
    /// Obtém todos os chamados.
    /// </summary>
    /// <returns>Uma tarefa que representa a operação assíncrona. O resultado contém a lista de chamados.</returns>
    public async Task<IEnumerable<Chamado>> GetAllAsync()
    {
        return await _context.Chamados
            .Include(x => x.Departamento)
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>
    /// Adiciona um novo chamado.
    /// </summary>
    /// <param name="chamado">O chamado a ser adicionado.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
    public async Task AddAsync(Chamado chamado)
    {
        await _context.Chamados.AddAsync(chamado);
    }

    /// <summary>
    /// Atualiza um chamado existente.
    /// </summary>
    /// <param name="chamado">O chamado a ser atualizado.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
    /// <exception cref="EntidadeNaoEncontradaException">Lançada se o chamado não for encontrado.</exception>
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

    /// <summary>
    /// Remove um chamado pelo ID especificado.
    /// </summary>
    /// <param name="id">O ID do chamado a ser removido.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
    /// <exception cref="EntidadeNaoEncontradaException">Lançada se o chamado não for encontrado.</exception>
    public async Task DeleteAsync(Guid id)
    {
        var entidade = await _context.Chamados.FindAsync(id);

        if (entidade == null)
            throw new EntidadeNaoEncontradaException($"Entidade com o ID {id} não foi encontrada.");

        _context.Chamados.Remove(entidade);
    }
}