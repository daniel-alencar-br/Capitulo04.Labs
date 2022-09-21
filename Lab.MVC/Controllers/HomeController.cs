using System.Web.Mvc;

namespace Lab.MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MostrarErro()
        {
            ViewBag.MensagemErro = "Erro interno do servidor";
            return View("_Erro");
        }
    }
}