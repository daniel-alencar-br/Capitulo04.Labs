using Lab.MVC.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Lab.MVC.Data
{
    public class PedidosDao
    {
        //método para incluir um novo pedido
        public static void IncluirPedido(Pedido pedido)
        {
            using (var ctx = new DbVendasEntities())
            {
                ctx.Pedidos.Add(pedido);
                ctx.SaveChanges();
            }
        }

        //método para listar os pedidos
        public static IEnumerable<Pedido> ListarPedidos(string doc)
        {
            using (var ctx = new DbVendasEntities())
            {
                var lista = ctx.Pedidos.ToList();
                if (!string.IsNullOrEmpty(doc))
                {
                    lista = lista.Where(p => p.DocCliente
                        .Equals(doc)).ToList();
                }
                return lista;
            }
        }

        //método para buscar um pedido
        public static Pedido BuscarPedido(int id)
        {
            using (var ctx = new DbVendasEntities())
            {
                return ctx.Pedidos.FirstOrDefault(p => p.Id == id);
            }
        }

        //método para remover um pedido
        public static void RemoverPedido(Pedido pedido)
        {
            using (var ctx = new DbVendasEntities())
            {
                ctx.Entry<Pedido>(pedido).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }
    }
}