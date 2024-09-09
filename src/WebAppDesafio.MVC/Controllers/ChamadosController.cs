using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
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

        /// <summary>
        /// Redireciona para a ação Listar.
        /// </summary>
        /// <returns>Redirecionamento para a ação Listar.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Listar));
        }

        /// <summary>
        /// Retorna a view para listar chamados.
        /// </summary>
        /// <returns>View para listar chamados.</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            // Busca de dados está na Action Datatable()
            return View();
        }

        /// <summary>
        /// Obtém os dados dos chamados para o DataTable.
        /// </summary>
        /// <returns>Dados dos chamados em formato JSON.</returns>
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

        /// <summary>
        /// Retorna a view para cadastrar um novo chamado.
        /// </summary>
        /// <returns>View para cadastrar um novo chamado.</returns>
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

        /// <summary>
        /// Cadastra um novo chamado.
        /// </summary>
        /// <param name="chamadoVm">Dados do chamado a ser criado.</param>
        /// <returns>Resultado da operação de cadastro.</returns>
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

        /// <summary>
        /// Retorna a view para editar um chamado.
        /// </summary>
        /// <param name="id">ID do chamado a ser editado.</param>
        /// <param name="assunto">Assunto do chamado a ser editado.</param>
        /// <param name="solicitante">Solicitante do chamado a ser editado.</param>
        /// <param name="departamentoId">ID do departamento do chamado a ser editado.</param>
        /// <param name="dataAbertura">Data de abertura do chamado a ser editado.</param>
        /// <returns>View para editar um chamado.</returns>
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

        /// <summary>
        /// Atualiza um chamado existente.
        /// </summary>
        /// <param name="id">ID do chamado a ser atualizado.</param>
        /// <param name="chamadoVm">Dados do chamado a serem atualizados.</param>
        /// <returns>Resultado da operação de atualização.</returns>
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

        /// <summary>
        /// Exclui um chamado existente.
        /// </summary>
        /// <param name="id">ID do chamado a ser excluído.</param>
        /// <returns>Resultado da operação de exclusão.</returns>
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

        /// <summary>
        /// Gera um relatório de chamados em formato PDF.
        /// </summary>
        /// <returns>Arquivo PDF do relatório de chamados.</returns>
        [HttpGet]
        public async Task<IActionResult> Report()
        {
            var extension = 1;
            var contentRootPath = _hostEnvironment.ContentRootPath;
            var path = Path.Combine(contentRootPath, "wwwroot", "reports", "rptChamados.rdlc");

            var localReport = new LocalReport();
            await using (var reportStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                localReport.LoadReportDefinition(reportStream);
            }

            // Carrega os dados que serão apresentados no relatório
            var response = await _chamadoClient.GetChamados();
            localReport.DataSources.Add(new ReportDataSource("dsChamados", response.Dados));

            // Renderiza o relatório em PDF
            var reportResult = localReport.Render("PDF");

            //return File(reportResult.MainStream, "application/pdf");
            return File(reportResult, "application/octet-stream", "rptChamados.pdf");
        }
    }
}
