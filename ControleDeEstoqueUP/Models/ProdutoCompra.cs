using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ControleDeEstoqueUP.Models{
    [Table("ProdutosCompra")]
    public class ProdutoCompra{

        public ProdutoCompra() {
        }

        public ProdutoCompra(Produto produto, double quantidade, decimal valor) {
            Produto = produto;
            Quantidade = quantidade;
            Valor = valor;
        }

        public ProdutoCompra(Produto produto, double quantidade, decimal valor, Compra compra, int id = 0) {
            Produto = produto;
            Quantidade = quantidade;
            Valor = valor;
            Compra = compra;
            Id = id;
        }


        public int Id { get; set; }

        public double Quantidade { get; set; }

        public decimal Valor { get; set; }

        public virtual Compra Compra { get; set; }

        public virtual Produto Produto { get; set; }

        public override bool Equals(object obj) {
            return obj is ProdutoCompra compra &&
                   Id == compra.Id;
        }

        public override int GetHashCode() {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
