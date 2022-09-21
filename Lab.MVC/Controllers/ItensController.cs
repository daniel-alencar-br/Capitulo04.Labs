using Lab.MVC.Data;
using Lab.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab.MVC.Controllers
{
    public class ItensController : Controller
    {
        // GET: Itens
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Incluir(int? idPedido) 
        {
            try
            {
                ViewBag.ListaDeProdutos = new SelectList(
                    ProdutosDao.ListarProdutos(), "Id", "Descricao");
                ViewBag.ListaDePedidos = new SelectList(
                    ItensDao.ListarPedidos(), "IdPedido", "NomeCliente");
                ViewBag.ListaDeItens = ItensDao.ListarItensPorPedido(idPedido);

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }

        [HttpPost]
        public ActionResult Incluir(Item item, int? idPedido)
        {
            if (!ModelState.IsValid)
            {
                return Incluir(idPedido);
            }
            try
            {
                item.IdPedido = (int)idPedido;
                ItensDao.IncluirItem(item);
                return RedirectToAction("Incluir", new { 
                    idPedido = (int)idPedido });
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
                if(id == null)
                {
                    throw new Exception("Código não informado");
                }
                var item = ItensDao.BuscarItem((int)id);
                if (item == null)
                {
                    throw new Exception("Item não encontrado");
                }
                int idPedido = item.IdPedido;

                ItensDao.RemoverItem(item);

                return RedirectToAction("Incluir", new
                {
                    idPedido
                });
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }
    }
}