using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ControleDeEstoqueUP.Models{
    [Table("Vendas")]
    public class Venda : Movimentacao{
        private decimal valorTotal;
        private ICollection<ProdutoCompra> produtosCompra;

        public Venda(Cliente cliente, Funcionario funcionario, DateTime data, decimal valorTotal, ICollection<ProdutoVenda> produtosVenda, int id) {
            Cliente = cliente;
            Funcionario = funcionario;
            Data = data;
            Total = valorTotal;
            ProdutosVenda = produtosVenda;
            Id = id;
        }

        public virtual ICollection<ProdutoVenda> ProdutosVenda { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}
