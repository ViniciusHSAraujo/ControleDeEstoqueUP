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
    public partial class frmPesquisaCategoria : Window {
        private readonly CategoriaDAO categoriaDAO = new CategoriaDAO();
        private readonly List<Categoria> categorias;

        public int categoriaId;

        public frmPesquisaCategoria() {
            InitializeComponent();
            categorias = categoriaDAO.ListarCategorias();
            gridResultados.ItemsSource = categorias.ToList();
        }

        private void BtnLocalizar_Click(object sender, RoutedEventArgs e) {

            switch (ParametroEscolha.SelectedIndex) {
                case 0: //Geral
                    txtPesquisa.Clear();
                    gridResultados.ItemsSource = categorias.ToList();
                    break;
                case 1: //ID
                    gridResultados.ItemsSource = categorias.ToList().Where(p => p.Id.ToString().Contains(txtPesquisa.Text));
                    break;
                case 2: //NOME
                    gridResultados.ItemsSource = categorias.Where(p => p.Nome.ToLower().Contains((txtPesquisa.Text.ToLower())));
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
                Categoria cat = selecao as Categoria;

                categoriaId = cat.Id;
                Close();
            }
        }
    }
}
