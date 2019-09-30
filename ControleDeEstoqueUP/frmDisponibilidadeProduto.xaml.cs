using ControleDeEstoqueUP.DAL;
using ControleDeEstoqueUP.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;

namespace ControleDeEstoqueUP {
    /// <summary>
    /// Interaction logic for frmBasePesquisa.xaml
    /// </summary>
    public partial class frmDisponibilidadeProduto : Window {
        private readonly SubProdutoDAO subProdutoDAO = new SubProdutoDAO();

        public frmDisponibilidadeProduto() {
            InitializeComponent();
        }

        public frmDisponibilidadeProduto(Produto produto) {
            InitializeComponent();

            lblHeader.Content = $"Situação do produto {produto.Nome} no estoque:";

            List<SubProduto> produtos = subProdutoDAO.ListarSubProdutos().Where(p => p.Produto.Id == produto.Id).ToList();
            List<dynamic> comprasGrid = new List<dynamic>();
            foreach (SubProduto prod in produtos) {
                comprasGrid.Add(new { ProdID = prod.Produto.Id, SubProdID = prod.Id, ProdutoNome = prod.Produto.Nome, prod.SKU, Status = prod.StatusString });
            }
            gridMovimentacao.ItemsSource = comprasGrid;
            gridMovimentacao.Items.Refresh();
        }
    }
}

