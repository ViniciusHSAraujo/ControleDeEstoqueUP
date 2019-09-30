using ControleDeEstoqueUP.DAL;
using ControleDeEstoqueUP.Models;
using ControleDeEstoqueUP.Utils;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ControleDeEstoqueUP {
    /// <summary>
    /// Interaction logic for frmBaseCadastro.xaml
    /// </summary>
    public partial class frmProduto : Window {

        /**
         * Operações:
         *  0: Nenhuma operação.
         *  1: Inserção
         *  2: Busca
         *  3: Edição
         */
        private int operacao;
        private readonly ProdutoDAO produtoDAO = new ProdutoDAO();
        private readonly CategoriaDAO categoriaDAO = new CategoriaDAO();
        private readonly UnidadeMedidaDAO unidadeDeMedidaDAO = new UnidadeMedidaDAO();
        private List<Categoria> categorias;
        private List<UnidadeDeMedida> unidades;

        public int ProdutoPesquisa;

        public frmProduto() {
            InitializeComponent();
            MudarOperacao(0);
        }

        /**
         * BOTÕES
         */

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e) {
            AtualizarCategoriasEUnidades();
            MudarOperacao(1);
            LimparCampos();
        }

        private void BtnLocalizar_Click(object sender, RoutedEventArgs e) {
            frmPesquisaProduto pesquisa = new frmPesquisaProduto();
            pesquisa.ShowDialog();

            ProdutoPesquisa = pesquisa.produtoId;
            // Se vier 0, então a pessoa fechou sem escolher nenhum item. Então ele não vai fazer nada.
            if (ProdutoPesquisa != 0) {
                Produto produto = produtoDAO.BuscarProdutoPeloId(ProdutoPesquisa);
                PopularCamposPeloProduto(produto);
                MudarOperacao(2);
            }
        }


        private void BtnEditar_Click(object sender, RoutedEventArgs e) {
            AtualizarCategoriasEUnidades();
            MudarOperacao(3);
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Nenhum item selecionado!");
            } else {
                if (WPFUtils.MostrarCaixaDeTextoDeConfirmação("Tem certeza que deseja excluir esse produto? Essa operação não poderá ser desfeita!")) {
                    produtoDAO.Excluir(Convert.ToInt32(txtCodigo.Text));
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
                    if (ValidarCamposObrigatorios()) {
                        Produto produto = CriarProdutoComOsDadosDaTela();
                        produto = produtoDAO.Inserir(produto);
                        PopularCamposPeloProduto(produto);
                        WPFUtils.MostrarCaixaDeTextoDeInformação("Produto cadastrado com sucesso!");
                        MudarOperacao(2);
                    }
                    break;
                case 3: //EDIÇÃO
                    if (ValidarCamposObrigatorios()) {
                        Produto produto = CriarProdutoComOsDadosDaTela();
                        cbUnidadeDeMedida.IsEnabled = false;
                        produtoDAO.Editar(produto);
                        WPFUtils.MostrarCaixaDeTextoDeInformação("Produto atualizado com sucesso!");
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
         * Método que valida se todos os campos obrigatórios foram ou não preenchidos.
         * Retorna verdadeiro se a validação for positiva, ou falso caso algum campo não tenha sido preenchido/escolhido.
         */
        private bool ValidarCamposObrigatorios() {
            if (string.IsNullOrWhiteSpace(txtNome.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Informe o nome do produto!");
                txtNome.Focus();
                return false;
            } else if (string.IsNullOrWhiteSpace(txtPrecoVenda.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Informe o preço de venda do produto!");
                txtPrecoVenda.Focus();
                return false;
            } else if (cbCategoria.SelectedItem == null) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Selecione a categoria do produto!");
                cbCategoria.Focus();
                return false;
            } else if (cbUnidadeDeMedida.SelectedItem == null) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Selecione a unidade de medida do produto!");
                cbUnidadeDeMedida.Focus();
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
            txtPrecoCusto.Clear();
            txtPrecoVenda.Clear();
            txtQuantidadeDisponivel.Clear();
            cbCategoria.SelectedItem = null;
            cbUnidadeDeMedida.SelectedItem = null;
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
                    panelBotoesCompraEVenda.Visibility = Visibility.Hidden;
                    panelBotoesCompraEVenda.IsEnabled = false;
                    break;
                case 1: //ADIÇÃO: BOTÕES DE SALVAR E CANCELAR ATIVOS. TELA ATIVA.
                    btnAdicionar.IsEnabled = false;
                    btnLocalizar.IsEnabled = false;
                    btnEditar.IsEnabled = false;
                    btnExcluir.IsEnabled = false;
                    btnSalvar.IsEnabled = true;
                    btnCancelar.IsEnabled = true;
                    panelContent.IsEnabled = true;
                    panelBotoesCompraEVenda.Visibility = Visibility.Hidden;
                    panelBotoesCompraEVenda.IsEnabled = false;
                    break;
                case 2: //PRODUTO EM TELA: BOTÕES DE EXCLUIR, EDITAR, CANCELAR ATIVOS. TELA BLOQUEADA.
                    btnAdicionar.IsEnabled = false;
                    btnLocalizar.IsEnabled = false;
                    btnEditar.IsEnabled = true;
                    btnExcluir.IsEnabled = true;
                    btnSalvar.IsEnabled = false;
                    btnCancelar.IsEnabled = true;
                    panelContent.IsEnabled = false;
                    panelBotoesCompraEVenda.Visibility = Visibility.Visible;
                    panelBotoesCompraEVenda.IsEnabled = true;
                    break;
                case 3: //EDIÇÃO: BOTÕES DE SALVAR E CANCELAR ATIVOS. TELA ATIVA.
                    btnAdicionar.IsEnabled = false;
                    btnLocalizar.IsEnabled = false;
                    btnEditar.IsEnabled = false;
                    btnExcluir.IsEnabled = false;
                    btnSalvar.IsEnabled = true;
                    btnCancelar.IsEnabled = true;
                    panelContent.IsEnabled = true;
                    panelBotoesCompraEVenda.Visibility = Visibility.Visible;
                    panelBotoesCompraEVenda.IsEnabled = false;
                    break;

            }
        }

        /**
         * Método que recebe os dados e cria uma nova categoria
         */
        private Produto CriarProdutoComOsDadosDaTela() {
            //O código está nulo ou vazio? A variável recebe 0, se está preenchido, recebe o valor que está lá.
            int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text);
            string nome = txtNome.Text;
            decimal valorPago = string.IsNullOrEmpty(txtPrecoCusto.Text) ? 0 : Convert.ToDecimal(txtPrecoCusto.Text);
            decimal valorvenda = string.IsNullOrEmpty(txtPrecoVenda.Text) ? 0 : Convert.ToDecimal(txtPrecoVenda.Text);
            UnidadeDeMedida unidadeDeMedida = (UnidadeDeMedida)cbUnidadeDeMedida.SelectedValue;
            Categoria categoria = (Categoria)cbCategoria.SelectedValue;
            return new Produto(nome, valorvenda, unidadeDeMedida, categoria, id, valorPago);
        }

        /**
         * Método que modifica os botões na tela e seta a variável operacao com a operação que está sendo realizada no momento.
         */
        private void MudarOperacao(int op) {
            operacao = op;
            ModificarBotoesFormulario(operacao);
        }

        private void AtualizarCategoriasEUnidades() {
            categorias = categoriaDAO.ListarCategorias();
            unidades = unidadeDeMedidaDAO.ListadeUnidades();

            cbCategoria.ItemsSource = categorias;
            cbUnidadeDeMedida.ItemsSource = unidades;
        }

        /**
         * Método que recebe um produto e preenche na tela os campos com suas informações.
         */
        private void PopularCamposPeloProduto(Produto produto) {
            txtCodigo.Text = produto.Id.ToString();
            txtNome.Text = produto.Nome;
            txtPrecoCusto.Text = produto.ValorPago.ToString();
            txtPrecoVenda.Text = produto.ValorVenda.ToString();

            txtQuantidadeDisponivel.Text = produtoDAO.CalcularSaldoDoProduto(produto).ToString();

            cbCategoria.SelectedItem = produto.Categoria;
            cbUnidadeDeMedida.SelectedItem = produto.UnidadeDeMedida;
        }

        private void BtnCompras_Click(object sender, RoutedEventArgs e) {
            Produto p = CriarProdutoComOsDadosDaTela();
            frmMovimentacaoProduto w = new frmMovimentacaoProduto(p, 1);
            w.ShowDialog();
        }

        private void BtnVendas_Click(object sender, RoutedEventArgs e) {
            Produto p = CriarProdutoComOsDadosDaTela();
            frmMovimentacaoProduto w = new frmMovimentacaoProduto(p, 2);
            w.ShowDialog();
        }

        private void BtnEstoque_Click(object sender, RoutedEventArgs e) {
            Produto p = CriarProdutoComOsDadosDaTela();
            frmDisponibilidadeProduto w = new frmDisponibilidadeProduto(p);
            w.ShowDialog();
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
