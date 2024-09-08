using Shared.ViewModels;
using Shared.ViewModels.Atualizar;
using Shared.ViewModels.Criar;
using WebAppDesafio.MVC.ViewModels;

namespace WebAppDesafio.MVC.Infra.Clients;

/// <summary>
/// Cliente para consumir a API de chamados.
/// </summary>
public class ChamadoClient : BaseClient
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="ChamadoClient"/> com o cliente HTTP especificado.
    /// </summary>
    /// <param name="httpClient">O cliente HTTP.</param>
    public ChamadoClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Obtém um chamado pelo ID especificado.
    /// </summary>
    /// <param name="id">O ID do chamado.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. O resultado contém a resposta personalizada com o chamado.</returns>
    /// <exception cref="ApplicationException">Lançada se houver um erro ao obter o chamado.</exception>
    public async Task<CustomResponse<ChamadoViewModel>> GetChamado(Guid id)
    {
        var response = await _httpClient.GetAsync($"api/v1/chamados/{id}");

        if (response.IsSuccessStatusCode)
        {
            var result = ConverterParaObj<CustomResponse<ChamadoViewModel>>(await response.Content.ReadAsStringAsync());

            if (result != null)
            {
                return result;
            }
        }

        throw new ApplicationException("Erro ao obter chamado.");
    }

    /// <summary>
    /// Obtém todos os chamados.
    /// </summary>
    /// <returns>Uma tarefa que representa a operação assíncrona. O resultado contém a resposta personalizada com a lista de chamados.</returns>
    /// <exception cref="ApplicationException">Lançada se houver um erro ao obter os chamados.</exception>
    public async Task<CustomResponse<IEnumerable<ChamadoViewModel>>> GetChamados()
    {
        var response = await _httpClient.GetAsync("api/v1/chamados");

        if (response.IsSuccessStatusCode)
        {
            var result = ConverterParaObj<CustomResponse<IEnumerable<ChamadoViewModel>>>(await response.Content.ReadAsStringAsync());

            if (result != null)
            {
                return result;
            }
        }

        throw new ApplicationException("Erro ao obter chamados.");
    }

    /// <summary>
    /// Cria um novo chamado.
    /// </summary>
    /// <param name="chamado">O modelo de visão contendo os dados do chamado a ser criado.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. O resultado contém a resposta personalizada com o chamado criado.</returns>
    /// <exception cref="ApplicationException">Lançada se houver um erro ao criar o chamado.</exception>
    public async Task<CustomResponse<ChamadoViewModel>> CriarChamado(CriarChamadoViewModel chamado)
    {
        var content = ConverterParaJson(chamado);
        var response = await _httpClient.PostAsync("api/v1/chamados", content);

        if (response.IsSuccessStatusCode)
        {
            var result = ConverterParaObj<CustomResponse<ChamadoViewModel>>(await response.Content.ReadAsStringAsync());

            if (result != null)
            {
                return result;
            }
        }

        throw new ApplicationException("Erro ao criar chamado.");
    }

    /// <summary>
    /// Atualiza um chamado existente.
    /// </summary>
    /// <param name="id">O ID do chamado a ser atualizado.</param>
    /// <param name="chamado">O modelo de visão contendo os dados atualizados do chamado.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. O resultado contém a resposta personalizada com o chamado atualizado.</returns>
    /// <exception cref="ApplicationException">Lançada se houver um erro ao atualizar o chamado.</exception>
    public async Task<CustomResponse<ChamadoViewModel>> AtualizarChamado(Guid id, AtualizarChamadoViewModel chamado)
    {
        var content = ConverterParaJson(chamado);
        var response = await _httpClient.PutAsync($"api/v1/chamados/{id}", content);

        if (response.IsSuccessStatusCode)
        {
            var result = ConverterParaObj<CustomResponse<ChamadoViewModel>>(await response.Content.ReadAsStringAsync());
            
            if (result != null)
            {
                return result;
            }
        }

        throw new ApplicationException("Erro ao atualizar chamado.");
    }

    /// <summary>
    /// Exclui um chamado pelo ID especificado.
    /// </summary>
    /// <param name="id">O ID do chamado a ser excluído.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. O resultado contém a resposta personalizada com o chamado excluído.</returns>
    /// <exception cref="ApplicationException">Lançada se houver um erro ao excluir o chamado.</exception>
    public async Task<CustomResponse<ChamadoViewModel>> ExcluirChamado(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/v1/chamados/{id}");

        if (response.IsSuccessStatusCode)
        {
            var result = ConverterParaObj<CustomResponse<ChamadoViewModel>>(await response.Content.ReadAsStringAsync());

            if (result != null)
            {
                return result;
            }
        }

        throw new ApplicationException("Erro ao excluir chamado.");
    }
}

