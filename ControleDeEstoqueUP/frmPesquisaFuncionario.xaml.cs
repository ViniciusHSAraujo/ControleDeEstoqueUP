using ControleDeEstoqueUP.DAL;
using ControleDeEstoqueUP.Models;
using ControleDeEstoqueUP.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ControleDeEstoqueUP {
    /// <summary>
    /// Interaction logic for frmPesquisaFuncionario.xaml
    /// </summary>
    public partial class frmPesquisaFuncionario : Window {
        private readonly FuncionarioDAO funcionarioDAO = new FuncionarioDAO();
        private readonly List<Funcionario> funcionarios;

        public int funcionarioId;
        public frmPesquisaFuncionario() {
            InitializeComponent();
            funcionarios = funcionarioDAO.ListarFuncionarios();
            gridResultados.ItemsSource = funcionarios.ToList();
        }

        private void btnLocalizar_Click(object sender, RoutedEventArgs e) {
            switch (ParametroEscolha.SelectedIndex) {
                case 0: //Geral
                    txtPesquisa.Clear();
                    gridResultados.ItemsSource = funcionarios.ToList();
                    break;
                case 1: //ID
                    gridResultados.ItemsSource = funcionarios.Where(f => f.Id.ToString().Contains(txtPesquisa.Text)).ToList();
                    break;
                case 2: //NOME
                    gridResultados.ItemsSource = funcionarios.Where(f => f.Nome.ToLower().Contains((txtPesquisa.Text.ToLower()))).ToList();
                    break;
                default:
                    break;
            }
        }
        private void btnAbrir_Click(object sender, RoutedEventArgs e) {
            if (gridResultados.SelectedItem == null) {
                WPFUtils.MostrarCaixaDeTextoDeErro("Nenhum item selecionado.");
            } else {
                object selecao = gridResultados.SelectedItem;
                Funcionario fun = selecao as Funcionario;

                funcionarioId = fun.Id;
                Close();
            }
        }
    }
}
