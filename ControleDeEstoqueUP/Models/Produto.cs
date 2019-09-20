using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ControleDeEstoqueUP.Models{
    [Table("Produtos")]
    public class Produto{
		public int Id { get; set; }

        public string Nome { get; set; }

        public decimal ValorPago { get; set; }

        public decimal ValorVenda { get; set; }

        public double Quantidade { get; set; }

        public virtual UnidadeDeMedida UnidadeDeMedida { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual ICollection<ProdutoCompra> ProdutosCompra { get; set; }

        public virtual ICollection<ProdutoVenda> ProdutosVenda { get; set; }

        public bool Status { get; set; }
    }
}
