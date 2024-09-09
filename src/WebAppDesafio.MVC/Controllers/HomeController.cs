using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebAppDesafio.MVC.ViewModels;

namespace WebAppDesafio.MVC.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Retorna a view principal da aplicação.
        /// </summary>
        /// <returns>View principal.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Retorna a view da página "Sobre".
        /// </summary>
        /// <returns>View da página "Sobre".</returns>
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        /// <summary>
        /// Retorna a view da página "Contato".
        /// </summary>
        /// <returns>View da página "Contato".</returns>
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Retorna a view da página "Privacidade".
        /// </summary>
        /// <returns>View da página "Privacidade".</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Retorna a view de erro com informações sobre a requisição atual.
        /// </summary>
        /// <returns>View de erro com o modelo de erro.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
