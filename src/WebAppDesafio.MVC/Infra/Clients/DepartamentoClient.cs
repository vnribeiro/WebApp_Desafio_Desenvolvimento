using Shared.ViewModels;
using Shared.ViewModels.Atualizar;
using Shared.ViewModels.Criar;
using WebAppDesafio.MVC.ViewModels;

namespace WebAppDesafio.MVC.Infra.Clients;

/// <summary>
/// Cliente para consumir a API de departamentos.
/// </summary>
public class DepartamentoClient : BaseClient
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="DepartamentoClient"/> com o cliente HTTP especificado.
    /// </summary>
    /// <param name="httpClient">O cliente HTTP.</param>
    public DepartamentoClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Obtém um departamento pelo ID especificado.
    /// </summary>
    /// <param name="id">O ID do departamento.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. O resultado contém a resposta personalizada com o departamento.</returns>
    /// <exception cref="ApplicationException">Lançada se houver um erro ao obter o departamento.</exception>
    public async Task<CustomResponse<DepartamentoViewModel>> GetDepartamento(Guid id)
    {
        var response = await _httpClient.GetAsync($"api/v1/departamentos/{id}");

        if (response.IsSuccessStatusCode)
        {
            var result = ConverterParaObj<CustomResponse<DepartamentoViewModel>>(await response.Content.ReadAsStringAsync());

            if (result != null)
            {
                return result;
            }
        }

        throw new ApplicationException("Erro ao obter departamento.");
    }

    /// <summary>
    /// Obtém todos os departamentos.
    /// </summary>
    /// <returns>Uma tarefa que representa a operação assíncrona. O resultado contém a resposta personalizada com a lista de departamentos.</returns>
    /// <exception cref="ApplicationException">Lançada se houver um erro ao obter os departamentos.</exception>
    public async Task<CustomResponse<IEnumerable<DepartamentoViewModel>>> GetDepartamentos()
    {
        var response = await _httpClient.GetAsync($"api/v1/departamentos");

        if (response.IsSuccessStatusCode)
        {
            var result = ConverterParaObj<CustomResponse<IEnumerable<DepartamentoViewModel>>>(await response.Content.ReadAsStringAsync());

            if (result != null)
            {
                return result;
            }
        }

        throw new ApplicationException("Erro ao obter departamentos.");
    }

    /// <summary>
    /// Cria um novo departamento.
    /// </summary>
    /// <param name="departamento">O modelo de visão contendo os dados do departamento a ser criado.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. O resultado contém a resposta personalizada com o departamento criado.</returns>
    /// <exception cref="ApplicationException">Lançada se houver um erro ao criar o departamento.</exception>
    public async Task<CustomResponse<DepartamentoViewModel>> CriarDepartamento(CriarDepartamentoViewModel departamento)
    {
        var content = ConverterParaJson(departamento);
        var response = await _httpClient.PostAsync($"api/v1/departamentos", content);

        if (response.IsSuccessStatusCode)
        {
            var result = ConverterParaObj<CustomResponse<DepartamentoViewModel>>(await response.Content.ReadAsStringAsync());

            if (result != null)
            {
                return result;
            }
        }

        throw new ApplicationException("Erro ao criar departamento.");
    }

    /// <summary>
    /// Atualiza um departamento existente.
    /// </summary>
    /// <param name="id">O ID do departamento a ser atualizado.</param>
    /// <param name="departamento">O modelo de visão contendo os dados atualizados do departamento.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. O resultado contém a resposta personalizada com o departamento atualizado.</returns>
    /// <exception cref="ApplicationException">Lançada se houver um erro ao atualizar o departamento.</exception>
    public async Task<CustomResponse<DepartamentoViewModel>> AtualizarDepartamento(Guid id, AtualizarDepartamentoViewModel departamento)
    {
        var content = ConverterParaJson(departamento);
        var response = await _httpClient.PutAsync($"api/v1/departamentos/{id}", content);

        if (response.IsSuccessStatusCode)
        {
            var result = ConverterParaObj<CustomResponse<DepartamentoViewModel>>(await response.Content.ReadAsStringAsync());

            if (result != null)
            {
                return result;
            }
        }

        throw new ApplicationException("Erro ao atualizar departamento.");
    }

    /// <summary>
    /// Exclui um departamento pelo ID especificado.
    /// </summary>
    /// <param name="id">O ID do departamento a ser excluído.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. O resultado contém a resposta personalizada com o departamento excluído.</returns>
    /// <exception cref="ApplicationException">Lançada se houver um erro ao excluir o departamento.</exception>
    public async Task<CustomResponse<DepartamentoViewModel>> ExcluirDepartamento(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/v1/departamentos/{id}");

        if (response.IsSuccessStatusCode)
        {
            var result = ConverterParaObj<CustomResponse<DepartamentoViewModel>>(await response.Content.ReadAsStringAsync());

            if (result != null)
            {
                return result;
            }
        }
        
        throw new ApplicationException("Erro ao excluir departamento.");
    }
}

