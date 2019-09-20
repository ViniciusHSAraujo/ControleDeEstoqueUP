using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ControleDeEstoqueUP.Models{
    [Table("Vendas")]
    public class Venda : Movimentacao{
		public virtual ICollection<ProdutoVenda> ProdutosVenda { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}
