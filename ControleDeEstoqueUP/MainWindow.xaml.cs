using ControleDeEstoqueUP.Utils;
using System.Windows;

namespace ControleDeEstoqueUP {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public static System.Windows.Rect WorkArea { get; }

        public MainWindow() {
            InitializeComponent();
            TelaPrincipal.Width = System.Windows.SystemParameters.WorkArea.Width;
            TelaPrincipal.Height = System.Windows.SystemParameters.WorkArea.Height;

            if (isUsuarioZero()) {
                miClientes.IsEnabled = false;
                miFornecedores.IsEnabled = false;
                miProdutos.IsEnabled = false;
                miUnidades.IsEnabled = false;
                miCategorias.IsEnabled = false;
                miCompras.IsEnabled = false;
                miVendas.IsEnabled = false;

                WPFUtils.MostrarCaixaDeTextoDeInformação("Olá! Bem vindo ao nosso sistema!\nPara ter acesso completo, é necessário que você entre com as credenciais válidas de um funcionário.\nPor favor, realize o cadastro de um novo funcionário e acesse novamente o sistema com o ID gerado e a senha cadastrada.");
            }
        }

        private void MiClientes_Click(object sender, RoutedEventArgs e) {
            frmCliente w = new frmCliente();
            w.ShowDialog();
        }

        private void MiFornecedores_Click(object sender, RoutedEventArgs e) {
            frmFornecedor w = new frmFornecedor();
            w.ShowDialog();
        }

        private void MiFuncionarios_Click(object sender, RoutedEventArgs e) {
            frmFuncionario w = new frmFuncionario();
            w.ShowDialog();
        }

        private void MiProdutos_Click(object sender, RoutedEventArgs e) {
            frmProduto w = new frmProduto();
            w.ShowDialog();
        }

        private void MiCategorias_Click(object sender, RoutedEventArgs e) {
            frmCategoria w = new frmCategoria();
            w.ShowDialog();
        }

        private void MiUnidades_Click(object sender, RoutedEventArgs e) {
            frmUnidadeMed w = new frmUnidadeMed();
            w.ShowDialog();
        }

        private void MiSobre_Click(object sender, RoutedEventArgs e) {
            frmSobre w = new frmSobre();
            w.ShowDialog();
        }

        private void MiCompras_Click(object sender, RoutedEventArgs e) {
            frmCompra w = new frmCompra();
            w.ShowDialog();
        }

        private void MiVendas_Click(object sender, RoutedEventArgs e) {
            frmVenda w = new frmVenda();
            w.ShowDialog();
        }

        private bool isUsuarioZero() {
            if (frmLogin.funcionarioLogado.Id == 0) {
                return true;
            } else {
                return false;
            }
        }
    }
}
