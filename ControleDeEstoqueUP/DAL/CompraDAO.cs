using System;
using ControleDeEstoqueUP.Data;
using ControleDeEstoqueUP.Models;
using ControleDeEstoqueUP.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeEstoqueUP.DAL {
    class CompraDAO {

        private static ApplicationDbContext database = SingletonContext.GetInstance();


        /*
         * Método que Realiza a inserção do Fornecedor no Banco de Dados
         */
        public Compra Inserir(Compra compra) {
            try {
                database.Compras.Add(compra);
                database.SaveChanges();
                var produtosCompra = compra.ProdutosCompra;
                foreach (var produtoCompra in produtosCompra) {
                    produtoCompra.Compra = compra;
                }
                database.ProdutosCompra.AddRange(produtosCompra);
                database.SaveChanges();

                return compra;
            } catch (Exception e) {
                throw new Exception("Erro ao cadastrar Compra:\n" + e.Message);
            }
        }

        /*
         * Método que realiza a edição do Fornecedor já Cadastrado.
         */
        public void Editar(Compra compra) {
            try {
                Compra compraBanco = database.Compras.FirstOrDefault(c => c.Id == compra.Id);
                compraBanco.Fornecedor = compra.Fornecedor;
                compraBanco.Data = compra.Data;
                database.SaveChanges();
            } catch (Exception e) {
                throw new Exception("Ocorrou um erro ao editar o fornecedor:\n" + e.Message);
            }
        }

        /*
         * Método que irá realizar a Exclusão de um fornecedor já cadastrado no Banco de Dados 
         */

        public void Excluir(int id) {
            try {
                var compra = database.Compras.FirstOrDefault(c => c.Id == id);

                List<ProdutoCompra> produtosDaCompra = database.ProdutosCompra.Where(pc => pc.Compra.Id == compra.Id).ToList();
                database.ProdutosCompra.RemoveRange(produtosDaCompra);
                database.Compras.Remove(compra);
                database.SaveChanges();
            } catch (Exception e) {
                throw new Exception("Erro ao Excluir a Compra:\n" + e.Message);
            }
        }

        /*
         * Método para Listar os Fornecedores Cadastrados no Banco de Dados;
         */

        public List<Compra> ListarCompras() {
            return database.Compras.ToList();
        }

        /*
         * Método que irá Buscar o Fornecedor no Banco de Dados com base no id INFORMADO
         */
        public Compra BuscarCompraPorId(int id) {
            return database.Compras.FirstOrDefault(c => c.Id == id);
        }

        /*
         * Método que irá Buscar as compras de um determinado item no Banco de Dados
         */
        public List<ProdutoCompra> BuscarComprasDeUmProduto(Produto produto) {
            return database.ProdutosCompra.Where(pc => pc.Produto.Id == produto.Id).ToList();
        }

    }
}
