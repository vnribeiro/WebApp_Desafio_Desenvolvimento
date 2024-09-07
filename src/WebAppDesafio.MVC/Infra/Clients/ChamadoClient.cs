using Shared.ViewModels;
using Shared.ViewModels.Atualizar;
using Shared.ViewModels.Criar;
using WebAppDesafio.MVC.ViewModels;

namespace WebAppDesafio.MVC.Infra.Clients;

public class ChamadoClient : BaseClient
{
    private readonly HttpClient _httpClient;

    public ChamadoClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CustomResponse<ChamadoViewModel>> GetChamado(Guid id)
    {
        var response = await _httpClient.GetAsync($"api/v1/chamados/{id}");

        if (response.IsSuccessStatusCode)
        {
            var result = ConverterParaObj<CustomResponse<ChamadoViewModel>>(await response.Content.ReadAsStringAsync());

            if (result is { Sucesso: true })
            {
                return result;
            }
        }

        return new CustomResponse<ChamadoViewModel>
        {
            Sucesso = false,
            Mensagem = "Erro ao obter chamado."
        };
    }

    public async Task<CustomResponse<ChamadoViewModel>> GetChamados()
    {
        var response = await _httpClient.GetAsync($"api/v1/chamados");

        if (response.IsSuccessStatusCode)
        {
            var result = ConverterParaObj<CustomResponse<ChamadoViewModel>>(await response.Content.ReadAsStringAsync());

            if (result is { Sucesso: true })
            {
                return result;
            }
        }

        return new CustomResponse<ChamadoViewModel>
        {
            Sucesso = false,
            Mensagem = "Erro ao obter chamados."
        };
    }

    public async Task<CustomResponse<ChamadoViewModel>> CriarChamado(CriarChamadoViewModel chamado)
    {
        var content = ConverterParaJson(chamado);
        var response = await _httpClient.PostAsync($"api/v1/chamados", content);

        if (response.IsSuccessStatusCode)
        {
            var result = ConverterParaObj<CustomResponse<ChamadoViewModel>>(await response.Content.ReadAsStringAsync());

            if (result is { Sucesso: true })
            {
                return result;
            }
        }

        return new CustomResponse<ChamadoViewModel>
        {
            Sucesso = false,
            Mensagem = "Erro ao cadastrar chamado."
        };
    }

    public async Task<CustomResponse<ChamadoViewModel>> AtualizarChamado(Guid id, AtualizarChamadoViewModel chamado)
    {
        var content = ConverterParaJson(chamado);
        var response = await _httpClient.PutAsync($"api/v1/chamados/{id}", content);

        if (response.IsSuccessStatusCode)
        {
            var result = ConverterParaObj<CustomResponse<ChamadoViewModel>>(await response.Content.ReadAsStringAsync());

            if (result is { Sucesso: true })
            {
                return result;
            }
        }

        return new CustomResponse<ChamadoViewModel>
        {
            Sucesso = false,
            Mensagem = "Erro ao atualizar chamado."
        };
    }

    public async Task<CustomResponse<ChamadoViewModel>> ExcluirChamado(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/v1/chamados/{id}");

        if (response.IsSuccessStatusCode)
        {
            var result = ConverterParaObj<CustomResponse<ChamadoViewModel>>(await response.Content.ReadAsStringAsync());

            if (result is { Sucesso: true })
            {
                return result;
            }
        }

        return new CustomResponse<ChamadoViewModel>
        {
            Sucesso = false,
            Mensagem = "Erro ao excluir chamado."
        };
    }
}

