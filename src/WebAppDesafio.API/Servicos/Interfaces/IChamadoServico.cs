using Shared.ViewModels.Atualizar;
using Shared.ViewModels.Criar;
using WebAppDesafio.API.Dominio.Models;

namespace WebAppDesafio.API.Servicos.Interfaces;

public interface IChamadoServico
{
    public Task<Chamado> Criar(CriarChamadoViewModel chamadoViewModel);
    public Task Atualizar(AtualizarChamadoViewModel chamadoViewModel);
    public Task Remover(Guid id);
}
