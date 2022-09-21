using Lab.MVC.Models;
using Lab.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lab.MVC.Data
{
    public class ItensDao
    {
        public static void IncluirItem(Item item)
        {
            using(var ctx = new DbVendasEntities())
            {
                ctx.Entry<Item>(item).State = EntityState.Added;
                ctx.SaveChanges();
            }
        }

        public static void RemoverItem(Item item)
        {
            using (var ctx = new DbVendasEntities())
            {
                ctx.Entry<Item>(item).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }

        public static Item BuscarItem(int id)
        {
            using (var ctx = new DbVendasEntities())
            {
                return ctx.Itens.FirstOrDefault(p => p.Id == id);
            }
        }

        public static IEnumerable<ClientePedidoViewModel> ListarPedidos()
        {
            using(var ctx = new DbVendasEntities())
            {
                var lista = ctx.Clientes.Join(
                    ctx.Pedidos,
                    c => c.Documento,
                    p => p.DocCliente,
                    (c, p) => new ClientePedidoViewModel
                    {
                        Documento = c.Documento,
                        NomeCliente = c.Nome + " - " + p.NumeroPedido,
                        IdPedido = p.Id,
                        NumeroPedido = p.NumeroPedido
                    });

                return lista.ToList();
            }
        }

        public static IEnumerable<ItensPedidoViewModel> ListarItensPorPedido(
            int? idPedido)
        {
            List<ItensPedidoViewModel> lista = new List<ItensPedidoViewModel>();
            if (idPedido != null)
            {
                using (var ctx = new DbVendasEntities())
                {
                    lista = (from pedido in ctx.Pedidos
                             join item in ctx.Itens
                             on pedido.Id equals item.IdPedido
                             join produto in ctx.Produtos
                             on item.IdProduto equals produto.Id
                             where pedido.Id == (int)idPedido
                             select new ItensPedidoViewModel
                             {
                                 IdItem = item.Id,
                                 QuantItens = item.Quantidade,
                                 IdPedido = pedido.Id,
                                 NumeroPedido = pedido.NumeroPedido,
                                 DescProduto = produto.Descricao,
                                 TotalItem = item.Quantidade *
                                         (double)produto.Preco
                             }).ToList();
                }
            }
            return lista;
        }
    }
}