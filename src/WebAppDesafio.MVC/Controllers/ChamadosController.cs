﻿using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;
using Shared.ViewModels.Atualizar;
using Shared.ViewModels.Criar;
using WebAppDesafio.MVC.Infra.Clients;
using WebAppDesafio.MVC.ViewModels;
using WebAppDesafio.MVC.ViewModels.Enums;

namespace WebAppDesafio.MVC.Controllers
{
    public class ChamadosController : Controller
    {
        private readonly IHostEnvironment? _hostEnvironment;
        private readonly ChamadoClient _chamadoClient;
        private readonly DepartamentoClient _departamentoClient;

        public ChamadosController(IHostEnvironment? hostEnvironment, 
            ChamadoClient chamadoClient, 
            DepartamentoClient departamentoClient)
        {
            _hostEnvironment = hostEnvironment;
            _chamadoClient = chamadoClient;
            _departamentoClient = departamentoClient;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Listar));
        }

        [HttpGet]
        public IActionResult Listar()
        {
            // Busca de dados está na Action Datatable()
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Datatable()
        {
            try
            {
                var chamados = await _chamadoClient.GetChamados();

                var dataTableVm = new DataTableAjaxViewModel()
                {
                    Length = chamados.Dados.Count(),
                    Data = chamados.Dados
                };

                return Ok(dataTableVm);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {
            var chamadoVm = new ChamadoViewModel()
            {
                DataAbertura = DateTime.Now
            };

            ViewData["Title"] = "Cadastrar Novo Chamado";

            try
            {
                var response = await _departamentoClient.GetDepartamentos();

                if (!response.Sucesso)
                {
                    ViewData["Error"] = "Erro ao obter os departamentos";
                }

                ViewData["ListaDepartamentos"] = response.Dados;

            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
            }

            return View("Cadastrar", chamadoVm);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(ChamadoViewModel chamadoVm)
        {
            try
            {
                var chamado = new CriarChamadoViewModel
                {
                    Solicitante = chamadoVm.Solicitante,
                    Assunto = chamadoVm.Assunto,
                    IdDepartamento = chamadoVm.Departamento.Id,
                    DataAbertura = chamadoVm.DataAbertura,
                };

                var response = await _chamadoClient.CriarChamado(chamado);

                if (response.Sucesso)
                {
                    return Ok(new ResponseViewModel
                    {
                        Type = AlertTypes.success,
                        Message = "Chamado gravado com sucesso!",
                        Controller = RouteData.Values["controller"].ToString(),
                        Action = nameof(this.Listar)
                    });
                }

                throw new ApplicationException($"Falha ao cadastrar o Chamado.");
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Editar([FromQuery] Guid id, 
            [FromQuery] string assunto, 
            [FromQuery] string solicitante, 
            [FromQuery] Guid departamentoId, 
            [FromQuery] DateTime dataAbertura)
        {
            ViewData["Title"] = "Atualizar Chamado";

            try
            {
                var chamadoViewModel = new ChamadoViewModel
                {
                    Id = id,
                    Assunto = assunto,
                    Solicitante = solicitante,
                    Departamento = new DepartamentoViewModel
                    {
                        Id = departamentoId
                    },
                    DataAbertura = dataAbertura
                };

                var response = await _departamentoClient.GetDepartamentos();

                if (!response.Sucesso)
                {
                    ViewData["Error"] = "Erro ao obter os departamentos";
                }

                ViewData["ListaDepartamentos"] = response.Dados;

                return View("Editar", chamadoViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Atualizar([FromRoute] Guid id, [FromBody] AtualizarChamadoViewModel chamadoVm)
        {
            try
            {
                var response = await _chamadoClient.AtualizarChamado(id, chamadoVm);

                if (response.Sucesso)
                {
                    return Ok(new ResponseViewModel
                    {
                        Type = AlertTypes.success,
                        Message = "Chamado atualizado com sucesso!",
                        Controller = RouteData.Values["controller"].ToString(),
                        Action = nameof(this.Listar)
                    });
                }

                throw new ApplicationException($"Falha ao Atualizar o Chamado.");
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Excluir([FromRoute] Guid id)
        {
            try
            {
                var response = await _chamadoClient.ExcluirChamado(id);

                if (response.Sucesso)
                {
                    return Ok(new ResponseViewModel
                    {
                        Type = AlertTypes.success,
                        Message = "Chamado excluido com sucesso!",
                        Controller = RouteData.Values["controller"].ToString(),
                        Action = nameof(this.Listar)
                    });
                }

                throw new ApplicationException($"Falha ao excluir o Chamado.");
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Report()
        {
            var mimeType = string.Empty;
            var extension = 1;
            var contentRootPath = _hostEnvironment.ContentRootPath;
            var path = Path.Combine(contentRootPath, "wwwroot", "reports", "rptChamados.rdlc");
            //
            // ... parameters
            //
            var localReport = new LocalReport(path);

            // Carrega os dados que serão apresentados no relatório
            var response = await _chamadoClient.GetChamados();

            localReport.AddDataSource("dsChamados", response.Dados);

            // Renderiza o relatório em PDF
            ReportResult reportResult = localReport.Execute(RenderType.Pdf);

            //return File(reportResult.MainStream, "application/pdf");
            return File(reportResult.MainStream, "application/octet-stream", "rptChamados.pdf");
        }
    }
}
