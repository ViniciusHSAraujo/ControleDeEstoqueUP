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
    public partial class frmPesquisaProduto : Window {

        ProdutoDAO produtoDAO = new ProdutoDAO();
        private List<Produto> produtos;

        public int produtoId;

        public frmPesquisaProduto() {
            InitializeComponent();
            produtos = produtoDAO.ListarProdutos();
            gridResultados.ItemsSource = produtos.ToList();
        }

        private void BtnLocalizar_Click(object sender, RoutedEventArgs e) {

            switch (ParametroEscolha.SelectedIndex) {
                case 0: //Geral
                    txtPesquisa.Clear();
                    gridResultados.ItemsSource = produtos.ToList();
                    break;
                case 1: //ID
                    gridResultados.ItemsSource = produtos.Where(p => p.Id.ToString().Contains(txtPesquisa.Text)).ToList();
                    break;
                case 2: //NOME
                    gridResultados.ItemsSource = produtos.Where(p => p.Nome.ToLower().Contains((txtPesquisa.Text.ToLower()))).ToList();
                    break;
                case 3: //CATEGORIA
                    gridResultados.ItemsSource = produtos.Where(p => p.Categoria.Nome.ToLower().Contains((txtPesquisa.Text.ToLower()))).ToList();
                    break;
                default:
                    break;
            }
        }

        private void BtnAbrir_Click(object sender, RoutedEventArgs e) {
            if (gridResultados.SelectedItem == null) {
                WPFUtils.MostrarCaixaDeTextoDeErro("Nenhum item selecionado.");
            } else {
                var selecao = gridResultados.SelectedItem;
                Produto pro = selecao as Produto;

                produtoId = pro.Id;
                Close();
            }
        }
    }
}
