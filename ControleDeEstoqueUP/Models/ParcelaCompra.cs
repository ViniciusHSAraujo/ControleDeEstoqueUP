using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeEstoqueUP.Models {
    [Table("ParcelasCompra")]
    class ParcelaCompra {
        [Key]
        public int Codigo { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public Nullable<System.DateTime> DataPagamento { get; set; }
        public Nullable<System.DateTime> DataVencimento { get; set; }
        public int Com_codigo { get; set; }
        public virtual Compra Compra { get; set; }
    }
}
