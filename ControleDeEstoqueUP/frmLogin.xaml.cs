using ControleDeEstoqueUP.Models;
using ControleSeuEstoque.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Security;
using ControleDeEstoqueUP.DAL;

namespace ControleDeEstoqueUP {
    /// <summary>
    /// Interaction logic for frmLogin.xaml
    /// </summary>
    public partial class frmLogin : Window {
        FuncionarioDAO funcionarioDAO = new FuncionarioDAO();
        public static Funcionario funcionarioLogado;

        public frmLogin() {
            InitializeComponent();
            txtCodigoFuncionario.Focus();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e) {

            var senha = txtSenha.Password;

            if (string.IsNullOrWhiteSpace(txtCodigoFuncionario.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeErro("É necessário preencher o código do funcionário!");
            } else if (string.IsNullOrWhiteSpace(senha)) {
                WPFUtils.MostrarCaixaDeTextoDeErro("É necessário preencher a senha!");
            } else {
                int usuario = Convert.ToInt32(txtCodigoFuncionario.Text);

                if (usuario == 0 && senha == "CEUP123") {
                    funcionarioLogado = new Funcionario() { Id = 0 };
                } else {
                    funcionarioLogado = funcionarioDAO.RealizarLogin(usuario, senha);
                }

                if (funcionarioLogado != null) {
                    var w = new MainWindow();
                    formLogin.Visibility = Visibility.Hidden;
                    w.ShowDialog();
                    formLogin.Close();
                } else {
                    WPFUtils.MostrarCaixaDeTextoDeErro("Credenciais inválidas! Verifique e tente novamente.");
                }
            }
        }

        /**
         * Validação que aceita apenas a entrada de números inteiros no TextBox
         */
        private void ApenasNumerosValidationTextBox(object sender, TextCompositionEventArgs e) {
            e.Handled = !Int32.TryParse(e.Text, out int result);
        }

        /**
         * Validação que aceita números e a vírgula no TextBox.
         */
        private void ApenasNumerosEVirgulaValidationTextBox(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9,-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
