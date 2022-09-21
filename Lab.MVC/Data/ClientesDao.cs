using Lab.MVC.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Lab.MVC.Data
{
    public class ClientesDao
    {
        //método para incluir um novo cliente
        public static void IncluirCliente(Cliente cliente)
        {
            using(var ctx = new DbVendasEntities())
            {
                ctx.Clientes.Add(cliente);
                ctx.SaveChanges();
            }
        }

        //método para buscar um cliente pelo numero do documento
        public static Cliente BuscarCliente(string documento)
        {
            using (var ctx = new DbVendasEntities())
            {
                return ctx.Clientes.FirstOrDefault(p => 
                    p.Documento.Equals(documento));
            }
        }

        //método para listar todos os clientes
        public static IEnumerable<Cliente> ListarClientes()
        {
            using (var ctx = new DbVendasEntities())
            {
                return ctx.Clientes.ToList();
            }
        }

        //método para alterar um cliente
        public static void AlterarCliente (Cliente cliente)
        {
            using (var ctx = new DbVendasEntities())
            {
                ctx.Entry<Cliente>(cliente).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        //método para remover um cliente
        public static void RemoverCliente(Cliente cliente)
        {
            using (var ctx = new DbVendasEntities())
            {
                ctx.Entry<Cliente>(cliente).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }

    }
}