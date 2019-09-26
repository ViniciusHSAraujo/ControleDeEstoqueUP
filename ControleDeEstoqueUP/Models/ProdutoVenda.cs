using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ControleDeEstoqueUP.Models{
    [Table("ProdutosVenda")]
    public class ProdutoVenda {
        public ProdutoVenda() {
        }

        public ProdutoVenda(Produto produto, double quantidade, decimal valor) {
            Produto = produto;
            Quantidade = quantidade;
            Valor = valor;
        }

        public int Id { get; set; }

        public double Quantidade { get; set; }

        public decimal Valor { get; set; }

        public virtual Venda Venda { get; set; }

        public virtual Produto Produto { get; set; }

        public override bool Equals(object obj) {
            return obj is ProdutoVenda venda &&
                   Id == venda.Id;
        }

        public override int GetHashCode() {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
