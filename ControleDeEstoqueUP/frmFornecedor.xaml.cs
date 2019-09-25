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
    /// Lógica interna para CadFornecedor.xaml
    /// </summary>
    public partial class frmFornecedor : Window {
        public frmFornecedor() {
            InitializeComponent();
            MudarOperacao(0);
        }

        int operacao;
        FornecedorDAO fornecedorDAO = new FornecedorDAO();

        public int FornecedorPesquisar;

        /*
         * Método que irá adicionar o Fornecedor Cadastrado com base nas informações e
         * Limpar todos os campos após o click no botão Adicionar (+)
         */
        private void BtnAdicionar_Click(object sender, RoutedEventArgs e) {
            MudarOperacao(1);
            LimparCampos();
        }
        private void BtnEditar_Click(object sender, RoutedEventArgs e) {
            MudarOperacao(3);
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Nenhum Item Selecionado!");
            } else {
                if (WPFUtils.MostrarCaixaDeTextoDeConfirmação("Tem certeza que deseja excluir este Fornecedor?")) {
                    fornecedorDAO.Excluir(Convert.ToInt32(txtCodigo.Text));
                    WPFUtils.MostrarCaixaDeTextoDeInformação("Fornecedor Excluído com Sucesso!");
                    operacao = 0;
                    ModificarBotoesFormulario(operacao);
                    LimparCampos();
                } else {
                    WPFUtils.MostrarCaixaDeTextoDeInformação("Operação Cancelada!");
                }
            }
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e) {
            switch (operacao) {
                case 1: //Adicionar
                    if (string.IsNullOrWhiteSpace(txtRazaoSocial.Text)) {
                        WPFUtils.MostrarCaixaDeTextoDeAlerta("Informe a Razão Social do Fornecedor!");
                    } else {
                        var fornecedor = CriarFornecedorComOsDadosTela();
                        fornecedor = fornecedorDAO.Inserir(fornecedor);
                        PreencherTelaPeloFornecedor(fornecedor);
                        WPFUtils.MostrarCaixaDeTextoDeInformação("Fornecedor Cadastrado com Sucesso!");
                        MudarOperacao(2);
                    }
                    break;
                case 3: // Editar
                    if (string.IsNullOrWhiteSpace(txtRazaoSocial.Text)) {
                        WPFUtils.MostrarCaixaDeTextoDeAlerta("Informe a Razão Social do Fornecedor!");
                    } else {
                        var fornecedor = CriarFornecedorComOsDadosTela();
                        fornecedorDAO.Editar(fornecedor);
                        WPFUtils.MostrarCaixaDeTextoDeInformação("Dados do fornecedor Atualizado com Sucesso!");
                        MudarOperacao(2);
                    }
                    break;
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e) {
            MudarOperacao(0);
            LimparCampos();
        }
        private void LimparCampos() {
            txtCodigo.Clear();
            txtRazaoSocial.Clear();
            txtIE.Clear();
            txtCnpj.Clear();
            txtCEP.Clear();
            txtEndereco.Clear();
            txtBairro.Clear();
            txtCidade.Clear();
            txtUF.Clear();
            txtTelefone.Clear();
            txtCelular.Clear();
            txtEmail.Clear();
        }
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
                case 2: //PRODUTO EM TELA: BOTÕES DE EXCLUIR, EDITAR, CANCELAR ATIVOS. TELA BLOQUEADA.
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
        private void MudarOperacao(int operac) {
            operacao = operac;
            ModificarBotoesFormulario(operacao);
        }
        


        private Fornecedor CriarFornecedorComOsDadosTela() {
            string nome = txtRazaoSocial.Text;
            string cnpj = txtCnpj.Text;
            string cep = txtCEP.Text;
            string endereco = txtEndereco.Text;
            string bairro = txtBairro.Text;
            string cidade = txtCidade.Text;
            string uf = txtUF.Text;
            string telefone = txtTelefone.Text;
            string celular = txtCelular.Text;
            string email = txtEmail.Text;

            return new Fornecedor(cnpj, nome, cep, endereco, bairro, cidade, uf, telefone, celular, email);
        }

        private void PreencherTelaPeloFornecedor(Fornecedor fornecedor) {
            txtCodigo.Text = fornecedor.Id.ToString();
            txtRazaoSocial.Text = fornecedor.Nome;
            txtCnpj.Text = fornecedor.CNPJ;
            txtCEP.Text = fornecedor.CEP;
            txtEndereco.Text = fornecedor.Endereco;
            txtBairro.Text = fornecedor.Bairro;
            txtCidade.Text = fornecedor.Cidade;
            txtUF.Text = fornecedor.UF;
            txtTelefone.Text = fornecedor.Telefone;
            txtCelular.Text = fornecedor.Celular;
            txtEmail.Text = fornecedor.Email;
        }

        private void BtnLocalizar_Click(object sender, RoutedEventArgs e) {
            var pesquisa = new frmPesquisaFornecedor();
            pesquisa.ShowDialog();

            FornecedorPesquisar = pesquisa.fornecedorId;

            if (FornecedorPesquisar != 0) {
                Fornecedor fornecedor = fornecedorDAO.BuscarFornecedorPorId(FornecedorPesquisar);
                PreencherTelaPeloFornecedor(fornecedor);
                MudarOperacao(2);
            }
        }
    }
}
