using ControleDeEstoqueUP.DAL;
using ControleDeEstoqueUP.Models;
using ControleDeEstoqueUP.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ControleDeEstoqueUP {
    /// <summary>
    /// Lógica interna para fmrPesquisaUnidadeMed.xaml
    /// </summary>
    public partial class frmPesquisaUnidadeMed : Window {
        private readonly UnidadeMedidaDAO unidadeDAO = new UnidadeMedidaDAO();
        private readonly List<UnidadeDeMedida> unidades;
        public int unidadeID;
        public frmPesquisaUnidadeMed() {
            InitializeComponent();
            unidades = unidadeDAO.ListadeUnidades();
            gridResultados.ItemsSource = unidades.ToList();
        }

        private void BtnAbrir_Click(object sender, RoutedEventArgs e) {
            if (gridResultados.SelectedItems == null) {
                WPFUtils.MostrarCaixaDeTextoDeErro("Nenhum Item Selecionado!");
            } else {
                object selecao = gridResultados.SelectedItem;
                UnidadeDeMedida uni = selecao as UnidadeDeMedida;

                unidadeID = uni.Id;
                Close();
            }
        }

        private void BtnLocalizar_Click(object sender, RoutedEventArgs e) {
            switch (ParametroEscolha.SelectedIndex) {
                case 0: // Geral
                    txtPesquisa.Clear();
                    gridResultados.ItemsSource = unidades.ToList();
                    break;
                case 1: // ID
                    gridResultados.ItemsSource = unidades.ToList().Where(un => un.Id.ToString().Contains(txtPesquisa.Text));
                    break;
                case 2: // Nome
                    gridResultados.ItemsSource = unidades.Where(un => un.Nome.ToLower().Contains(txtPesquisa.Text.ToLower()));
                    break;
                default:
                    break;
            }
        }

        private void GridResultados_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }
    }
}
