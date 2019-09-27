
using System;
using ControleDeEstoqueUP.Data;
using ControleDeEstoqueUP.Models;
using ControleDeEstoqueUP.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeEstoqueUP.DAL {
    class VendaDAO {

        private static ApplicationDbContext database = SingletonContext.GetInstance();

        /*
         * Método que Realiza a inserção da venda no Banco de Dados
         */
        public Venda Inserir(Venda venda) {
            try {
                var produtosVenda = venda.ProdutosVenda;
                foreach (var produtoVenda in produtosVenda) {
                    var prod = database.SubProdutos.Find(produtoVenda.SubProduto.Id);
                    prod.Status = false;
                    database.SaveChanges();
                }
                database.Vendas.Add(venda);
                database.SaveChanges();

                return venda;
            } catch (Exception e) {
                throw new Exception("Erro ao cadastrar venda:\n" + e.Message);
            }
        }

        /*
         * Método que realiza a edição do Fornecedor já Cadastrado.
         */
        public void Editar(Venda venda) {
            try {
                Venda vendaBanco = database.Vendas.FirstOrDefault(v => v.Id == venda.Id);
                vendaBanco.Cliente = venda.Cliente;
                vendaBanco.Data = venda.Data;
                database.SaveChanges();
            } catch (Exception e) {
                throw new Exception("Ocorrou um erro ao editar a venda:\n" + e.Message);
            }
        }

        /*
         * Método que irá realizar a Exclusão de um fornecedor já cadastrado no Banco de Dados 
         */

        public void Excluir(int id) {
            try {
                var venda = database.Vendas.FirstOrDefault(v => v.Id == id);

                List<ProdutoVenda> produtosDaVenda = database.ProdutosVenda.Where(pv => pv.Venda.Id == venda.Id).ToList();
                database.ProdutosVenda.RemoveRange(produtosDaVenda);
                database.Vendas.Remove(venda);
                database.SaveChanges();
            } catch (Exception e) {
                throw new Exception("Erro ao Excluir a venda:\n" + e.Message);
            }
        }

        /*
         * Método para Listar os Fornecedores Cadastrados no Banco de Dados;
         */

        public List<Venda> ListarVendas() {
            return database.Vendas.ToList();
        }

        /*
         * Método que irá Buscar o Fornecedor no Banco de Dados com base no id INFORMADO
         */
        public Venda BuscarVendaPorId(int id) {
            return database.Vendas.FirstOrDefault(v => v.Id == id);
        }

        /*
         * Método que irá Buscar as compras de um determinado item no Banco de Dados
         */
        public List<ProdutoVenda> BuscarVendasDeUmProduto(Produto produto) {
            return database.ProdutosVenda.Where(pv => pv.SubProduto.Produto.Id == produto.Id).ToList();
        }

    }
}
