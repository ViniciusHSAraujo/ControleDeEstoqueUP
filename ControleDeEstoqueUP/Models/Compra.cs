using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeEstoqueUP.Models {
    [Table("Compras")]
    class Compra {
        public Compra() {
            this.ItensCompra = new HashSet<ItemCompra>();
            this.ParcelasCompra = new HashSet<ParcelaCompra>();
        }

        [Key]
        public int Codigo { get; set; }
        public Nullable<DateTime> Data { get; set; }
        public Nullable<int> NotaFiscal { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<int> QtdeParcelas { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> for_codigo { get; set; }
        public Nullable<int> tpa_codigo { get; set; }
        public virtual ICollection<ItemCompra> ItensCompra { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual TipoPagamento TipoPagamento { get; set; }
        public virtual ICollection<ParcelaCompra> ParcelasCompra { get; set; }
    }
}
