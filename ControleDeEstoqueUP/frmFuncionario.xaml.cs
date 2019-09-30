using ControleDeEstoqueUP.DAL;
using ControleDeEstoqueUP.Models;
using ControleDeEstoqueUP.Utils;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

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
        private int operacao;
        private readonly FuncionarioDAO funcionarioDAO = new FuncionarioDAO();
        private int FuncionarioPesquisa;

        public frmFuncionario() {
            InitializeComponent();
            MudarOperacao(0);
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e) {
            MudarOperacao(1);
            LimparCampos();
        }

        private void btnLocalizar_Click(object sender, RoutedEventArgs e) {
            frmPesquisaFuncionario pesquisa = new frmPesquisaFuncionario();
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
                    if (ValidarCamposObrigatorios()) {
                        Funcionario funcionario = CriarFuncionarioComOsDadosDaTela();
                        funcionario = funcionarioDAO.Inserir(funcionario);
                        PopularCamposPeloFuncionario(funcionario);
                        WPFUtils.MostrarCaixaDeTextoDeInformação("Funcionário cadastrado com sucesso!");
                        MudarOperacao(2);
                    }
                    break;
                case 3: //EDIÇÃO
                    if (ValidarCamposObrigatorios()) {
                        Funcionario funcionario = CriarFuncionarioComOsDadosDaTela();
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
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Informe o nome do funcionário!");
                txtNome.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCpf.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Informe o CPF do funcionário!");
                txtCpf.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTelefone.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Informe o telefone do funcionário!");
                txtTelefone.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCEP.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Informe o CEP do funcionário!");
                txtCEP.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSenha.Password)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Informe a senha do funcionário!");
                txtSenha.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCargo.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Informe o cargo do funcionário!");
                txtCargo.Focus();
                return false;
            }
            if (!txtAdmissao.SelectedDate.HasValue) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Informe a data de admissão do funcionário!");
                txtAdmissao.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Informe o email do funcionário!");
                txtEmail.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSalario.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Informe o salario do funcionário!");
                txtSalario.Focus();
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
            txtCpf.Clear();
            txtCEP.Clear();
            txtEndereco.Clear();
            txtBairro.Clear();
            txtCidade.Clear();
            txtUF.Clear();
            txtTelefone.Clear();
            txtCelular.Clear();
            txtEmail.Clear();
            txtSalario.Clear();
            txtCargo.Clear();
            txtSenha.Clear();
            txtDemissao.SelectedDate = null;
            txtAdmissao.SelectedDate = null;
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
                    gridTela.IsEnabled = false;
                    break;
                case 1: //ADIÇÃO: BOTÕES DE SALVAR E CANCELAR ATIVOS. TELA ATIVA.
                    btnAdicionar.IsEnabled = false;
                    btnLocalizar.IsEnabled = false;
                    btnEditar.IsEnabled = false;
                    btnExcluir.IsEnabled = false;
                    btnSalvar.IsEnabled = true;
                    btnCancelar.IsEnabled = true;
                    gridTela.IsEnabled = true;
                    break;
                case 2: //FUNCIONARIO EM TELA: BOTÕES DE EXCLUIR, EDITAR, CANCELAR ATIVOS. TELA BLOQUEADA.
                    btnAdicionar.IsEnabled = false;
                    btnLocalizar.IsEnabled = false;
                    btnEditar.IsEnabled = true;
                    btnExcluir.IsEnabled = true;
                    btnSalvar.IsEnabled = false;
                    btnCancelar.IsEnabled = true;
                    gridTela.IsEnabled = false;
                    break;
                case 3: //EDIÇÃO: BOTÕES DE SALVAR E CANCELAR ATIVOS. TELA ATIVA.
                    btnAdicionar.IsEnabled = false;
                    btnLocalizar.IsEnabled = false;
                    btnEditar.IsEnabled = false;
                    btnExcluir.IsEnabled = false;
                    btnSalvar.IsEnabled = true;
                    btnCancelar.IsEnabled = true;
                    gridTela.IsEnabled = true;
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
            string cep = txtCEP.Text;
            string cpf = txtCpf.Text;
            string endereco = txtEndereco.Text;
            string bairro = txtBairro.Text;
            string cidade = txtCidade.Text;
            string uf = txtUF.Text;
            string telefone = txtTelefone.Text;
            string celular = txtCelular.Text;
            string email = txtEmail.Text;
            string senha = txtSenha.Password;
            string cargo = txtCargo.Text;
            double salario = Convert.ToDouble(txtSalario.Text);
            DateTime admissao = txtAdmissao.SelectedDate.Value;
            DateTime? demissao = txtDemissao.SelectedDate;
            return new Funcionario(nome, cpf, cep, endereco, bairro, cidade, uf, telefone, celular, email, senha, cargo, salario, admissao, demissao, id);
        }

        private void MudarOperacao(int op) {
            operacao = op;
            ModificarBotoesFormulario(operacao);
        }
        /**
       * Método que recebe um funcionário e preenche na tela os campos com suas informações.
       */
        private void PopularCamposPeloFuncionario(Funcionario funcionario) {
            txtCodigo.Text = funcionario.Id.ToString();
            txtNome.Text = funcionario.Nome.ToString();
            txtCEP.Text = funcionario.CEP.ToString();
            txtCpf.Text = funcionario.CPF.ToString();
            txtEndereco.Text = funcionario.Endereco.ToString();
            txtBairro.Text = funcionario.Bairro.ToString();
            txtCidade.Text = funcionario.Cidade.ToString();
            txtUF.Text = funcionario.UF.ToString();
            txtTelefone.Text = funcionario.Telefone.ToString();
            txtCelular.Text = funcionario.Celular.ToString();
            txtEmail.Text = funcionario.Email.ToString();
            txtSenha.Password = funcionario.Senha.ToString();
            txtCargo.Text = funcionario.Cargo.ToString();
            txtSalario.Text = funcionario.Salario.ToString();
            txtAdmissao.SelectedDate = funcionario.Admissao;
            txtDemissao.SelectedDate = funcionario.Demissao;
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
