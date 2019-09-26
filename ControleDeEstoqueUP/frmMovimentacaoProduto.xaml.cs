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

        CompraDAO compraDAO = new CompraDAO();
        VendaDAO vendaDAO = new VendaDAO();

        public frmMovimentacaoProduto() {
            InitializeComponent();
        }

        public frmMovimentacaoProduto(Produto produto, int operacao) {
            InitializeComponent();
            switch (operacao) {
                case 1: //COMPRAS
                    Janela.Title = "Movimentação de Compras do Produto";
                    lblHeader.Content = $"Movimentação do produto {produto.Nome} (COMPRAS):";
                    headerCodCompraVenda.Header = "Cod. da Compra";
                    var compras = compraDAO.BuscarComprasDeUmProduto(produto);
                    List<dynamic> comprasGrid = new List<dynamic>();
                    foreach (var produtoCompra in compras) {
                        comprasGrid.Add(new { ID = produtoCompra.Compra.Id, ProdutoNome = produtoCompra.Produto.Nome, produtoCompra.Quantidade, produtoCompra.Valor, Subtotal = Convert.ToDecimal(produtoCompra.Quantidade) * produtoCompra.Valor });
                    }
                    gridMovimentacao.ItemsSource = comprasGrid;
                    gridMovimentacao.Items.Refresh();
                break;
                case 2: //VENDAS
                    Janela.Title = "Movimentação de Vendas do Produto";
                    lblHeader.Content = $"Movimentação do produto {produto.Nome} (VENDAS):";
                    headerCodCompraVenda.Header = "Cod. da Venda";
                    var vendas = vendaDAO.BuscarVendasDeUmProduto(produto);
                    List<dynamic> vendasGrid = new List<dynamic>();
                    foreach (var produtoVenda in vendas) {
                        vendasGrid.Add(new { ID = produtoVenda.Venda.Id, ProdutoNome = produtoVenda.Produto.Nome, produtoVenda.Quantidade, produtoVenda.Valor, Subtotal = Convert.ToDecimal(produtoVenda.Quantidade) * produtoVenda.Valor });
                    }
                    gridMovimentacao.ItemsSource = vendasGrid;
                    gridMovimentacao.Items.Refresh();
                    break;
            }
        }
    }
}
