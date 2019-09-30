using ControleDeEstoqueUP.DAL;
using ControleDeEstoqueUP.Models;
using ControleDeEstoqueUP.Utils;
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
    public partial class frmVenda : Window {

        /**
         * Operações:
         *  0: Nenhuma operação.
         *  1: Inserção
         *  2: Busca
         *  3: Edição
         */
        int operacao;
        VendaDAO vendaDAO = new VendaDAO();

        ClienteDAO clienteDAO= new ClienteDAO();
        Cliente clienteVenda;

        Funcionario funcionarioVenda;

        ICollection<ProdutoVenda> produtosDaVenda= new List<ProdutoVenda>();
        List<dynamic> produtosDaVendaGrid = new List<dynamic>();

        ProdutoDAO produtoDAO = new ProdutoDAO();
        SubProdutoDAO subProdutoDAO = new SubProdutoDAO();
        SubProduto subProdutoEscolhido;

        public int VendaPesquisa;
        int IdCliente;
        int IdSubProduto;


        public frmVenda() {
            InitializeComponent();
            MudarOperacao(0);
            gridProdutos.ItemsSource = produtosDaVendaGrid;
        }

        /**
         * BOTÕES
         */

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e) {
            MudarOperacao(1);
            LimparCampos();
            funcionarioVenda = frmLogin.funcionarioLogado;
            txtFuncionarioID.Text = funcionarioVenda.Id.ToString();
            txtFuncionarioNome.Text = funcionarioVenda.Nome.ToString();
        }

        private void BtnLocalizar_Click(object sender, RoutedEventArgs e) {
            var pesquisa = new frmPesquisaVenda();
            pesquisa.ShowDialog();

            VendaPesquisa = pesquisa.vendaId;
            // Se vier 0, então a pessoa fechou sem escolher nenhum item. Então ele não vai fazer nada.
            if (VendaPesquisa != 0) {
                Venda venda = vendaDAO.BuscarVendaPorId(VendaPesquisa);
                PopularCamposPelaVenda(venda);
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
                if (WPFUtils.MostrarCaixaDeTextoDeConfirmação("Tem certeza que deseja excluir essa venda? Essa operação não poderá ser desfeita e o saldo será deduzido na hora!")) {
                    vendaDAO.Excluir(Convert.ToInt32(txtCodigo.Text));
                    WPFUtils.MostrarCaixaDeTextoDeInformação("Venda excluída com sucesso!");
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
                        var venda = CriarVendaComOsDadosDaTela();
                        vendaDAO.Inserir(venda);
                        PopularCamposPelaVenda(venda);
                        WPFUtils.MostrarCaixaDeTextoDeInformação("Venda cadastrada com sucesso!");
                        MudarOperacao(2);
                    }
                    break;
                case 3: //EDIÇÃO
                    if (ValidarCamposObrigatorios()) {
                        var venda = CriarVendaComOsDadosDaTela();
                        vendaDAO.Editar(venda);
                        WPFUtils.MostrarCaixaDeTextoDeInformação("Venda atualizada com sucesso!");
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
            } else if (string.IsNullOrWhiteSpace(txtClienteID.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Cliente não escolhido!");
                txtClienteID.Focus();
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
            txtClienteID.Clear();
            txtClienteNome.Clear();
            txtFuncionarioID.Clear();
            txtFuncionarioNome.Clear();
            txtNomeProduto.Clear();
            txtTotal.Clear();
            txtValor.Clear();
            gridProdutos.ItemsSource = null;
            produtosDaVenda.Clear();
            produtosDaVendaGrid.Clear();
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
        private Venda CriarVendaComOsDadosDaTela() {
            //O código está nulo ou vazio? A variável recebe 0, se está preenchido, recebe o valor que está lá.
            int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text);
            var cliente = clienteVenda;
            var funcionario = funcionarioVenda;
            var data = txtData.SelectedDate.Value;
            var valorTotal = Convert.ToDecimal(txtTotal.Text);
            var produtosVenda = produtosDaVenda;
            return new Venda(cliente, funcionario, data, valorTotal, produtosVenda, id);
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
        private void PopularCamposPelaVenda(Venda venda) {
            txtCodigo.Text = venda.Id.ToString();
            txtClienteID.Text = venda.Cliente.Id.ToString();
            txtClienteNome.Text = venda.Cliente.Nome.ToString();
            txtFuncionarioID.Text = venda.Funcionario.Id.ToString();
            txtFuncionarioNome.Text = venda.Funcionario.Nome.ToString();
            txtData.SelectedDate = venda.Data;
            txtTotal.Text = venda.Total.ToString();
            PopularGridDeItensPelaVenda(venda);
        }

        private void PopularGridDeItensPelaVenda(Venda venda) {
            produtosDaVenda = venda.ProdutosVenda;
            produtosDaVendaGrid.Clear();
            foreach (var produtoVenda in produtosDaVenda) {
                produtosDaVendaGrid.Add(new { ProdutoID = produtoVenda.SubProduto.Id, ProdutoNome = produtoVenda.SubProduto.Produto.Nome, produtoVenda.SubProduto.SKU, produtoVenda.Valor });
            }
            AtualizarGrid();
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
        private void TxtClienteID_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var pesquisa = new frmPesquisarCliente();
            pesquisa.ShowDialog();
            IdCliente = pesquisa.clienteId;
            if (IdCliente != 0) {
                clienteVenda = clienteDAO.BuscarClientePeloId(IdCliente);
                txtClienteID.Text = clienteVenda.Id.ToString();
                txtClienteNome.Text = clienteVenda.Nome;
            } else {
                WPFUtils.MostrarCaixaDeTextoDeErro("Nenhum cliente escolhido!");
                clienteVenda = null;
                txtClienteID.Clear();
                txtClienteNome.Clear();
            }
            
        }

        /**
         * Quando o campo de ID do Fornecedor perde o foco.
         */
        private void TxtClienteID_LostFocus(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(txtClienteID.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeErro("Nenhum cliente escolhido!");
                clienteVenda = null;
                txtClienteNome.Clear();
                txtClienteID.Clear();
            } else {
                IdCliente = Convert.ToInt32(txtClienteID.Text);
                clienteVenda = clienteDAO.BuscarClientePeloId(IdCliente);
                if (clienteVenda == null) {
                    WPFUtils.MostrarCaixaDeTextoDeErro("Código de cliente inválido!");
                    clienteVenda = null;
                    txtClienteNome.Clear();
                    txtClienteID.Clear();
                } else {
                    txtClienteNome.Text = clienteVenda.Nome;
                }
            }
        }

        /**
         * Quando se clica duas vezes no campo de ID do Produto
         */
        private void TxtCodigoProduto_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var pesquisa = new frmPesquisaProdutoEspecifico();
            pesquisa.ShowDialog();
            IdSubProduto = pesquisa.subProdutoId;
            if (IdSubProduto != 0) {
                subProdutoEscolhido = subProdutoDAO.BuscarSubProdutoPeloId(IdSubProduto);
                txtCodigoProduto.Text = subProdutoEscolhido.Id.ToString();
                txtNomeProduto.Text = subProdutoEscolhido.Produto.Nome;
                txtValor.Text = subProdutoEscolhido.Produto.ValorVenda.ToString("F2");
                txtSKU.Text = subProdutoEscolhido.SKU;
            } else {
                WPFUtils.MostrarCaixaDeTextoDeErro("Nenhum produto escolhido!");
                subProdutoEscolhido = null;
                txtCodigoProduto.Clear();
                txtNomeProduto.Clear();
                txtValor.Clear();
                txtSKU.Clear();
            }
        }

        /**
         * Quando o campo de ID do Produto perde o foco.
         */
        private void TxtCodigoProduto_LostFocus(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(txtCodigoProduto.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeErro("Nenhum produto escolhido!");
                subProdutoEscolhido = null;
                txtCodigoProduto.Clear();
                txtNomeProduto.Clear();
                txtValor.Clear();
                txtSKU.Clear();
            } else {
                IdSubProduto = Convert.ToInt32(txtCodigoProduto.Text);
                subProdutoEscolhido = subProdutoDAO.BuscarSubProdutoPeloId(IdSubProduto);
                if (subProdutoEscolhido == null) {
                    WPFUtils.MostrarCaixaDeTextoDeErro("Código de produto inválido!");
                    subProdutoEscolhido = null;
                    txtCodigoProduto.Clear();
                    txtNomeProduto.Clear();
                    txtValor.Clear();
                    txtSKU.Clear();
                } else if (subProdutoEscolhido.Status == false) {
                    WPFUtils.MostrarCaixaDeTextoDeErro("Esse código se refere a um produto já vendido!");
                    subProdutoEscolhido = null;
                    txtCodigoProduto.Clear();
                    txtNomeProduto.Clear();
                    txtValor.Clear();
                    txtSKU.Clear();
                } else if (produtosDaVenda.Any(p => p.SubProduto.Id == subProdutoEscolhido.Id)) {
                    WPFUtils.MostrarCaixaDeTextoDeErro("O produto com o código em questão já foi selecionado nessa venda!");
                    subProdutoEscolhido = null;
                    txtCodigoProduto.Clear();
                    txtNomeProduto.Clear();
                    txtValor.Clear();
                    txtSKU.Clear();
                } else {
                    txtNomeProduto.Text = subProdutoEscolhido.Produto.Nome;
                    txtValor.Text = subProdutoEscolhido.Produto.ValorVenda.ToString("F2");
                    txtSKU.Text = subProdutoEscolhido.SKU;
                }
            }
        }

        private void BtnAddItem_Click(object sender, RoutedEventArgs e) {
            if (ValidarCamposObrigatoriosDoProduto()) {
                var produtoVenda = CriarProdutoVendaComOsDadosDaTela();
                    produtosDaVenda.Add(produtoVenda);
                    produtosDaVendaGrid.Add(new { ProdutoID = produtoVenda.SubProduto.Id, ProdutoNome = produtoVenda.SubProduto.Produto.Nome, SKU = produtoVenda.SubProduto.SKU,produtoVenda.Valor });
                    AtualizarGrid();
                    LimparCamposDoProduto();
                    RecalcularValorTotal();
                    txtCodigoProduto.Focus();
            }
        }

        private void AtualizarGrid() {
            gridProdutos.ItemsSource = produtosDaVendaGrid;
            gridProdutos.Items.Refresh();
        }

        private bool ValidarCamposObrigatoriosDoProduto() {
            if (string.IsNullOrWhiteSpace(txtCodigoProduto.Text) || string.IsNullOrWhiteSpace(txtNomeProduto.Text)) {
                WPFUtils.MostrarCaixaDeTextoDeAlerta("Nenhum produto escolhido!");
                txtCodigoProduto.Focus();
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
            txtSKU.Clear();
            txtValor.Clear();
        }

        private ProdutoVenda CriarProdutoVendaComOsDadosDaTela() {
            var subproduto = subProdutoEscolhido;
            var valor = Convert.ToDecimal(txtValor.Text);
            return new ProdutoVenda(subproduto, valor);
        }

        private void BtnRemoveItem_Click(object sender, RoutedEventArgs e) {

            if (gridProdutos.SelectedItem == null) {
                WPFUtils.MostrarCaixaDeTextoDeErro("Nenhum item foi selecionado!");
            } else {
                produtosDaVenda.Remove(gridProdutos.SelectedValue as ProdutoVenda);
                produtosDaVendaGrid.RemoveAt(gridProdutos.SelectedIndex);
                AtualizarGrid();
                RecalcularValorTotal();
            }

        }

        private void RecalcularValorTotal() {
            decimal valorTotal = 0;
            foreach (var produto in produtosDaVendaGrid) {
                valorTotal += produto.Valor;
            }
            txtTotal.Text = valorTotal.ToString("F2");
        }
    }
}
