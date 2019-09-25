using ControleDeEstoqueUP.DAL;
using ControleDeEstoqueUP.Models;
using ControleSeuEstoque.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class frmCompra : Window {

        /**
         * Operações:
         *  0: Nenhuma operação.
         *  1: Inserção
         *  2: Busca
         *  3: Edição
         */
        int operacao;
        CompraDAO compraDAO = new CompraDAO();

        FornecedorDAO fornecedorDAO = new FornecedorDAO();
        Fornecedor fornecedorCompra;

        Funcionario funcionarioCompra;

        ICollection<ProdutoCompra> produtosDaCompra= new List<ProdutoCompra>();
        List<dynamic> produtosDaCompraGrid= new List<dynamic>();

        ProdutoDAO produtoDAO = new ProdutoDAO();
        Produto produtoEscolhido;

        public int CompraPesquisa;
        int IdFornecedor;
        int IdProduto;


        public frmCompra() {
            InitializeComponent();
            MudarOperacao(0);
            gridProdutos.ItemsSource = produtosDaCompraGrid;
        }

        /**
         * BOTÕES
         */

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e) {
            MudarOperacao(1);
            LimparCampos();
            //txtFuncionarioID.Text = frmLogin.funcionarioLogado.Id.ToString();
            //txtFuncionarioNome.Text = frmLogin.funcionarioLogado.Nome.ToString();
        }

        private void BtnLocalizar_Click(object sender, RoutedEventArgs e) {
            var pesquisa = new frmPesquisaProduto();
            pesquisa.ShowDialog();

            CompraPesquisa = pesquisa.produtoId;
            // Se vier 0, então a pessoa fechou sem escolher nenhum item. Então ele não vai fazer nada.
            if (CompraPesquisa != 0) {
                Compra compra = compraDAO.BuscarCompraPorId(CompraPesquisa);
                //PopularCamposPeloProduto(compra);
                MudarOperacao(2);
            }
        }

        
        private void BtnEditar_Click(object sender, RoutedEventArgs e) {
            MudarOperacao(3);
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Nenhuma compra selecionada!");
            } else {
                if (WPFUtils.MostrarCaixaDeTextoDeConfirmação("Tem certeza que deseja excluir essa compra? Essa operação não poderá ser desfeita!")) {
                    compraDAO.Excluir(Convert.ToInt32(txtCodigo.Text));
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
                        var compra = CriarCompraComOsDadosDaTela();
                        compraDAO.Inserir(compra);
                        PopularCamposPelaCompra(compra);
                        WPFUtils.MostrarCaixaDeTextoDeInformação("Compra cadastrada com sucesso!");
                        MudarOperacao(2);
                    }
                    break;
                case 3: //EDIÇÃO
                    if (ValidarCamposObrigatorios()) {
                        var compra = CriarCompraComOsDadosDaTela();
                        compraDAO.Editar(compra);
                        WPFUtils.MostrarCaixaDeTextoDeInformação("Compra atualizada com sucesso!");
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
            if (string.IsNullOrWhiteSpace(txtFuncionarioID.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Funcionário não escolhido!");
                txtFuncionarioID.Focus();
                return false;
            } else if (string.IsNullOrWhiteSpace(txtFornecedorID.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Fornecedor não escolhido!");
                txtFornecedorID.Focus();
                return false;
            } else if (txtData.SelectedDate == null) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Nenhuma data selecionada!");
                txtData.Focus();
                return false;
            } else if (!gridProdutos.HasItems) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Nenhum produto adicionado!");
                return false;
            } else {
                return true;
            }
        }  

        /**
         * Método que limpa os campos da tela.
         */
        private void LimparCampos() {
            txtCodigo.Clear();
            txtCodigoProduto.Clear();
            txtData.SelectedDate = null;
            txtFornecedorID.Clear();
            txtFornecedorNome.Clear();
            txtFuncionarioID.Clear();
            txtFuncionarioNome.Clear();
            txtNomeProduto.Clear();
            txtQuantidade.Clear();
            txtTotal.Clear();
            txtValor.Clear();
            gridProdutos.ItemsSource = null;
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
         * Método que recebe os dados e cria uma nova compra
         */
        private Compra CriarCompraComOsDadosDaTela() {
            //O código está nulo ou vazio? A variável recebe 0, se está preenchido, recebe o valor que está lá.
            int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text);
            var fornecedor = fornecedorCompra;
            var funcionario = funcionarioCompra;
            var data = txtData.SelectedDate.Value;
            var valorTotal = Convert.ToDecimal(txtTotal.Text);
            var produtosCompra = produtosDaCompra;
            return new Compra(fornecedor, funcionario, data, valorTotal, produtosCompra, id);
        }

        /**
         * Método que modifica os botões na tela e seta a variável operacao com a operação que está sendo realizada no momento.
         */
        private void MudarOperacao(int op) {
            operacao = op;
            ModificarBotoesFormulario(operacao);
        }

        /**
         * Método que recebe uma compra e preenche na tela os campos com suas informações.
         */
        private void PopularCamposPelaCompra(Compra compra) {
            txtCodigo.Text = compra.Id.ToString();
            txtFornecedorID.Text = compra.Fornecedor.Id.ToString();
            txtFornecedorNome.Text = compra.Fornecedor.RazaoSocial.ToString();
            //txtFuncionarioID.Text = compra.Funcionario.Id.ToString();
            //txtFuncionarioNome.Text = compra.Funcionario.Nome.ToString();
            txtData.SelectedDate = compra.Data;
            txtTotal.Text = compra.Total.ToString();
            PopularGridDeItensPelaCompra(compra);
        }

        private void PopularGridDeItensPelaCompra(Compra compra) {
            produtosDaCompra = compra.ProdutosCompra;
            produtosDaCompraGrid.Clear();
            foreach (var produtoCompra in produtosDaCompra) {
                produtosDaCompraGrid.Add(new { ProdutoID = produtoCompra.Produto.Id, ProdutoNome = produtoCompra.Produto.Nome, produtoCompra.Quantidade, produtoCompra.Valor, Subtotal = Convert.ToDecimal(produtoCompra.Quantidade) * produtoCompra.Valor });
            }
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

        /*
         * ___  ___            _         _              _        ______                   _         _          
         * |  \/  |           | |       | |            | |       | ___ \                 | |       | |         
         * | .  . |  ___    __| | _   _ | |  ___     __| |  ___  | |_/ / _ __   ___    __| | _   _ | |_   ___  
         * | |\/| | / _ \  / _` || | | || | / _ \   / _` | / _ \ |  __/ | '__| / _ \  / _` || | | || __| / _ \ 
         * | |  | || (_) || (_| || |_| || || (_) | | (_| ||  __/ | |    | |   | (_) || (_| || |_| || |_ | (_) |
         * \_|  |_/ \___/  \__,_| \__,_||_| \___/   \__,_| \___| \_|    |_|    \___/  \__,_| \__,_| \__| \___/ 
         */

        /**
         * Quando se clica duas vezes no campo de ID do Fornecedor
         */
        private void TxtFornecedorID_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var pesquisa = new frmPesquisaFornecedor();
            pesquisa.ShowDialog();
            IdFornecedor = pesquisa.fornecedorId;
            if (IdFornecedor != 0) {
                fornecedorCompra = fornecedorDAO.BuscarFornecedorPorId(IdFornecedor);
                txtFornecedorID.Text = fornecedorCompra.Id.ToString();
                txtFornecedorNome.Text = fornecedorCompra.RazaoSocial;
            } else {
                WPFUtils.MostrarCaixaDeTextoDeErro("Nenhum fornecedor escolhido!");
                fornecedorCompra = null;
                txtFornecedorNome.Clear();
                txtFornecedorID.Clear();
            }
            
        }

        /**
         * Quando o campo de ID do Fornecedor perde o foco.
         */
        private void TxtFornecedorID_LostFocus(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(txtFornecedorID.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeErro("Nenhum fornecedor escolhido!");
                fornecedorCompra = null;
                txtFornecedorNome.Clear();
                txtFornecedorID.Clear();
            } else {
                IdFornecedor = Convert.ToInt32(txtFornecedorID.Text);
                fornecedorCompra = fornecedorDAO.BuscarFornecedorPorId(IdFornecedor);
                if (fornecedorCompra == null) {
                    WPFUtils.MostrarCaixaDeTextoDeErro("Código de fornecedor inválido!");
                    fornecedorCompra = null;
                    txtFornecedorNome.Clear();
                    txtFornecedorID.Clear();
                } else {
                    txtFornecedorNome.Text = fornecedor.Nome;
                }
            }
        }

        /**
         * Quando se clica duas vezes no campo de ID do Produto
         */
        private void TxtCodigoProduto_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var pesquisa = new frmPesquisaProduto();
            pesquisa.ShowDialog();
            IdProduto = pesquisa.produtoId;
            if (IdProduto != 0) {
                produtoEscolhido = produtoDAO.BuscarProdutoPeloId(IdProduto);
                txtCodigoProduto.Text = produtoEscolhido.Id.ToString();
                txtNomeProduto.Text = produtoEscolhido.Nome;
            } else {
                WPFUtils.MostrarCaixaDeTextoDeErro("Nenhum produto escolhido!");
                produtoEscolhido = null;
                txtCodigoProduto.Clear();
                txtNomeProduto.Clear();
            }
        }

        /**
         * Quando o campo de ID do Produto perde o foco.
         */
        private void TxtCodigoProduto_LostFocus(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(txtCodigoProduto.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeErro("Nenhum produto escolhido!");
                produtoEscolhido = null;
                txtCodigoProduto.Clear();
                txtNomeProduto.Clear();
            } else {
                IdProduto = Convert.ToInt32(txtCodigoProduto.Text);
                produtoEscolhido = produtoDAO.BuscarProdutoPeloId(IdProduto);
                if (produtoEscolhido == null) {
                    WPFUtils.MostrarCaixaDeTextoDeErro("Código de produto inválido!");
                    produtoEscolhido = null;
                    txtCodigoProduto.Clear();
                    txtNomeProduto.Clear();
                } else {
                    txtNomeProduto.Text = produtoEscolhido.Nome;
                }
            }
        }

        private void BtnAddItem_Click(object sender, RoutedEventArgs e) {
            if (ValidarCamposObrigatoriosDoProduto()) {
                var produtoCompra = CriarProdutoCompraComOsDadosDaTela();
                produtosDaCompra.Add(produtoCompra);
                produtosDaCompraGrid.Add(new { ProdutoID = produtoCompra.Produto.Id, ProdutoNome = produtoCompra.Produto.Nome, produtoCompra.Quantidade, produtoCompra.Valor, Subtotal = Convert.ToDecimal(produtoCompra.Quantidade) * produtoCompra.Valor });
                AtualizarGrid();
                LimparCamposDoProduto();
                RecalcularValorTotal();
                txtCodigoProduto.Focus();
            }
        }

        private void AtualizarGrid() {
            gridProdutos.ItemsSource = produtosDaCompraGrid;
            gridProdutos.Items.Refresh();
        }

        private bool ValidarCamposObrigatoriosDoProduto() {
            if (string.IsNullOrWhiteSpace(txtCodigoProduto.Text) || string.IsNullOrWhiteSpace(txtNomeProduto.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Nenhum produto escolhido!");
                txtCodigoProduto.Focus();
                return false;
            } else if (string.IsNullOrWhiteSpace(txtQuantidade.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Quantidade não definida!");
                txtQuantidade.Focus();
                return false;
            } else if (string.IsNullOrWhiteSpace(txtValor.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Valor do produto não definido!");
                txtValor.Focus();
                return false;
            } else {
                return true;
            }
        }

        private void LimparCamposDoProduto() {
            txtCodigoProduto.Clear();
            txtNomeProduto.Clear();
            txtQuantidade.Clear();
            txtValor.Clear();
        }

        private ProdutoCompra CriarProdutoCompraComOsDadosDaTela() {
            var produto = produtoEscolhido;
            var quantidade = Convert.ToDouble(txtQuantidade.Text);
            var valor = Convert.ToDecimal(txtValor.Text);
            return new ProdutoCompra(produto, quantidade, valor);
        }

        private void BtnRemoveItem_Click(object sender, RoutedEventArgs e) {

            if (gridProdutos.SelectedItem == null) {
                WPFUtils.MostrarCaixaDeTextoDeErro("Nenhum item foi selecionado!");
            } else {
                produtosDaCompra.Remove(gridProdutos.SelectedValue as ProdutoCompra);
                produtosDaCompraGrid.RemoveAt(gridProdutos.SelectedIndex);
                AtualizarGrid();
                RecalcularValorTotal();
            }

        }

        private void RecalcularValorTotal() {
            decimal valorTotal = 0;
            foreach (var produto in produtosDaCompraGrid) {
                valorTotal += produto.Subtotal;
            }
            txtTotal.Text = valorTotal.ToString("F2");
        }
    }
}
