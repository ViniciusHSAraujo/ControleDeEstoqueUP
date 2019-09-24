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
    /// Interaction logic for frmFuncionario.xaml
    /// </summary>
    public partial class frmFuncionario : Window {
        /**
         * Operações:
         *  0: Nenhuma operação.
         *  1: Inserção
         *  2: Busca
         *  3: Edição
         */
        int operacao;
        FuncionarioDAO funcionarioDAO = new FuncionarioDAO();
        List<Funcionario> funcionarios;
        private int FuncionarioPesquisa;

        public frmFuncionario() {
            InitializeComponent();
            MudarOperacao(0);
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e) {
            AtualizarFuncionario();
            MudarOperacao(1);
            LimparCampos();
        }

        private void btnLocalizar_Click(object sender, RoutedEventArgs e) {
            var pesquisa = new frmPesquisaFuncionario();
            pesquisa.ShowDialog();

            FuncionarioPesquisa = pesquisa.funcionarioId;
            // Se vier 0, então a pessoa fechou sem escolher nenhum item. Então ele não vai fazer nada.
            if (FuncionarioPesquisa != 0) {
                Funcionario funcionario = funcionarioDAO.BuscarFuncionarioPeloId(FuncionarioPesquisa);
                PopularCamposPeloFuncionario(funcionario);
                MudarOperacao(2);
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e) {
            AtualizarFuncionario();
            MudarOperacao(3);
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Nenhum item selecionado!");
            } else {
                if (WPFUtils.MostrarCaixaDeTextoDeConfirmação("Tem certeza que deseja excluir esse funcionário? Essa operação não poderá ser desfeita!")) {
                    funcionarioDAO.Excluir(Convert.ToInt32(txtCodigo.Text));
                    WPFUtils.MostrarCaixaDeTextoDeInformação("Item excluído com sucesso!");
                    operacao = 0;
                    ModificarBotoesFormulario(operacao);
                    LimparCampos();
                } else {
                    WPFUtils.MostrarCaixaDeTextoDeInformação("Operação cancelada!");
                }
            }
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e) {
            switch (operacao) {
                case 1: //ADIÇÃO
                    if (!ValidarCamposObrigatorios()) {
                        var funcionario = CriarFuncionarioComOsDadosDaTela();
                        funcionario = funcionarioDAO.Inserir(funcionario);
                        PopularCamposPeloFuncionario(funcionario);
                        WPFUtils.MostrarCaixaDeTextoDeInformação("Funcionário cadastrado com sucesso!");
                        MudarOperacao(2);
                    }
                    break;
                case 3: //EDIÇÃO
                    if (!ValidarCamposObrigatorios()) {
                        var funcionario = CriarFuncionarioComOsDadosDaTela();
                        funcionarioDAO.Editar(funcionario);
                        WPFUtils.MostrarCaixaDeTextoDeInformação("Funcionário atualizado com sucesso!");
                        MudarOperacao(2);
                    }
                    break;
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e) {
            MudarOperacao(0);
            LimparCampos();
        }

        private bool ValidarCamposObrigatorios() {
            if (string.IsNullOrWhiteSpace(txtNome.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Informe o nome do produto!");
                txtNome.Focus();
                return false;
            }
            return true;
        }

        /**
       * Método que limpa os campos da tela.
       */
        private void LimparCampos() {
            txtCodigo.Clear();
            txtNome.Clear();
            txtCEP.Clear();
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
                case 2: //FUNCIONARIO EM TELA: BOTÕES DE EXCLUIR, EDITAR, CANCELAR ATIVOS. TELA BLOQUEADA.
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
        private Funcionario CriarFuncionarioComOsDadosDaTela() {
            //O código está nulo ou vazio? A variável recebe 0, se está preenchido, recebe o valor que está lá.
            int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text);
            string nome = txtNome.Text;
            return new Funcionario();
        }

        private void MudarOperacao(int op) {
            operacao = op;
            ModificarBotoesFormulario(operacao);
        }

        private void AtualizarFuncionario() {
            funcionarios = funcionarioDAO.ListarFuncionarios();
        }

        /**
       * Método que recebe um funcionário e preenche na tela os campos com suas informações.
       */
        private void PopularCamposPeloFuncionario(Funcionario funcionario) {
            txtCodigo.Text = funcionario.Id.ToString();
            txtNome.Text = funcionario.Nome;
        }
    }
}
