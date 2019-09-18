using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeEstoqueUP.Models {
    [Table("ItensCompra")]
    class ItemCompra {
        [Key]
        public int Codigo { get; set; }
        public Nullable<double> Quantidade { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public int com_codigo { get; set; }
        public virtual Compra Compra { get; set; }
        public int pro_codigo { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
