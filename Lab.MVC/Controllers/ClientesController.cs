using Lab.MVC.Data;
using Lab.MVC.Models;
using System;
using System.Web.Mvc;

namespace Lab.MVC.Controllers
{
    public class ClientesController : Controller
    {
        // GET: Clientes
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Incluir(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                ClientesDao.IncluirCliente(cliente);
                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }

        public ActionResult Listar()
        {
            try
            {
                return View(ClientesDao.ListarClientes());
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }

        private ActionResult Buscar(string id, string viewName)
        {
            try
            {
                if(id == null)
                {
                    throw new Exception("O documento não foi informado corretamente");
                }
                var cliente = ClientesDao.BuscarCliente(id);
                if(cliente == null)
                {
                    throw new Exception("Nenhum cliente encontrrado");
                }
                return View(viewName, cliente);
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }

        public ActionResult Alterar(string id)
        {
            return Buscar(id, "Alterar");
        }
        public ActionResult Detalhes(string id)
        {
            return Buscar(id, "Detalhes");
        }
        public ActionResult Remover(string id)
        {
            return Buscar(id, "Remover");
        }

        [HttpPost]
        public ActionResult Alterar(Cliente cliente)
        {
            try
            {
                ClientesDao.AlterarCliente(cliente);
                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }

        [HttpPost]
        public ActionResult Remover(Cliente cliente)
        {
            try
            {
                ClientesDao.RemoverCliente(cliente);
                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }

    }
}