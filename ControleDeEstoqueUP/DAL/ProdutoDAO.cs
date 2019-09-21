

using ControleDeEstoqueUP.Data;
using ControleDeEstoqueUP.Models;
using ControleSeuEstoque.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeEstoqueUP.DAL {
    class ProdutoDAO {

        private static ApplicationDbContext database = SingletonContext.GetInstance();

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
                var produto = database.Produtos.FirstOrDefault(p => p.Id == id);
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

        public void AtualizarSaldoDosProdutos(List<Produto> produtos) {
            foreach (var produto in produtos) {
                var compras = database.ProdutosCompra.Where(pc => pc.Produto == produto).Sum(pc => pc.Quantidade);
                var vendas = database.ProdutosVenda.Where(pv => pv.Produto == produto).Sum(pv => pv.Quantidade);
                produto.Quantidade = compras - vendas;
                database.SaveChanges();
            }
        }
        public void AtualizarCustoDosProdutos(List<Produto> produtos) {
            foreach (var produto in produtos) {
                var custoTotal = 0.00;
                var quantidadeComprada = 0.00;

                var compras = database.ProdutosCompra.Where(pc => pc.Produto == produto).ToList();
                foreach (var compra in compras) {
                    custoTotal += compra.Quantidade * Convert.ToDouble(compra.Valor);
                    quantidadeComprada += compra.Quantidade;
                }

                produto.ValorPago = Convert.ToDecimal(custoTotal / quantidadeComprada);
                database.SaveChanges();
            }
        }
    }
}
