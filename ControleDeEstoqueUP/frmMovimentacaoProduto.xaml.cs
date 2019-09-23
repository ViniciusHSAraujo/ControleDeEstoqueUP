using ControleDeEstoqueUP.DAL;
using ControleDeEstoqueUP.Models;
using ControleSeuEstoque.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ControleDeEstoqueUP {
    /// <summary>
    /// Interaction logic for frmBasePesquisa.xaml
    /// </summary>
    public partial class frmMovimentacaoProduto : Window {

        //ProdutoCompraDAO produtoCompraDAO = new ProdutoCompraDAO();
        //ProdutoVendaDAO produtoVendaDAO = new ProdutoVendaDAO();

        public frmMovimentacaoProduto() {
            InitializeComponent();
        }

        public frmMovimentacaoProduto(Produto produto, int operacao) {
            InitializeComponent();
            switch (operacao) {
                case 1: //COMPRAS
                    Janela.Title = "Movimentação de Compras do Produto";
                    lblHeader.Content = $"Movimentação do produto {produto.Nome} (COMPRAS):";
                    //gridMovimentacao = produtoCompraDAO.ListarComprasDeUmProduto(produto);
                break;
                case 2: //VENDAS
                    Janela.Title = "Movimentação de Vendas do Produto";
                    lblHeader.Content = $"Movimentação do produto {produto.Nome} (VENDAS):";
                    //gridMovimentacao = produtoVendaDAO.ListarVendasDeUmProduto(produto);
                    break;
            }
        }
    }
}
