using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ControleDeEstoqueUP.Models{
    [Table("Compras")]
    public class Compra : Movimentacao{

        public Compra() {
        }

		public virtual ICollection<ProdutoCompra> ProdutosCompra { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }
    }
}
