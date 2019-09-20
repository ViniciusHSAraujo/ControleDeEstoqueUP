using System.Windows;

namespace ControleDeEstoqueUP {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void MiClientes_Click(object sender, RoutedEventArgs e) {
            //var w = new frmCliente();
            //w.ShowDialog();
        }

        private void MiFornecedores_Click(object sender, RoutedEventArgs e) {
            //var w = new frmFornecedor();
            //w.ShowDialog();
        }

        private void MiFuncionarios_Click(object sender, RoutedEventArgs e) {
            //var w = new frmFuncionario();
            //w.ShowDialog();
        }

        private void MiProdutos_Click(object sender, RoutedEventArgs e) {
            //var w = new frmProduto();
            //w.ShowDialog();
        }

        private void MiCategorias_Click(object sender, RoutedEventArgs e) {
            var w = new frmCategoria();
            w.ShowDialog();
        }

        private void MiUnidades_Click(object sender, RoutedEventArgs e) {
            //var w = new frmUnidade();
            //w.ShowDialog();
        }
    }
}
