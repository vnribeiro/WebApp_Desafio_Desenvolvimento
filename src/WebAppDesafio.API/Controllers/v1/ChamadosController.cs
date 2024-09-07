using System.ComponentModel.DataAnnotations;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Shared.ViewModels;
using Shared.ViewModels.Atualizar;
using Shared.ViewModels.Criar;
using WebAppDesafio.API.Dominio.Models;
using WebAppDesafio.API.Infra.Dados;
using WebAppDesafio.API.Infra.Exceptions;
using WebAppDesafio.API.Infra.Repositorios.Interfaces;

namespace WebAppDesafio.API.Controllers.v1;

[ApiController]
[ApiVersion(1.0)]
[Route("api/v{apiVersion:apiVersion}/chamados")]
public class ChamadosController : MainController
{
    private readonly IChamadoRepositorio _chamadoRepositorio;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ChamadosController> _logger;

    public ChamadosController(IChamadoRepositorio chamadoRepositorio, 
        IUnitOfWork unitOfWork,
        ILogger<ChamadosController> logger)
    {
        _chamadoRepositorio = chamadoRepositorio;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <summary>
    /// Obtém um chamado pelo ID.
    /// </summary>
    /// <param name="id">ID do chamado.</param>
    /// <returns>Retorna o chamado correspondente ao ID fornecido.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CustomResponse<Chamado>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse<Chamado>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomResponse<Chamado>>> GetChamadoById([FromRoute, Required] Guid id)
    {
        try
        {
            var chamado = await _chamadoRepositorio.GetByIdAsync(id);

            return Ok(new CustomResponse<Chamado>
            {
                Sucesso = true,
                Dados = chamado,
                Mensagem = "Chamado recuperado com sucesso."
            });
        }
        catch (EntidadeNaoEncontradaException ex)
        {
            _logger.LogError(ex, ex.Message);

            return NotFound(new CustomResponse<Chamado>
            {
                Sucesso = false,
                Mensagem = ex.Message
            });
        }
    }

    /// <summary>
    /// Obtém todos os chamados.
    /// </summary>
    /// <returns>Retorna uma lista de todos os chamados.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(CustomResponse<IEnumerable<Chamado>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<CustomResponse<IEnumerable<Chamado>>>> GetChamados()
    {
        var chamados = await _chamadoRepositorio.GetAllAsync();

        return Ok(new CustomResponse<IEnumerable<Chamado>>
        {
            Sucesso = true,
            Dados = chamados,
            Mensagem = "Chamados recuperados com sucesso."
        });
    }

    /// <summary>
    /// Cria um novo chamado.
    /// </summary>
    /// <param name="chamadoViewModel">Dados do chamado a ser criado.</param>
    /// <returns>Retorna o chamado criado.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CustomResponse<Chamado>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CustomResponse<Chamado>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CustomResponse<Chamado>>> CriarChamado([FromBody, Required] CriarChamadoViewModel chamadoViewModel)
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
            var chamado = MapCriaChamado(chamadoViewModel);

            // Cria o chamado
            await _chamadoRepositorio.AddAsync(chamado);
            await _unitOfWork.CommitAsync();

            return CreatedAtAction(nameof(GetChamadoById),
            new { apiVersion = GetApiVersion(), id = chamado.Id },
            new CustomResponse<Chamado>
            {
                Sucesso = true,
                Dados = chamado,
                Mensagem = "Chamado Criado com sucesso."
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            return StatusCode(StatusCodes.Status500InternalServerError,
            new CustomResponse<Chamado>
            {
                Sucesso = false,
                Mensagem = e.Message,
            });
        }
    }

    /// <summary>
    /// Atualiza um chamado existente.
    /// </summary>
    /// <param name="id">ID do chamado a ser atualizado.</param>
    /// <param name="chamadoViewModel">Dados atualizados do chamado.</param>
    /// <returns>Retorna o status da operação.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(CustomResponse<Chamado>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CustomResponse<Chamado>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomResponse<Chamado>>> AtualizarChamado(Guid id, AtualizarChamadoViewModel chamadoViewModel)
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

        if (!id.Equals(chamadoViewModel.Id))
        {
            return BadRequest(new CustomResponse<Chamado>
            {
                Sucesso = false,
                Mensagem = "O ID do chamado não corresponde ao ID fornecido."
            });
        }

        try
        {   // Mapeia a viewmodel para o domínio
            var chamado = MapAtualizarChamado(chamadoViewModel);

            // Atualiza o chamado
            await _chamadoRepositorio.UpdateAsync(chamado);
            await _unitOfWork.CommitAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return NotFound(new CustomResponse<Chamado>
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
    [ProducesResponseType(typeof(CustomResponse<Chamado>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomResponse<Chamado>>> DeletarChamado(Guid id)
    {
        try
        {
            await _chamadoRepositorio.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return NotFound(new CustomResponse<Chamado>
            {
                Sucesso = false,
                Mensagem = ex.Message
            });
        }
    }

    private static Chamado MapCriaChamado(CriarChamadoViewModel chamadoViewModel)
    {
        var departamento = new Departamento(chamadoViewModel.Departamento.Descricao);
        var chamado = new Chamado(chamadoViewModel.Assunto, chamadoViewModel.Solicitante, departamento, chamadoViewModel.DataAbertura);
        return chamado;
    }

    private static Chamado MapAtualizarChamado(AtualizarChamadoViewModel chamadoViewModel)
    {
        var departamento = new Departamento(chamadoViewModel.Departamento.Id, chamadoViewModel.Departamento.Descricao);
        var chamado = new Chamado(chamadoViewModel.Id, chamadoViewModel.Assunto, chamadoViewModel.Solicitante, departamento, chamadoViewModel.DataAbertura);
        return chamado;
    }
}

