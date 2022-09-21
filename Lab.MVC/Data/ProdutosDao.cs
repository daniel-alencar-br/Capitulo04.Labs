using Lab.MVC.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Lab.MVC.Data
{
    public class ProdutosDao
    {
        //método para incluir um novo produto
        public static void IncluirProduto(Produto produto)
        {
            using (var ctx = new DbVendasEntities())
            {
                ctx.Produtos.Add(produto);
                ctx.SaveChanges();
            }
        }

        //método para buscar um produto pelo id
        public static Produto BuscarProduto(int id)
        {
            using (var ctx = new DbVendasEntities())
            {
                return ctx.Produtos.FirstOrDefault(p =>
                    p.Id == id);
            }
        }

        //método para listar todos os produtos
        public static IEnumerable<Produto> ListarProdutos()
        {
            using (var ctx = new DbVendasEntities())
            {
                return ctx.Produtos.ToList();
            }
        }

        //método para alterar um produto
        public static void AlterarProduto(Produto produto)
        {
            using (var ctx = new DbVendasEntities())
            {
                ctx.Entry<Produto>(produto).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        //método para listar todas as categorias
        public static IEnumerable<Categoria> ListarCategorias()
        {
            using (var ctx = new DbVendasEntities())
            {
                return ctx.Categorias.ToList();
            }
        }
    }
}