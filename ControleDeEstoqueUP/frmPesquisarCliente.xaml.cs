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
    /// Interaction logic for frmPesquisarCliente.xaml
    /// </summary>
    public partial class frmPesquisarCliente : Window {
        ClienteDAO clienteDAO = new ClienteDAO();
        private List<Cliente> clientes;

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
                var selecao = gridResultados.SelectedItem;
                Cliente cli = selecao as Cliente;

                clienteId = cli.Id;
                Close();
            }
        }
    }
}
