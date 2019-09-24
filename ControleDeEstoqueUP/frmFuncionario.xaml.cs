using ControleDeEstoqueUP.DAL;
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

namespace ControleDeEstoqueUP
{
    /// <summary>
    /// Interaction logic for frmFuncionario.xaml
    /// </summary>
    public partial class frmFuncionario : Window
    {
        /**
         * Operações:
         *  0: Nenhuma operação.
         *  1: Inserção
         *  2: Busca
         *  3: Edição
         */
        int operacao;
        FuncionarioDAO funcionarioDAO = new FuncionarioDAO();
        public frmFuncionario()
        {
            InitializeComponent();
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e) {

        }

        private void btnLocalizar_Click(object sender, RoutedEventArgs e) {

        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e) {

        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e) {

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e) {

        }
    }
}
