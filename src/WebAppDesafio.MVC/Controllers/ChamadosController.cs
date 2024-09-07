﻿using Microsoft.AspNetCore.Mvc;
using WebAppDesafio.MVC.ViewModels;
using WebAppDesafio.MVC.ViewModels.Enums;

namespace WebAppDesafio.MVC.Controllers
{
    public class ChamadosController : Controller
    {

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
        public IActionResult Datatable()
        {
            try
            {
                var chamadosApiClient = new ChamadosApiClient();
                var lstChamados = chamadosApiClient.ChamadosListar();

                var dataTableVM = new DataTableAjaxViewModel()
                {
                    length = lstChamados.Count,
                    data = lstChamados
                };

                return Ok(dataTableVM);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            var chamadoVM = new ChamadoViewModel()
            {
                DataAbertura = DateTime.Now
            };
            ViewData["Title"] = "Cadastrar Novo Chamado";

            try
            {
                var departamentosApiClient = new DepartamentosApiClient();

                ViewData["ListaDepartamentos"] = departamentosApiClient.DepartamentosListar();
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
            }

            return View("Cadastrar", chamadoVM);
        }

        [HttpPost]
        public IActionResult Cadastrar(ChamadoViewModel chamadoVM)
        {
            try
            {
                var chamadosApiClient = new ChamadosApiClient();
                var realizadoComSucesso = chamadosApiClient.ChamadoGravar(chamadoVM);

                if (realizadoComSucesso)
                    return Ok(new ResponseViewModel(
                                $"Chamado gravado com sucesso!",
                                AlertTypes.success,
                                this.RouteData.Values["controller"].ToString(),
                                nameof(this.Listar)));
                else
                    throw new ApplicationException($"Falha ao excluir o Chamado.");
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        [HttpGet]
        public IActionResult Editar([FromRoute] int id)
        {
            ViewData["Title"] = "Cadastrar Novo Chamado";

            try
            {
                var chamadosApiClient = new ChamadosApiClient();
                var chamadoVM = chamadosApiClient.ChamadoObter(id);

                var departamentosApiClient = new DepartamentosApiClient();
                ViewData["ListaDepartamentos"] = departamentosApiClient.DepartamentosListar();

                return View("Cadastrar", chamadoVM);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        [HttpDelete]
        public IActionResult Excluir([FromRoute] int id)
        {
            try
            {
                var chamadosApiClient = new ChamadosApiClient();
                var realizadoComSucesso = chamadosApiClient.ChamadoExcluir(id);

                if (realizadoComSucesso)
                    return Ok(new ResponseViewModel(
                                $"Chamado {id} excluído com sucesso!",
                                AlertTypes.success,
                                "Chamados",
                                nameof(Listar)));
                else
                    throw new ApplicationException($"Falha ao excluir o Chamado {id}.");
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        [HttpGet]
        public IActionResult Report()
        {
            string mimeType = string.Empty;
            int extension = 1;
            string contentRootPath = _hostEnvironment.ContentRootPath;
            string path = Path.Combine(contentRootPath, "wwwroot", "reports", "rptChamados.rdlc");
            //
            // ... parameters
            //
            LocalReport localReport = new LocalReport(path);

            // Carrega os dados que serão apresentados no relatório
            var chamadosApiClient = new ChamadosApiClient();
            var lstChamados = chamadosApiClient.ChamadosListar();

            localReport.AddDataSource("dsChamados", lstChamados);

            // Renderiza o relatório em PDF
            ReportResult reportResult = localReport.Execute(RenderType.Pdf);

            //return File(reportResult.MainStream, "application/pdf");
            return File(reportResult.MainStream, "application/octet-stream", "rptChamados.pdf");
        }

    }
}
