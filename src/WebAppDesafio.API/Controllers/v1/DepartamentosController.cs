using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Shared.ViewModels;
using Shared.ViewModels.Atualizar;
using Shared.ViewModels.Criar;
using WebAppDesafio.API.Infra.Dados;
using WebAppDesafio.API.Infra.Exceptions;
using WebAppDesafio.API.Infra.Repositorios.Interfaces;
using Departamento = WebAppDesafio.API.Dominio.Models.Departamento;

namespace WebAppDesafio.API.Controllers.v1;

[ApiController]
[ApiVersion(1.0)]
[Route("api/v{apiVersion:apiVersion}/departamentos")]
public class DepartamentosController : MainController
{
    private readonly IDepartamentoRepositorio _departamentoRepositorio;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ChamadosController> _logger;

    public DepartamentosController(IDepartamentoRepositorio departamentoRepositorio, 
        IUnitOfWork unitOfWork,
        ILogger<ChamadosController> logger)
    {
        _departamentoRepositorio = departamentoRepositorio;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <summary>
    /// Obtém um departamento pelo ID.
    /// </summary>
    /// <param name="id">ID do departamento.</param>
    /// <returns>Retorna o departamento correspondente ao ID fornecido.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CustomResponse<Departamento>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse<Departamento>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomResponse<Departamento>>> GetDepartamentoById([FromRoute, Required] Guid id)
    {
        try
        {
            var departamento = await _departamentoRepositorio.GetByIdAsync(id);

            return Ok(new CustomResponse<Departamento>
            {
                Sucesso = true,
                Dados = departamento,
                Mensagem = "Chamado recuperado com sucesso."
            });
        }
        catch (EntidadeNaoEncontradaException ex)
        {
            _logger.LogError(ex, ex.Message);

            return NotFound(new CustomResponse<Departamento>
            {
                Sucesso = false,
                Mensagem = ex.Message
            });
        }
    }

    /// <summary>
    /// Obtém todos os Departamentos.
    /// </summary>
    /// <returns>Retorna uma lista de todos os chamados.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(CustomResponse<IEnumerable<Departamento>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<CustomResponse<IEnumerable<Departamento>>>> GetDepartamentos()
    {
        var departamentos = await _departamentoRepositorio.GetAllAsync();

        return Ok(new CustomResponse<IEnumerable<Departamento>>
        {
            Sucesso = true,
            Dados = departamentos,
            Mensagem = "Departamentos recuperados com sucesso."
        });
    }

    /// <summary>
    /// Cria um novo departamento.
    /// </summary>
    /// <param name="departamentoViewModel">Dados do departamento a ser criado.</param>
    /// <returns>Retorna o departamento criado.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CustomResponse<Departamento>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CustomResponse<Departamento>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CustomResponse<Departamento>>> CriarChamado([FromBody, Required] CriarDepartamentoViewModel departamentoViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                Success = false,
                Message = "Dados informados inválidos.",
                Errors = GetModelStateErros()
            });
        }

        try
        {
            // Mapeia a viewmodel para o domínio
            var departamento = new Departamento(departamentoViewModel.Descricao);

            // Cria o chamado
            await _departamentoRepositorio.AddAsync(departamento);
            await _unitOfWork.CommitAsync();

            return CreatedAtAction(nameof(GetDepartamentoById),
            new { apiVersion = GetApiVersion(), id = departamento.Id },
            new CustomResponse<Departamento>
            {
                Sucesso = true,
                Dados = departamento,
                Mensagem = "Chamado Criado com sucesso."
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            return StatusCode(StatusCodes.Status500InternalServerError,
            new CustomResponse<Departamento>
            {
                Sucesso = false,
                Mensagem = e.Message,
            });
        }
    }

    /// <summary>
    /// Atualiza um departamento existente.
    /// </summary>
    /// <param name="id">ID do departamento a ser atualizado.</param>
    /// <param name="atualizarDepartamentoViewModel">Dados atualizados do departamento.</param>
    /// <returns>Retorna o status da operação.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(CustomResponse<Departamento>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CustomResponse<Departamento>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomResponse<Departamento>>> AtualizarChamado(Guid id, AtualizarDepartamentoViewModel atualizarDepartamentoViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                Success = false,
                Message = "Dados informados inválidos.",
                Errors = GetModelStateErros()
            });
        }

        if (!id.Equals(atualizarDepartamentoViewModel.Id))
        {
            return BadRequest(new CustomResponse<Departamento>
            {
                Sucesso = false,
                Mensagem = "O ID do chamado não corresponde ao ID fornecido."
            });
        }

        try
        {   // Mapeia a viewmodel para o domínio
            var chamado = new Departamento(atualizarDepartamentoViewModel.Id, atualizarDepartamentoViewModel.Descricao);

            // Atualiza o chamado
            await _departamentoRepositorio.UpdateAsync(chamado);
            await _unitOfWork.CommitAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return NotFound(new CustomResponse<Departamento>
            {
                Sucesso = false,
                Mensagem = ex.Message
            });
        }

    }

    /// <summary>
    /// Deleta um chamado pelo ID.
    /// </summary>
    /// <param name="id">ID do chamado a ser deletado.</param>
    /// <returns>Retorna o status da operação.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(CustomResponse<Departamento>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomResponse<Departamento>>> DeletarChamado(Guid id)
    {
        try
        {
            await _departamentoRepositorio.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return NotFound(new CustomResponse<Departamento>
            {
                Sucesso = false,
                Mensagem = ex.Message
            });
        }
    }
}

