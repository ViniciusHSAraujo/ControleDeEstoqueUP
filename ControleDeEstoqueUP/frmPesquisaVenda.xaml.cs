﻿using ControleDeEstoqueUP.DAL;
using ControleDeEstoqueUP.Models;
using ControleDeEstoqueUP.Utils;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for frmBasePesquisa.xaml
    /// </summary>
    public partial class frmPesquisaVenda : Window {

        VendaDAO vendaDAO = new VendaDAO();

        private List<Venda> vendas;

        public int vendaId;

        public frmPesquisaVenda() {
            InitializeComponent();
            vendas = vendaDAO.ListarVendas();
            gridResultados.ItemsSource = vendas.ToList();
        }

        private void BtnLocalizar_Click(object sender, RoutedEventArgs e) {

            switch (ParametroEscolha.SelectedIndex) {
                case 0: //Geral
                    txtPesquisa.Clear();
                    gridResultados.ItemsSource = vendas.ToList();
                    break;
                case 1: //ID
                    gridResultados.ItemsSource = vendas.Where(c => c.Id.ToString().Contains(txtPesquisa.Text)).ToList();
                    break;
                case 2: //NOME DO CLIENTE
                    gridResultados.ItemsSource = vendas.Where(c => c.Cliente.Nome.ToLower().Contains((txtPesquisa.Text.ToLower()))).ToList();
                    break;
                case 3: //NOME DO FUNCIONARIO
                    gridResultados.ItemsSource = vendas.Where(c => c.Funcionario.Nome.ToLower().Contains((txtPesquisa.Text.ToLower()))).ToList();
                    break;
                case 4: //DATA DA COMPRA
                    gridResultados.ItemsSource = vendas.Where(c => c.Data.ToString().Equals(txtPesquisa.Text)).ToList();
                    break;
                default:
                    break;
            }
        }

        private void BtnAbrir_Click(object sender, RoutedEventArgs e) {
            if (gridResultados.SelectedItem == null) {
                WPFUtils.MostrarCaixaDeTextoDeErro("Nenhum item selecionado.");
            } else {
                var selecao = gridResultados.SelectedItem;
                Venda venda = selecao as Venda;

                vendaId = venda.Id;
                Close();
            }
        }
    }
}
