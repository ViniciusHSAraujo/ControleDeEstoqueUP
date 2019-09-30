using ControleDeEstoqueUP.DAL;
using ControleDeEstoqueUP.Models;
using ControleDeEstoqueUP.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ControleDeEstoqueUP {
    /// <summary>
    /// Interaction logic for frmPesquisarCliente.xaml
    /// </summary>
    public partial class frmPesquisarCliente : Window {
        private readonly ClienteDAO clienteDAO = new ClienteDAO();
        private readonly List<Cliente> clientes;

        public int clienteId;
        public frmPesquisarCliente() {
            InitializeComponent();
            clientes = clienteDAO.ListarClientes();
            gridResultados.ItemsSource = clientes.ToList();
        }

        private void btnLocalizar_Click(object sender, RoutedEventArgs e) {
            switch (ParametroEscolha.SelectedIndex) {
                case 0: //Geral
                    txtPesquisa.Clear();
                    gridResultados.ItemsSource = clientes.ToList();
                    break;
                case 1: //ID
                    gridResultados.ItemsSource = clientes.Where(c => c.Id.ToString().Contains(txtPesquisa.Text)).ToList();
                    break;
                case 2: //NOME
                    gridResultados.ItemsSource = clientes.Where(c => c.Nome.ToLower().Contains((txtPesquisa.Text.ToLower()))).ToList();
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
                Cliente cli = selecao as Cliente;

                clienteId = cli.Id;
                Close();
            }
        }
    }
}
