using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using People.Web.ViewModels;

namespace People.Web.Controllers
{
    /// <summary>
    /// Controller for some default actions and views.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// GET index view.
        /// </summary>
        /// <returns>Redirect.</returns>
        public IActionResult Index()
        {
            return RedirectToAction("List", "User");
        }

        /// <summary>
        /// Show a page with error message to user.
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
