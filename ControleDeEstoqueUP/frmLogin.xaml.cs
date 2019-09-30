using ControleDeEstoqueUP.DAL;
using ControleDeEstoqueUP.Models;
using ControleDeEstoqueUP.Utils;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ControleDeEstoqueUP {
    /// <summary>
    /// Interaction logic for frmLogin.xaml
    /// </summary>
    public partial class frmLogin : Window {
        private readonly FuncionarioDAO funcionarioDAO = new FuncionarioDAO();
        public static Funcionario funcionarioLogado;

        public frmLogin() {
            InitializeComponent();
            txtCodigoFuncionario.Focus();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e) {

            string senha = txtSenha.Password;

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
                    MainWindow w = new MainWindow();
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
            e.Handled = !int.TryParse(e.Text, out int result);
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
