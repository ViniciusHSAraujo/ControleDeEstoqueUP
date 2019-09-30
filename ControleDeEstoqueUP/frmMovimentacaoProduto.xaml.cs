using ControleDeEstoqueUP.DAL;
using ControleDeEstoqueUP.Models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ControleDeEstoqueUP {
    /// <summary>
    /// Interaction logic for frmBasePesquisa.xaml
    /// </summary>
    public partial class frmMovimentacaoProduto : Window {
        private readonly CompraDAO compraDAO = new CompraDAO();
        private readonly VendaDAO vendaDAO = new VendaDAO();

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
                    List<ProdutoCompra> compras = compraDAO.BuscarComprasDeUmProduto(produto);
                    List<dynamic> comprasGrid = new List<dynamic>();
                    foreach (ProdutoCompra produtoCompra in compras) {
                        comprasGrid.Add(new { ID = produtoCompra.Compra.Id, ProdutoNome = produtoCompra.Produto.Nome, produtoCompra.Quantidade, produtoCompra.Valor, Subtotal = Convert.ToDecimal(produtoCompra.Quantidade) * produtoCompra.Valor });
                    }
                    gridMovimentacao.ItemsSource = comprasGrid;
                    gridMovimentacao.Items.Refresh();
                    break;
                case 2: //VENDAS
                    Janela.Title = "Movimentação de Vendas do Produto";
                    lblHeader.Content = $"Movimentação do produto {produto.Nome} (VENDAS):";
                    headerCodCompraVenda.Header = "Cod. da Venda";
                    List<ProdutoVenda> vendas = vendaDAO.BuscarVendasDeUmProduto(produto);
                    List<dynamic> vendasGrid = new List<dynamic>();
                    foreach (ProdutoVenda produtoVenda in vendas) {
                        vendasGrid.Add(new { ID = produtoVenda.Venda.Id, ProdutoNome = produtoVenda.SubProduto.Produto.Nome, produtoVenda.Valor });
                    }
                    gridMovimentacao.ItemsSource = vendasGrid;
                    gridMovimentacao.Items.Refresh();
                    break;
            }
        }
    }
}
