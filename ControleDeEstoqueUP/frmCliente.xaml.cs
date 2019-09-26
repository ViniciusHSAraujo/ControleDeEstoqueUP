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
    /// Interaction logic for frmCliente.xaml
    /// </summary>
    public partial class frmCliente : Window {
        /**
         * Operações:
         *  0: Nenhuma operação.
         *  1: Inserção
         *  2: Busca
         *  3: Edição
         */
        int operacao;
        ClienteDAO clienteDAO = new ClienteDAO();
        private int ClientePesquisa;
        public frmCliente() {
            InitializeComponent();
            MudarOperacao(0);
        }


        private void btnAdicionar_Click(object sender, RoutedEventArgs e) {
            MudarOperacao(1);
            LimparCampos();
        }

        private void btnLocalizar_Click(object sender, RoutedEventArgs e) {
            var pesquisa = new frmPesquisarCliente();
            pesquisa.ShowDialog();

            ClientePesquisa = pesquisa.clienteId;
            // Se vier 0, então a pessoa fechou sem escolher nenhum item. Então ele não vai fazer nada.
            if (ClientePesquisa != 0) {
                Cliente cliente = clienteDAO.BuscarClientePeloId(ClientePesquisa);
                PopularCamposPeloCliente(cliente);
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
                if (WPFUtils.MostrarCaixaDeTextoDeConfirmação("Tem certeza que deseja excluir esse cliente? Essa operação não poderá ser desfeita!")) {
                    clienteDAO.Excluir(Convert.ToInt32(txtCodigo.Text));
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
                        var cliente = CriarClienteComOsDadosDaTela();
                        cliente = clienteDAO.Inserir(cliente);
                        PopularCamposPeloCliente(cliente);
                        WPFUtils.MostrarCaixaDeTextoDeInformação("Cliente cadastrado com sucesso!");
                        MudarOperacao(2);
                    }
                    break;
                case 3: //EDIÇÃO
                    if (ValidarCamposObrigatorios()) {
                        var cliente = CriarClienteComOsDadosDaTela();
                        clienteDAO.Editar(cliente);
                        WPFUtils.MostrarCaixaDeTextoDeInformação("Cliente atualizado com sucesso!");
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
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Informe o nome do cliente!");
                txtNome.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCpf.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Informe o CPF do cliente!");
                txtCpf.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTelefone.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Informe o telefone do cliente!");
                txtTelefone.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCEP.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Informe o CEP do cliente!");
                txtCEP.Focus();
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
                case 2: //CLIENTE EM TELA: BOTÕES DE EXCLUIR, EDITAR, CANCELAR ATIVOS. TELA BLOQUEADA.
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
             * Método que recebe os dados e cria um cliente
             */
        private Cliente CriarClienteComOsDadosDaTela() {
            //O código está nulo ou vazio? A variável recebe 0, se está preenchido, recebe o valor que está lá.
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
            return new Cliente(nome, cpf, cep, endereco, bairro, cidade, uf, telefone, celular, email);
        }

        private void MudarOperacao(int op) {
            operacao = op;
            ModificarBotoesFormulario(operacao);
        }

        /**
      * Método que recebe um funcionário e preenche na tela os campos com suas informações.
      */
        private void PopularCamposPeloCliente(Cliente cliente) {
            txtCodigo.Text = cliente.Id.ToString();
            txtNome.Text = cliente.Nome.ToString();
            txtCEP.Text = cliente.CEP.ToString();
            txtCpf.Text = cliente.CPF.ToString();
            txtEndereco.Text = cliente.Endereco.ToString();
            txtBairro.Text = cliente.Bairro.ToString();
            txtCidade.Text = cliente.Cidade.ToString();
            txtUF.Text = cliente.UF.ToString();
            txtTelefone.Text = cliente.Telefone.ToString();
            txtCelular.Text = cliente.Celular.ToString();
            txtEmail.Text = cliente.Email.ToString();
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
