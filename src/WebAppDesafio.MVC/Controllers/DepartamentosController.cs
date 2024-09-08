using Microsoft.AspNetCore.Mvc;
using WebAppDesafio.MVC.Infra.Clients;
using WebAppDesafio.MVC.ViewModels;

namespace WebAppDesafio.MVC.Controllers
{
    public class DepartamentosController : Controller
    {
        private readonly DepartamentoClient _departamentoClient;

        public DepartamentosController(DepartamentoClient departamentoClient)
        {
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
    }
}
