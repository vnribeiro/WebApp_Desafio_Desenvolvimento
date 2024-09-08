using Shared.ViewModels.Atualizar;
using Shared.ViewModels.Criar;
using WebAppDesafio.API.Domain.Models;
using WebAppDesafio.API.Infra.Dados;
using WebAppDesafio.API.Infra.Repositorios.Interfaces;
using WebAppDesafio.API.Servicos.Interfaces;

namespace WebAppDesafio.API.Servicos;

public class DepartamentoServico : IDepartamentoServico
{
    private readonly IDepartamentoRepositorio _departamentoRepositorio; 
    private readonly IUnitOfWork _unitOfWork;

    public DepartamentoServico(IDepartamentoRepositorio departamentoRepositorio, 
        IUnitOfWork unitOfWork)
    {
        _departamentoRepositorio = departamentoRepositorio;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Cria um novo departamento com base no modelo de visão fornecido.
    /// </summary>
    /// <param name="departamentoViewModel">O modelo de visão contendo os dados do departamento.</param>
    /// <returns>O departamento criado.</returns>
    /// <exception cref="Exception">Lançada se houver um erro ao cadastrar o departamento.</exception>
    public async Task<Departamento> Criar(CriarDepartamentoViewModel departamentoViewModel)
    {
        // Map
        var departamento = new Departamento(departamentoViewModel.Descricao);

        await _departamentoRepositorio.AddAsync(departamento);
        var result = await _unitOfWork.CommitAsync();

        if (result)
        {
            return departamento;
        }

        throw new Exception("Erro ao cadastrar departamento.");
    }

    /// <summary>
    /// Atualiza um departamento existente com base no modelo de visão fornecido.
    /// </summary>
    /// <param name="departamentoViewModel">O modelo de visão contendo os dados atualizados do departamento.</param>
    /// <returns>Uma tarefa representando a operação assíncrona.</returns>
    /// <exception cref="Exception">Lançada se houver um erro ao atualizar o departamento.</exception>
    public async Task Atualizar(AtualizarDepartamentoViewModel departamentoViewModel)
    {
        var departamento = new Departamento(departamentoViewModel.Id, departamentoViewModel.Descricao);

        await _departamentoRepositorio.UpdateAsync(departamento);
        var result = await _unitOfWork.CommitAsync();

        if (!result)
        {
            throw new Exception("Erro ao atualizar departamento.");
        }
    }

    /// <summary>
    /// Remove um departamento com o ID especificado.
    /// </summary>
    /// <param name="id">O ID do departamento a ser removido.</param>
    /// <returns>Uma tarefa representando a operação assíncrona.</returns>
    /// <exception cref="Exception">Lançada se houver um erro ao excluir o departamento.</exception>
    public async Task Remover(Guid id)
    {
        await _departamentoRepositorio.DeleteAsync(id);
        var result = await _unitOfWork.CommitAsync();

        if (!result)
        {
            throw new Exception("Erro ao excluir departamento.");
        }
    }
}
