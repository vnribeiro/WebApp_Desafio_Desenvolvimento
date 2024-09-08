using Shared.ViewModels.Atualizar;
using Shared.ViewModels.Criar;
using WebAppDesafio.API.Dominio.Models;

namespace WebAppDesafio.API.Servicos.Interfaces;

public interface IDepartamentoServico
{
    public Task<Departamento> Criar(CriarDepartamentoViewModel departamentoViewModel);
    public Task Atualizar(AtualizarDepartamentoViewModel departamentoViewModel);
    public Task Remover(Guid id);
}
