

using ControleDeEstoqueUP.Data;
using ControleDeEstoqueUP.Models;
using ControleSeuEstoque.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeEstoqueUP.DAL {
    class SubProdutoDAO {

        private static ApplicationDbContext database = SingletonContext.GetInstance();

        /**
         * Método que faz a inserção da categoria no banco de dados.
         */
        public SubProduto Inserir(SubProduto subProduto) {
            try {
                database.SubProdutos.Add(subProduto);
                database.SaveChanges();
                return subProduto;
            } catch (Exception e) {
                throw new Exception("Ocorreu um erro ao cadastrar:\n" + e.Message);
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
            var compras = database.ProdutosCompra.Where(pc => pc.Produto.Id == produto.Id).Select(pc => pc.Quantidade).DefaultIfEmpty(0).Sum();
            var vendas = database.ProdutosVenda.Where(pv => pv.Produto.Id == produto.Id).Select(pv => pv.Quantidade).DefaultIfEmpty(0).Sum();
            return compras - vendas;
        }
        public void AtualizarCustoDoProduto(Produto produto) {
                //TODO
                var custoTotal = 0.00;
                var quantidadeComprada = 0.00;

                var compras = database.ProdutosCompra.Where(pc => pc.Produto.Id == produto.Id).ToList();
                foreach (var compra in compras) {
                    custoTotal += compra.Quantidade * Convert.ToDouble(compra.Valor);
                    quantidadeComprada += compra.Quantidade;
                }

                produto.ValorPago = Convert.ToDecimal(custoTotal / quantidadeComprada);
                database.SaveChanges();
        }
    }
}
