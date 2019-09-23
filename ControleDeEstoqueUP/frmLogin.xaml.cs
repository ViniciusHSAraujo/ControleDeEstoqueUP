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
    /// Interaction logic for frmLogin.xaml
    /// </summary>
    public partial class frmLogin : Window {

        public static Funcionario funcionarioLogado;

        public frmLogin() {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e) {
            int usuario = Convert.ToInt32(txtCodigoFuncionario.Text);
            string senha = txtSenha.Password;

            funcionarioLogado = null ; // FuncionarioDAO.LoginDeUsuario(usuario, senha);

            if (funcionarioLogado == null) {
                var w = new MainWindow();
                formLogin.Visibility = Visibility.Hidden;
                w.ShowDialog();
                formLogin.Close();
            } else {
                WPFUtils.MostrarCaixaDeTextoDeErro("Credenciais inválidas! Verifique e tente novamente.");
            }
        }
    }
}
