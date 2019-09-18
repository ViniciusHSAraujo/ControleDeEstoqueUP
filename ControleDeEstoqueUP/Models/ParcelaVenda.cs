using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeEstoqueUP.Models {
    [Table("ParcelasVenda")]
    class ParcelaVenda {
        [Key]
        public int ven_Codigo { get; set; }
        public virtual Venda Venda { get; set; }
        public int Codigo { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public Nullable<System.DateTime> DataPagamento { get; set; }
        public Nullable<System.DateTime> DataVencimento { get; set; }
    }
}
