using ControleDeEstoqueUP.DAL;
using ControleDeEstoqueUP.Models;
using ControleDeEstoqueUP.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
    public partial class frmDisponibilidadeProduto : Window {

        SubProdutoDAO subProdutoDAO = new SubProdutoDAO();

        public frmDisponibilidadeProduto() {
            InitializeComponent();
        }

        public frmDisponibilidadeProduto(Produto produto) {
            InitializeComponent();

            lblHeader.Content = $"Situação do produto {produto.Nome} no estoque:";

            var produtos = subProdutoDAO.ListarSubProdutos().Where(p => p.Produto.Id == produto.Id).ToList();
            List<dynamic> comprasGrid = new List<dynamic>();
            foreach (var prod in produtos) {
                comprasGrid.Add(new { ProdID = prod.Produto.Id, SubProdID = prod.Id, ProdutoNome = prod.Produto.Nome, prod.SKU, Status = prod.StatusString });
            }
            gridMovimentacao.ItemsSource = comprasGrid;
            gridMovimentacao.Items.Refresh();
        }
    }
}

