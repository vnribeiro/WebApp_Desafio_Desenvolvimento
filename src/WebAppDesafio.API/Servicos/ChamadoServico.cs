using Shared.ViewModels.Atualizar;
using Shared.ViewModels.Criar;
using WebAppDesafio.API.Domain.Models;
using WebAppDesafio.API.Infra.Dados;
using WebAppDesafio.API.Infra.Repositorios.Interfaces;
using WebAppDesafio.API.Servicos.Interfaces;

namespace WebAppDesafio.API.Servicos;

public class ChamadoServico : IChamadoServico
{
    private readonly IChamadoRepositorio _chamadoRepositorio; 
    private readonly IDepartamentoRepositorio _departamentoRepositorio;
    private readonly IUnitOfWork _unitOfWork;

    public ChamadoServico(IChamadoRepositorio chamadoRepositorio,
        IDepartamentoRepositorio departamentoRepositorio,
        IUnitOfWork unitOfWork)
    {
        _chamadoRepositorio = chamadoRepositorio;
        _departamentoRepositorio = departamentoRepositorio;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Cria um novo chamado com base no modelo de visão fornecido.
    /// </summary>
    /// <param name="chamadoViewModel">O modelo de visão contendo os dados do chamado.</param>
    /// <returns>O chamado criado.</returns>
    /// <exception cref="ArgumentException">Lançada se a data de abertura for retroativa.</exception>
    /// <exception cref="Exception">Lançada se o departamento não for encontrado ou se houver um erro ao cadastrar o chamado.</exception>
    public async Task<Chamado> Criar(CriarChamadoViewModel chamadoViewModel)
    {
        if (IsDataRetroativa(chamadoViewModel.DataAbertura))
        {
            throw new ArgumentException("Data de abertura retroativa não permitida ", nameof(chamadoViewModel.DataAbertura));
        }

        //Verifica se o departamento existe
        // Se não existir, lança uma exceção
        var departamento = await _departamentoRepositorio.GetByIdAsync(chamadoViewModel.IdDepartamento);

        if (departamento == null)
        {
            throw new Exception("Departamento não encontrado.");
        }

        // Map
        var chamado = new Chamado(chamadoViewModel.Assunto, chamadoViewModel.Solicitante, departamento, chamadoViewModel.DataAbertura);

        await _chamadoRepositorio.AddAsync(chamado);
        var result = await _unitOfWork.CommitAsync();

        if (result)
        {
            return chamado;
        }

        throw new Exception("Erro ao cadastrar chamado.");
    }

    /// <summary>
    /// Atualiza um chamado existente com base no modelo de visão fornecido.
    /// </summary>
    /// <param name="chamadoViewModel">O modelo de visão contendo os dados atualizados do chamado.</param>
    /// <returns>Uma tarefa representando a operação assíncrona.</returns>
    /// <exception cref="ArgumentException">Lançada se a data de abertura for retroativa.</exception>
    /// <exception cref="Exception">Lançada se o departamento não for encontrado ou se houver um erro ao atualizar o chamado.</exception>
    public async Task Atualizar(AtualizarChamadoViewModel chamadoViewModel)
    {
        if (IsDataRetroativa(chamadoViewModel.DataAbertura))
        {
            throw new ArgumentException("Data de abertura retroativa não permitida ", nameof(chamadoViewModel.DataAbertura));
        }

        //Verifica se o departamento existe
        // Se não existir, lança uma exceção
        var departamento = await _departamentoRepositorio.GetByIdAsync(chamadoViewModel.IdDepartamento);

        if (departamento == null)
        {
            throw new Exception("Departamento não encontrado.");
        }

        var chamado = new Chamado(chamadoViewModel.Id, chamadoViewModel.Assunto, chamadoViewModel.Solicitante, departamento, chamadoViewModel.DataAbertura);

        await _chamadoRepositorio.UpdateAsync(chamado);
        var result = await _unitOfWork.CommitAsync();

        if (!result)
        {
            throw new Exception("Erro ao atualizar chamado.");
        }
    }

    /// <summary>
    /// Remove um chamado com o ID especificado.
    /// </summary>
    /// <param name="id">O ID do chamado a ser removido.</param>
    /// <returns>Uma tarefa representando a operação assíncrona.</returns>
    /// <exception cref="Exception">Lançada se houver um erro ao excluir o chamado.</exception>
    public async Task Remover(Guid id)
    {
        await _chamadoRepositorio.DeleteAsync(id);
        var result = await _unitOfWork.CommitAsync();

        if (!result)
        {
            throw new Exception("Erro ao excluir chamado.");
        }
    }

    /// <summary>
    /// Verifica se a data fornecida é retroativa.
    /// </summary>
    /// <param name="data">A data a ser verificada.</param>
    /// <returns><c>true</c> se a data for retroativa; caso contrário, <c>false</c>.</returns>
    private static bool IsDataRetroativa(DateTime data)
    {
        // Coloca a data atual no formato dd/MM/yyyy
        var dataStr = DateTime.Today.ToString("dd/MM/yyyy");
        var dataAtual = DateTime.Parse(dataStr);

        // Compara a data fornecida com a data atual
        return data.Date < dataAtual.Date;
    }
}
