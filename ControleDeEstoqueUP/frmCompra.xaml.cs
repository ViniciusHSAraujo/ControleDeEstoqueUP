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
        Fornecedor fornecedor;

        ProdutoDAO produtoDAO = new ProdutoDAO();
        Produto produto;

        public int CompraPesquisa;
        int IdFornecedor;
        int IdProduto;


        public frmCompra() {
            InitializeComponent();
            MudarOperacao(0);
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
          /*  if (string.IsNullOrWhiteSpace(txtCodigo.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Nenhum item selecionado!");
            } else {
                if (WPFUtils.MostrarCaixaDeTextoDeConfirmação("Tem certeza que deseja excluir esse produto? Essa operação não poderá ser desfeita!")) {
                    //produtoDAO.Excluir(Convert.ToInt32(txtCodigo.Text));
                    WPFUtils.MostrarCaixaDeTextoDeInformação("Item excluído com sucesso!");
                    operacao = 0;
                    ModificarBotoesFormulario(operacao);
                    LimparCampos();
                } else {
                    WPFUtils.MostrarCaixaDeTextoDeInformação("Operação cancelada!");
                }
            }*/
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e) {
           /* switch (operacao) {
                case 1: //ADIÇÃO
                    if (!ValidarCamposObrigatorios()) {
                        var produto = CriarProdutoComOsDadosDaTela();
                        //produto = produtoDAO.Inserir(produto);
                        PopularCamposPeloProduto(produto);
                        WPFUtils.MostrarCaixaDeTextoDeInformação("Produto cadastrado com sucesso!");
                        MudarOperacao(2);
                    }
                    break;
                case 3: //EDIÇÃO
                    if (!ValidarCamposObrigatorios()) {
                        var produto = CriarProdutoComOsDadosDaTela();
                        cbUnidadeDeMedida.IsEnabled = false;
                        //produtoDAO.Editar(produto);
                        WPFUtils.MostrarCaixaDeTextoDeInformação("Produto atualizado com sucesso!");
                        MudarOperacao(2);
                    }
                    break;
            }*/
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e) {
            MudarOperacao(0);
            LimparCampos();

        }

        /**
         * Método que valida se todos os campos obrigatórios foram ou não preenchidos.
         * Retorna verdadeiro se a validação for positiva, ou falso caso algum campo não tenha sido preenchido/escolhido.
         */
      /*  private bool ValidarCamposObrigatorios() {
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
        }  */

        /**
         * Método que limpa os campos da tela.
         */
        private void LimparCampos() {
            txtCodigo.Clear();
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
      /*  private Produto CriarProdutoComOsDadosDaTela() {
            //O código está nulo ou vazio? A variável recebe 0, se está preenchido, recebe o valor que está lá.
            int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text);
            string nome = txtNome.Text;
            decimal valorPago = string.IsNullOrEmpty(txtPrecoCusto.Text) ? 0 : Convert.ToDecimal(txtPrecoCusto.Text);
            decimal valorvenda = string.IsNullOrEmpty(txtPrecoVenda.Text) ? 0 : Convert.ToDecimal(txtPrecoVenda.Text);
            //UnidadeDeMedida unidadeDeMedida = UnidadeDeMedidaDAO.;
            UnidadeDeMedida unidadeDeMedida = null;
            Categoria categoria = (Categoria) cbCategoria.SelectedValue;
            double quantidade = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : Convert.ToDouble(txtQuantidadeDisponivel.Text);
            return new Produto(nome, valorvenda, unidadeDeMedida,categoria,id,valorPago,quantidade);
        }*/

        /**
         * Método que modifica os botões na tela e seta a variável operacao com a operação que está sendo realizada no momento.
         */
        private void MudarOperacao(int op) {
            operacao = op;
            ModificarBotoesFormulario(operacao);
        }

        /**
         * Método que recebe um produto e preenche na tela os campos com suas informações.
         */
        private void PopularCamposPeloProduto(Produto produto) {
            txtCodigo.Text = produto.Id.ToString();
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

        /**
         * Quando se clica duas vezes no campo de ID do Fornecedor
         */
        private void TxtFornecedorID_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var pesquisa = new frmPesquisaFornecedor();
            pesquisa.ShowDialog();
            IdFornecedor = pesquisa.fornecedorId;
            if (IdFornecedor != 0) {
                fornecedor = fornecedorDAO.BuscarFornecedorPorId(IdFornecedor);
                txtFornecedorID.Text = fornecedor.Id.ToString();
                txtFornecedorNome.Text = fornecedor.RazaoSocial;
            } else {
                WPFUtils.MostrarCaixaDeTextoDeErro("Nenhum fornecedor escolhido!");
                fornecedor = null;
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
                fornecedor = null;
                txtFornecedorNome.Clear();
                txtFornecedorID.Clear();
            } else {
                IdFornecedor = Convert.ToInt32(txtFornecedorID.Text);
                fornecedor = fornecedorDAO.BuscarFornecedorPorId(IdFornecedor);
                if (fornecedor == null) {
                    WPFUtils.MostrarCaixaDeTextoDeErro("Código de fornecedor inválido!");
                    fornecedor = null;
                    txtFornecedorNome.Clear();
                    txtFornecedorID.Clear();
                } else {
                    txtFornecedorNome.Text = fornecedor.RazaoSocial;
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
                produto = produtoDAO.BuscarProdutoPeloId(IdProduto);
                txtCodigoProduto.Text = produto.Id.ToString();
                txtNomeProduto.Text = produto.Nome;
            } else {
                WPFUtils.MostrarCaixaDeTextoDeErro("Nenhum produto escolhido!");
                produto = null;
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
                produto = null;
                txtCodigoProduto.Clear();
                txtNomeProduto.Clear();
            } else {
                IdProduto = Convert.ToInt32(txtCodigoProduto.Text);
                produto = produtoDAO.BuscarProdutoPeloId(IdProduto);
                if (produto == null) {
                    WPFUtils.MostrarCaixaDeTextoDeErro("Código de produto inválido!");
                    produto = null;
                    txtCodigoProduto.Clear();
                    txtNomeProduto.Clear();
                } else {
                    txtNomeProduto.Text = produto.Nome;
                }
            }
        }
    }
}
