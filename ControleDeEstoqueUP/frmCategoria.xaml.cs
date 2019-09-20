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
    /// Interaction logic for frmBaseCadastro.xaml
    /// </summary>
    public partial class frmCategoria : Window {

        /**
         * Operações:
         *  0: Nenhuma operação.
         *  1: Inserção
         *  2: Busca
         *  3: Edição
         */
        int operacao;
        CategoriaDAO categoriaDAO = new CategoriaDAO();
        public frmCategoria() {
            InitializeComponent();
            MudarOperacao(0);
        }

        /**
         * BOTÕES
         */

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e) {
            MudarOperacao(1);
            LimparCampos();
        }

        private void BtnLocalizar_Click(object sender, RoutedEventArgs e) {
            MudarOperacao(2);
        }

        
        private void BtnEditar_Click(object sender, RoutedEventArgs e) {
            MudarOperacao(3);
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Nenhum item selecionado!");
            } else {
                if (WPFUtils.MostrarCaixaDeTextoDeConfirmação("Tem certeza que deseja excluir essa categoria? Essa operação não poderá ser desfeita!")) {
                    categoriaDAO.Inativar(Convert.ToInt32(txtCodigo.Text));
                    WPFUtils.MostrarCaixaDeTextoDeInformação("Item excluído com sucesso!");
                    operacao = 0;
                    ModificarBotoesFormulario(operacao);
                    LimparCampos();
                } else {
                    WPFUtils.MostrarCaixaDeTextoDeInformação("Operação cancelada!");
                }
            }
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e) {
            switch (operacao) {
                case 1: //ADIÇÃO
                    if (string.IsNullOrWhiteSpace(txtNome.Text)) {
                        WPFUtils.MostrarCaixaDeTextoDeAlerta("Informe um nome para a categoria!");
                    } else {
                        var categoria = CriarCategoriaComOsDadosDaTela();
                        categoria = categoriaDAO.Inserir(categoria);
                        txtCodigo.Text = categoria.Id.ToString();
                        WPFUtils.MostrarCaixaDeTextoDeInformação("Categoria cadastrada com sucesso!");
                        MudarOperacao(2);
                    }
                    break;
                case 3: //EDIÇÃO
                    if (string.IsNullOrWhiteSpace(txtNome.Text)) {
                        WPFUtils.MostrarCaixaDeTextoDeAlerta("Informe um nome para a categoria!");
                    } else {
                        var categoria = CriarCategoriaComOsDadosDaTela();
                        categoriaDAO.Editar(categoria);
                        WPFUtils.MostrarCaixaDeTextoDeInformação("Categoria atualizada com sucesso!");
                        MudarOperacao(2);
                    }
                    break;
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e) {
            MudarOperacao(0);
            LimparCampos();

        }

        /**
         * Método que limpa os campos da tela.
         */
        private void LimparCampos() {
            txtCodigo.Clear();
            txtNome.Clear();
        }

        /**
         * Método que modifica os botões no formulário de acordo com a operação que está sendo realizada.
         */
        private void ModificarBotoesFormulario(int operacao) {
            switch (operacao) {
                case 0: //INICIAL: BOTÕES DE ADICIONAR E LOCALIZAR ATIVOS. TELA BLOQUEADA.
                    btnAdicionar.IsEnabled = true;
                    btnLocalizar.IsEnabled = true;
                    btnEditar.IsEnabled = false;
                    btnExcluir.IsEnabled = false;
                    btnSalvar.IsEnabled = false;
                    btnCancelar.IsEnabled = false;
                    panelContent.IsEnabled = false;
                    break;
                case 1: //ADIÇÃO: BOTÕES DE SALVAR E CANCELAR ATIVOS. TELA ATIVA.
                    btnAdicionar.IsEnabled = false;
                    btnLocalizar.IsEnabled = false;
                    btnEditar.IsEnabled = false;
                    btnExcluir.IsEnabled = false;
                    btnSalvar.IsEnabled = true;
                    btnCancelar.IsEnabled = true;
                    panelContent.IsEnabled = true;
                    break;
                case 2: //PRODUTO EM TELA: BOTÕES DE EXCLUIR, EDITAR, CANCELAR ATIVOS. TELA BLOQUEADA.
                    btnAdicionar.IsEnabled = false;
                    btnLocalizar.IsEnabled = false;
                    btnEditar.IsEnabled = true;
                    btnExcluir.IsEnabled = true;
                    btnSalvar.IsEnabled = false;
                    btnCancelar.IsEnabled = true;
                    panelContent.IsEnabled = false;
                    break;
                case 3: //EDIÇÃO: BOTÕES DE SALVAR E CANCELAR ATIVOS. TELA ATIVA.
                    btnAdicionar.IsEnabled = false;
                    btnLocalizar.IsEnabled = false;
                    btnEditar.IsEnabled = false;
                    btnExcluir.IsEnabled = false;
                    btnSalvar.IsEnabled = true;
                    btnCancelar.IsEnabled = true;
                    panelContent.IsEnabled = true;
                    break;

            }
        }

        /**
         * Método que recebe os dados e cria uma nova categoria
         */
        private Categoria CriarCategoriaComOsDadosDaTela() {
            //O código está nulo ou vazio? A variável recebe 0, se está preenchido, recebe o valor que está lá.
            int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text);
            string nome = txtNome.Text;
            return new Categoria(nome, id);
        }

        /**
         * Método que modifica os botões na tela e seta a variável operacao com a operação que está sendo realizada no momento.
         */
        private void MudarOperacao(int op) {
            operacao = op;
            ModificarBotoesFormulario(operacao);
        }

    }
}
