using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ControleDeEstoqueUP.Models{
    [Table("ProdutosVenda")]
    public class ProdutoVenda {
        public ProdutoVenda() {
        }

        public ProdutoVenda(SubProduto subProduto, decimal valor) {
            SubProduto = subProduto;
            Valor = valor;
        }

        public int Id { get; set; }

        public decimal Valor { get; set; }

        public virtual Venda Venda { get; set; }

        public  SubProduto SubProduto { get; set; }

        public override bool Equals(object obj) {
            return obj is ProdutoVenda venda &&
                   Id == venda.Id;
        }

        public override int GetHashCode() {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
