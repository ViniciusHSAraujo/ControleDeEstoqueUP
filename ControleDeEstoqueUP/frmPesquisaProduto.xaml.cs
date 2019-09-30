using ControleDeEstoqueUP.DAL;
using ControleDeEstoqueUP.Models;
using ControleDeEstoqueUP.Utils;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;

namespace ControleDeEstoqueUP {
    /// <summary>
    /// Interaction logic for frmBasePesquisa.xaml
    /// </summary>
    public partial class frmPesquisaProduto : Window {
        private readonly ProdutoDAO produtoDAO = new ProdutoDAO();
        private readonly List<Produto> produtos;

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
                object selecao = gridResultados.SelectedItem;
                Produto pro = selecao as Produto;

                produtoId = pro.Id;
                Close();
            }
        }
    }
}
