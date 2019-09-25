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
    /// Lógica interna para formCadastroUnidade.xaml
    /// </summary>
    public partial class frmUnidadeMed : Window {

        int operacao;
        UnidadeMedidaDAO unidadeDAO = new UnidadeMedidaDAO();
        public int UnidadeMedidaPesquisa;

        public frmUnidadeMed() {
            InitializeComponent();
            MudarOperacao(0);
        }

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e) {
            MudarOperacao(1);
            LimparCampos();
        }

        private void BtnLocalizar_Click(object sender, RoutedEventArgs e) {
            var pesquisa = new frmPesquisaUnidadeMed();
            pesquisa.ShowDialog();

            UnidadeMedidaPesquisa = pesquisa.unidadeID;

            if (UnidadeMedidaPesquisa != 0) {
                UnidadeDeMedida unidade = unidadeDAO.BuscarUnidadePorId(UnidadeMedidaPesquisa);
                PreencherTelaPelaUnidade(unidade);
                MudarOperacao(2);
            }
        }

        public void PreencherTelaPelaUnidade(UnidadeDeMedida unidade) {
            txtCodigo.Text = unidade.Id.ToString();
            txtNome.Text = unidade.Nome;
            txtSimbolo.Text = unidade.Simbolo;
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e) {
            MudarOperacao(3);
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Nenhum Item foi Selecionado!");
            } else {
                if (WPFUtils.MostrarCaixaDeTextoDeConfirmação("Deseja Realmente excluir essa Unidade de Medida?")) {
                    unidadeDAO.Excluir(Convert.ToInt32(txtCodigo.Text));
                    WPFUtils.MostrarCaixaDeTextoDeInformação("Unidade de Medida Excluída com Sucesso!");
                    operacao = 0;
                    ModificarBotoesFormulario(operacao);
                    LimparCampos();
                } else {
                    WPFUtils.MostrarCaixaDeTextoDeInformação("Operação Cancelada");
                }
            }
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e) {
            switch (operacao) {
                case 1: // Adicionar
                    if (string.IsNullOrWhiteSpace(txtNome.Text)) {
                        WPFUtils.MostrarCaixaDeTextoDeAlerta("Informe um Nome para a Unidade de Medida!");
                    } else {
                        var unidadeMedida = CriarUnidadeMedidaComDadosTela();
                        unidadeMedida = unidadeDAO.Inserir(unidadeMedida);
                        txtCodigo.Text = unidadeMedida.Id.ToString();
                        WPFUtils.MostrarCaixaDeTextoDeInformação("Unidade de Medida Cadastrada Com Sucesso!");
                        MudarOperacao(2);
                    }
                    break;
                case 3: // Editar
                    if (string.IsNullOrWhiteSpace(txtNome.Text)) {
                        WPFUtils.MostrarCaixaDeTextoDeAlerta("Informe o Nome da Unidade de Medida!");
                    } else {
                        var unidadeMedida = CriarUnidadeMedidaComDadosTela();
                        unidadeDAO.Editar(unidadeMedida);
                        WPFUtils.MostrarCaixaDeTextoDeInformação("Unidade de Medida Atualizada Com Sucesso!");
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
            txtNome.Clear();
            txtCodigo.Clear();
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
                    gridConteudo.IsEnabled = false;
                    break;
                case 1: //ADIÇÃO: BOTÕES DE SALVAR E CANCELAR ATIVOS. TELA ATIVA.
                    btnAdicionar.IsEnabled = false;
                    btnLocalizar.IsEnabled = false;
                    btnEditar.IsEnabled = false;
                    btnExcluir.IsEnabled = false;
                    btnSalvar.IsEnabled = true;
                    btnCancelar.IsEnabled = true;
                    gridConteudo.IsEnabled = true;
                    break;
                case 2: //PRODUTO EM TELA: BOTÕES DE EXCLUIR, EDITAR, CANCELAR ATIVOS. TELA BLOQUEADA.
                    btnAdicionar.IsEnabled = false;
                    btnLocalizar.IsEnabled = false;
                    btnEditar.IsEnabled = true;
                    btnExcluir.IsEnabled = true;
                    btnSalvar.IsEnabled = false;
                    btnCancelar.IsEnabled = true;
                    gridConteudo.IsEnabled = false;
                    break;
                case 3: //EDIÇÃO: BOTÕES DE SALVAR E CANCELAR ATIVOS. TELA ATIVA.
                    btnAdicionar.IsEnabled = false;
                    btnLocalizar.IsEnabled = false;
                    btnEditar.IsEnabled = false;
                    btnExcluir.IsEnabled = false;
                    btnSalvar.IsEnabled = true;
                    btnCancelar.IsEnabled = true;
                    gridConteudo.IsEnabled = true;
                    break;

            }
        }

        /*
         * Método que Cria uma Nova Unidade de Medida de Acordo com os Itens informados na Tela
         */
        private UnidadeDeMedida CriarUnidadeMedidaComDadosTela() {
            int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text);
            string nome = txtNome.Text;
            string simbolo = txtSimbolo.Text;

            return new UnidadeDeMedida(nome, simbolo, id);
        }

        /*
        * Método que irá habilitar os botõe de acordo com as informaçõe spresentes no banco de dados e
        * Na operação realizada no instante.
        */
        private void MudarOperacao(int op) {
            operacao = op;
            ModificarBotoesFormulario(operacao);
        }
    }
}
