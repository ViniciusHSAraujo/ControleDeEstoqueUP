using ControleDeEstoqueUP.DAL;
using ControleDeEstoqueUP.Models;
using ControleDeEstoqueUP.Utils;
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
    public partial class frmPesquisaProdutoEspecifico : Window {

        ProdutoDAO produtoDAO = new ProdutoDAO();
        SubProdutoDAO subProdutoDAO = new SubProdutoDAO();
        private List<SubProduto> produtosDisponiveis;

        public int subProdutoId;

        public frmPesquisaProdutoEspecifico() {
            InitializeComponent();
            produtosDisponiveis = subProdutoDAO.ListarSubProdutosAtivos();
            gridResultados.ItemsSource = produtosDisponiveis.ToList();
        }

        private void BtnLocalizar_Click(object sender, RoutedEventArgs e) {

            switch (ParametroEscolha.SelectedIndex) {
                case 0: //Geral
                    txtPesquisa.Clear();
                    gridResultados.ItemsSource = produtosDisponiveis.ToList();
                    break;
                case 1: //ID
                    gridResultados.ItemsSource = produtosDisponiveis.Where(p => p.Id.ToString().Contains(txtPesquisa.Text)).ToList();
                    break;
                case 2: //NOME
                    gridResultados.ItemsSource = produtosDisponiveis.Where(p => p.Produto.Nome.ToLower().Contains((txtPesquisa.Text.ToLower()))).ToList();
                    break;
                case 3: //CATEGORIA
                    gridResultados.ItemsSource = produtosDisponiveis.Where(p => p.Produto.Categoria.Nome.ToLower().Contains((txtPesquisa.Text.ToLower()))).ToList();
                    break;
                case 4: //SKU
                    gridResultados.ItemsSource = produtosDisponiveis.Where(p => p.SKU.Contains((txtPesquisa.Text.ToLower()))).ToList();
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
                SubProduto spro = selecao as SubProduto;

                subProdutoId = spro.Id;
                Close();
            }
        }
    }
}
