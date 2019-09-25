using ControleDeEstoqueUP.DAL;
using ControleDeEstoqueUP.Models;
using ControleSeuEstoque.Utils;
using System;
using System.Collections.Generic;
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
    /// Lógica interna para fmrPesquisaFornecedor.xaml
    /// </summary>
    public partial class frmPesquisaFornecedor : Window {

        FornecedorDAO fornecedorDAO = new FornecedorDAO();
        private List<Fornecedor> fornecedores;

        public int fornecedorId;
        public frmPesquisaFornecedor() {
            InitializeComponent();
            fornecedores = fornecedorDAO.ListarFornecedores();
            gridResultados.ItemsSource = fornecedores.ToList();
        }

        private void BtnLocalizar_Click(object sender, RoutedEventArgs e) {
            switch (ParametroEscolha.SelectedIndex) {
                case 0: // Geral
                    txtPesquisa.Clear();
                    gridResultados.ItemsSource = fornecedores.ToList();
                    break;
                case 1: // ID
                    gridResultados.ItemsSource = fornecedores.ToList().Where(f => f.Id.ToString().Contains(txtPesquisa.Text));
                    break;
                case 2:
                    gridResultados.ItemsSource = fornecedores.Where(f => f.RazaoSocial.ToLower().Contains(txtPesquisa.Text.ToLower()));
                    break;
                default:
                    break;
            }
        }

        private void BtnAbrir_Click(object sender, RoutedEventArgs e) {
            if(gridResultados.SelectedItem == null) {
                WPFUtils.MostrarCaixaDeTextoDeErro("Nenhum Fornecedor Selecionado!");
            } else {
                var selecao = gridResultados.SelectedItem;
                Fornecedor forne = selecao as Fornecedor;

                fornecedorId = forne.Id;

                Close();
            }
        }

        private void GridResultados_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }
    }
}
