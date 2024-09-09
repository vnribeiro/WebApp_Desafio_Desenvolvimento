using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using Shared.ViewModels.Criar;
using WebAppDesafio.MVC.Infra.Clients;
using WebAppDesafio.MVC.ViewModels;
using WebAppDesafio.MVC.ViewModels.Enums;

namespace WebAppDesafio.MVC.Controllers
{
    public class DepartamentosController : Controller
    {
        private readonly DepartamentoClient _departamentoClient;
        private readonly IHostEnvironment? _hostEnvironment;

        public DepartamentosController(DepartamentoClient departamentoClient, 
            IHostEnvironment? hostEnvironment)
        {
            _departamentoClient = departamentoClient;
            _hostEnvironment = hostEnvironment;
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
        /// Retorna a view para listar departamentos.
        /// </summary>
        /// <returns>View para listar departamentos.</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            // Busca de dados está na Action Datatable()
            return View();
        }

        /// <summary>
        /// Obtém os dados dos departamentos para o DataTable.
        /// </summary>
        /// <returns>Dados dos departamentos em formato JSON.</returns>
        [HttpGet]
        public async Task<IActionResult> Datatable()
        {
            try
            {
                var response = await _departamentoClient.GetDepartamentos();

                var dataTableVm = new DataTableAjaxViewModel()
                {
                    Length = response.Dados.Count(),
                    Data = response.Dados
                };

                return Ok(dataTableVm);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        /// <summary>
        /// Retorna a view para cadastrar um novo departamento.
        /// </summary>
        /// <returns>View para cadastrar um novo departamento.</returns>
        [HttpGet]
        public IActionResult Cadastrar()
        {
            ViewData["Title"] = "Cadastrar Novo departamento";

            return View("Cadastrar");
        }

        /// <summary>
        /// Cadastra um novo departamento.
        /// </summary>
        /// <param name="departamentoVm">Dados do departamento a ser criado.</param>
        /// <returns>Resultado da operação de cadastro.</returns>
        [HttpPost]
        public async Task<IActionResult> Cadastrar(CriarDepartamentoViewModel departamentoVm)
        {
            try
            {
                var response = await _departamentoClient.CriarDepartamento(departamentoVm);

                if (response.Sucesso)
                {
                    return Ok(new ResponseViewModel
                    {
                        Type = AlertTypes.success,
                        Message = "Departamento criado com sucesso!",
                        Controller = RouteData.Values["controller"]!.ToString()!,
                        Action = nameof(this.Listar)
                    });
                }

                throw new ApplicationException($"Falha ao cadastrar o departamento.");
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        /// <summary>
        /// Retorna a view para editar um departamento.
        /// </summary>
        /// <param name="id">ID do departamento a ser editado.</param>
        /// <param name="descricao">Descrição do departamento a ser editado.</param>
        /// <returns>View para editar um departamento.</returns>
        [HttpGet]
        public IActionResult Editar([FromQuery] Guid id, [FromQuery] string descricao)
        {
            ViewData["Title"] = "Atualizar Departamento";

            try
            {
                var departamentoViewModel = new DepartamentoViewModel
                {
                    Id = id,
                    Descricao = descricao
                };

                return View("Editar", departamentoViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        /// <summary>
        /// Atualiza um departamento existente.
        /// </summary>
        /// <param name="id">ID do departamento a ser atualizado.</param>
        /// <param name="departamentoVm">Dados do departamento a serem atualizados.</param>
        /// <returns>Resultado da operação de atualização.</returns>
        [HttpPatch]
        public async Task<IActionResult> Atualizar([FromRoute] Guid id, [FromBody] DepartamentoViewModel departamentoVm)
        {
            try
            {
                var response = await _departamentoClient.AtualizarDepartamento(id, departamentoVm);

                if (response.Sucesso)
                {
                    return Ok(new ResponseViewModel
                    {
                        Type = AlertTypes.success,
                        Message = "Chamado atualizado com sucesso!",
                        Controller = RouteData.Values["controller"]!.ToString()!,
                        Action = nameof(this.Listar)
                    });
                }

                throw new ApplicationException($"Falha ao Atualizar o departamento.");
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        /// <summary>
        /// Exclui um departamento existente.
        /// </summary>
        /// <param name="id">ID do departamento a ser excluído.</param>
        /// <returns>Resultado da operação de exclusão.</returns>
        [HttpDelete]
        public async Task<IActionResult> Excluir([FromRoute] Guid id)
        {
            try
            {
                var response = await _departamentoClient.ExcluirDepartamento(id);

                if (response.Sucesso)
                {
                    return Ok(new ResponseViewModel
                    {
                        Type = AlertTypes.success,
                        Message = "Chamado excluido com sucesso!",
                        Controller = RouteData.Values["controller"]?.ToString()!,
                        Action = nameof(this.Listar)
                    });
                }

                throw new ApplicationException($"Falha ao excluir o departamento.");
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        /// <summary>
        /// Gera um relatório de departamentos em formato PDF.
        /// </summary>
        /// <returns>Arquivo PDF do relatório de departamentos.</returns>
        [HttpGet]
        public async Task<IActionResult> Report()
        {
            var contentRootPath = _hostEnvironment!.ContentRootPath;
            var path = Path.Combine(contentRootPath, "wwwroot", "reports", "rptDepartamentos.rdlc");

            var localReport = new LocalReport();
            await using (var reportStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                localReport.LoadReportDefinition(reportStream);
            }

            // Carrega os dados que serão apresentados no relatório
            var response = await _departamentoClient.GetDepartamentos();
            localReport.DataSources.Add(new ReportDataSource("dsDepartamentos", response.Dados));

            // Renderiza o relatório em PDF
            var reportResult = localReport.Render("PDF");

            //return File(reportResult.MainStream, "application/pdf");
            return File(reportResult, "application/octet-stream", "rptDepartamentos.pdf");
        }
    }
}
