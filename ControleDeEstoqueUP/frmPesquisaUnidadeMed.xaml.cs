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
    /// Lógica interna para fmrPesquisaUnidadeMed.xaml
    /// </summary>
    public partial class frmPesquisaUnidadeMed : Window {

        UnidadeMedidaDAO unidadeDAO = new UnidadeMedidaDAO();
        private List<UnidadeDeMedida> unidades;
        public int unidadeID;
        public frmPesquisaUnidadeMed() {
            InitializeComponent();
            unidades = unidadeDAO.ListadeUnidades();
            gridResultados.ItemsSource = unidades.ToList();
        }

        private void BtnAbrir_Click(object sender, RoutedEventArgs e) {
            if(gridResultados.SelectedItems == null) {
                WPFUtils.MostrarCaixaDeTextoDeErro("Nenhum Item Selecionado!");
            } else{
                var selecao = gridResultados.SelectedItem;
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
