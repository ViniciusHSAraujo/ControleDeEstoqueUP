

using ControleDeEstoqueUP.Data;
using ControleDeEstoqueUP.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeEstoqueUP.DAL {
    internal class ProdutoDAO {

        private static readonly ApplicationDbContext database = SingletonContext.GetInstance();

        /**
         * Método que faz a inserção da categoria no banco de dados.
         */
        public Produto Inserir(Produto produto) {
            try {
                database.Produtos.Add(produto);
                database.SaveChanges();
                return produto;
            } catch (Exception e) {
                throw new Exception("Ocorreu um erro ao cadastrar:\n" + e.Message);
            }

        }


        /**
         * Método que recebe uma categoria (editada) e realiza as edições da mesma no banco de dados.
         */
        public void Editar(Produto produto) {
            try {
                Produto produtoBanco = database.Produtos.FirstOrDefault(p => p.Id == produto.Id);
                produtoBanco.Nome = produto.Nome;
                produtoBanco.ValorVenda = produto.ValorVenda;
                produtoBanco.Categoria = produto.Categoria;
                database.SaveChanges();

            } catch (Exception e) {
                throw new Exception("Ocorreu um erro ao cadastrar:\n" + e.Message);
            }

        }

        /**
         * Método que recebe uma categoria (editada) e realiza as edições da mesma no banco de dados.
         */
        public void Excluir(int id) {
            try {
                Produto produto = database.Produtos.FirstOrDefault(p => p.Id == id);
                database.Produtos.Remove(produto);
                database.SaveChanges();

            } catch (Exception e) {
                throw new Exception("Ocorreu um erro ao inativar:\n" + e.Message);
            }

        }

        /**
        * Método que recebe uma categoria (editada) e realiza as edições da mesma no banco de dados.
        */
        public List<Produto> ListarProdutos() {
            return database.Produtos.ToList();
        }

        public Produto BuscarProdutoPeloId(int id) {
            return database.Produtos.FirstOrDefault(p => p.Id == id);
        }

        public double CalcularSaldoDoProduto(Produto produto) {
            int qtde = database.SubProdutos.Where(sp => sp.Produto.Id == produto.Id && sp.Status).Count();
            return qtde;
        }
        public void AtualizarCustoDoProduto(Produto produto) {
            //TODO
            double custoTotal = 0.00;
            double quantidadeComprada = 0.00;

            List<ProdutoCompra> compras = database.ProdutosCompra.Where(pc => pc.Produto.Id == produto.Id).ToList();
            foreach (ProdutoCompra compra in compras) {
                custoTotal += compra.Quantidade * Convert.ToDouble(compra.Valor);
                quantidadeComprada += compra.Quantidade;
            }

            produto.ValorPago = Convert.ToDecimal(custoTotal / quantidadeComprada);
            database.SaveChanges();
        }
    }
}
