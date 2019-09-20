using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ControleDeEstoqueUP.Models{
    [Table("ProdutosCompra")]
    public class ProdutoCompra{
		public int Id { get; set; }

        public double Quantidade { get; set; }

        public decimal Valor { get; set; }

        public virtual Compra Compra { get; set; }

        public virtual Produto Produto { get; set; }
    }
}
