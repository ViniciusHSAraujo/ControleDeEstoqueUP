using ControleDeEstoqueUP.DAL;
using ControleDeEstoqueUP.Models;
using ControleDeEstoqueUP.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ControleDeEstoqueUP {
    /// <summary>
    /// Lógica interna para fmrPesquisaFornecedor.xaml
    /// </summary>
    public partial class frmPesquisaFornecedor : Window {
        private readonly FornecedorDAO fornecedorDAO = new FornecedorDAO();
        private readonly List<Fornecedor> fornecedores;

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
                    gridResultados.ItemsSource = fornecedores.Where(f => f.Nome.ToLower().Contains(txtPesquisa.Text.ToLower()));
                    break;
                default:
                    break;
            }
        }

        private void BtnAbrir_Click(object sender, RoutedEventArgs e) {
            if (gridResultados.SelectedItem == null) {
                WPFUtils.MostrarCaixaDeTextoDeErro("Nenhum Fornecedor Selecionado!");
            } else {
                object selecao = gridResultados.SelectedItem;
                Fornecedor forne = selecao as Fornecedor;

                fornecedorId = forne.Id;

                Close();
            }
        }

        private void GridResultados_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }
    }
}
