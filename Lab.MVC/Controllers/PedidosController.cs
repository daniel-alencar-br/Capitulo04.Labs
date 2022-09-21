using Lab.MVC.Data;
using Lab.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab.MVC.Controllers
{
    public class PedidosController : Controller
    {
        // GET: Pedidos
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Incluir()
        {
            ViewBag.ListaDeClientes = new SelectList(
                ClientesDao.ListarClientes(), "Documento", "Nome");
            return View();
        }

        [HttpPost]
        public ActionResult Incluir(Pedido pedido)
        {
            pedido.Data = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return Incluir();
            }

            try
            {
                PedidosDao.IncluirPedido(pedido);

                ViewBag.ListaDeClientes = new SelectList(
                    ClientesDao.ListarClientes(), "Documento", "Nome");

                return Redirect("/Pedidos/Listar?id=" + pedido.DocCliente);
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }

        public ActionResult Listar(string id)
        {
            try
            {
                ViewBag.ListaDeClientes = new SelectList(
                    ClientesDao.ListarClientes(), "Documento", "Nome");
                return View(PedidosDao.ListarPedidos(id));
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }

        public ActionResult Remover(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception("Nenhum código fornecido");
                }

                var pedido = PedidosDao.BuscarPedido((int)id);
                if (pedido == null)
                {
                    throw new
                        Exception("Nenhum pedido encontrado");
                }
                var doc = pedido.DocCliente;

                PedidosDao.RemoverPedido(pedido);
                return Redirect("/Pedidos/Listar?id=" + doc);
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }
    }
}