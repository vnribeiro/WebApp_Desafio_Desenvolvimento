using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;
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
                    length = chamados.Dados.Count(),
                    data = chamados
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
                ViewData["ListaDepartamentos"] = await _departamentoClient.GetDepartamentos();
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

                    Departamento = new CriarDepartamentoViewModel
                    {
                        Descricao = chamadoVm.Departamento.Descricao
                    },

                    DataAbertura = chamadoVm.DataAbertura,
                };

                var response = await _chamadoClient.CriarChamado(chamado);

                if (response.Sucesso)
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
        public async Task<IActionResult> Editar([FromRoute] Guid id)
        {
            ViewData["Title"] = "Cadastrar Novo Chamado";

            try
            {
                var response = await _chamadoClient.GetChamado(id);

                ViewData["ListaDepartamentos"] = _departamentoClient.GetDepartamentos();

                return View("Cadastrar", response.Dados);
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
