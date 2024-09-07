using Shared.ViewModels;
using Shared.ViewModels.Atualizar;
using Shared.ViewModels.Criar;
using WebAppDesafio.MVC.ViewModels;

namespace WebAppDesafio.MVC.Infra.Clients;

public class DepartamentoClient : BaseClient
{
    private readonly HttpClient _httpClient;

    public DepartamentoClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CustomResponse<DepartamentoViewModel>> GetDepartamento(Guid id)
    {
        var response = await _httpClient.GetAsync($"api/v1/departamentos/{id}");

        if (response.IsSuccessStatusCode)
        {
            var result = ConverterParaObj<CustomResponse<DepartamentoViewModel>>(await response.Content.ReadAsStringAsync());

            if (result is { Sucesso: true })
            {
                return result;
            }
        }

        return new CustomResponse<DepartamentoViewModel>
        {
            Sucesso = false,
            Mensagem = "Erro ao obter departamento."
        };
    }

    public async Task<CustomResponse<DepartamentoViewModel>> GetDepartamentos()
    {
        var response = await _httpClient.GetAsync($"api/v1/departamentos");

        if (response.IsSuccessStatusCode)
        {
            var result = ConverterParaObj<CustomResponse<DepartamentoViewModel>>(await response.Content.ReadAsStringAsync());

            if (result is { Sucesso: true })
            {
                return result;
            }
        }

        return new CustomResponse<DepartamentoViewModel>
        {
            Sucesso = false,
            Mensagem = "Erro ao obter departamentos."
        };
    }

    public async Task<CustomResponse<DepartamentoViewModel>> CriarDepartamento(CriarDepartamentoViewModel departamento)
    {
        var content = ConverterParaJson(departamento);
        var response = await _httpClient.PostAsync($"api/v1/departamentos", content);

        if (response.IsSuccessStatusCode)
        {
            var result = ConverterParaObj<CustomResponse<DepartamentoViewModel>>(await response.Content.ReadAsStringAsync());

            if (result is { Sucesso: true })
            {
                return result;
            }
        }

        return new CustomResponse<DepartamentoViewModel>
        {
            Sucesso = false,
            Mensagem = "Erro ao cadastrar departamento."
        };
    }

    public async Task<CustomResponse<DepartamentoViewModel>> AtualizarDepartamento(Guid id, AtualizarDepartamentoViewModel departamento)
    {
        var content = ConverterParaJson(departamento);
        var response = await _httpClient.PutAsync($"api/v1/departamentos/{id}", content);

        if (response.IsSuccessStatusCode)
        {
            var result = ConverterParaObj<CustomResponse<DepartamentoViewModel>>(await response.Content.ReadAsStringAsync());

            if (result is { Sucesso: true })
            {
                return result;
            }
        }

        return new CustomResponse<DepartamentoViewModel>
        {
            Sucesso = false,
            Mensagem = "Erro ao atualizar departamento."
        };
    }

    public async Task<CustomResponse<DepartamentoViewModel>> ExcluirDepartamento(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/v1/departamentos/{id}");

        if (response.IsSuccessStatusCode)
        {
            var result = ConverterParaObj<CustomResponse<DepartamentoViewModel>>(await response.Content.ReadAsStringAsync());

            if (result is { Sucesso: true })
            {
                return result;
            }
        }

        return new CustomResponse<DepartamentoViewModel>
        {
            Sucesso = false,
            Mensagem = "Erro ao excluir departamento."
        };
    }
}

