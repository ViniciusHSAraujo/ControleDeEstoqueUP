using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeEstoqueUP.Models {
    [Table("Compras")]
    public class Compra : Movimentacao {

        public Compra() {
        }

        public Compra(Fornecedor fornecedor, Funcionario funcionario, DateTime data, decimal valorTotal, ICollection<ProdutoCompra> produtosCompra, int id = 0) {
            Fornecedor = fornecedor;
            Funcionario = funcionario;
            Data = data;
            Total = valorTotal;
            ProdutosCompra = produtosCompra;
        }

        public virtual ICollection<ProdutoCompra> ProdutosCompra { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }
    }
}
