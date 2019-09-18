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
    /// Interaction logic for frmBaseCadastro.xaml
    /// </summary>
    public partial class frmBaseCadastro : Window {
        public frmBaseCadastro() {
            InitializeComponent();
            ModificarBotoesFormulario(0);
        }

        private void BtnAdicinoar_Click(object sender, RoutedEventArgs e) {
            ModificarBotoesFormulario(1);
        }

        private void BtnLocalizar_Click(object sender, RoutedEventArgs e) {
            ModificarBotoesFormulario(2);
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e) {
            ModificarBotoesFormulario(0);
        }

        private void ModificarBotoesFormulario(int operacao) {
            switch (operacao) {
                case 0: //Inicial, somente o botão de adicionar e localizar ativos..
                    btnAdicionar.IsEnabled = true;
                    btnLocalizar.IsEnabled = true;
                    btnEditar.IsEnabled = false;
                    btnExcluir.IsEnabled = false;
                    btnSalvar.IsEnabled = false;
                    btnCancelar.IsEnabled = false;
                    panelContent.IsEnabled = false;
                    break;
                case 1: //Botão ADICIONAR clicado, somente o botão de adicionar e localizar ativos..
                    btnAdicionar.IsEnabled = false;
                    btnLocalizar.IsEnabled = false;
                    btnEditar.IsEnabled = false;
                    btnExcluir.IsEnabled = false;
                    btnSalvar.IsEnabled = true;
                    btnCancelar.IsEnabled = true;
                    panelContent.IsEnabled = true;
                    break;
                case 2: //Botão LOCALIZAR clicado, somente o botão de excluir, editar e cancelar ativos..
                    btnAdicionar.IsEnabled = false;
                    btnLocalizar.IsEnabled = false;
                    btnEditar.IsEnabled = true;
                    btnExcluir.IsEnabled = true;
                    btnSalvar.IsEnabled = false;
                    btnCancelar.IsEnabled = true;
                    panelContent.IsEnabled = true;
                    break;
            }
        }
    }
}
