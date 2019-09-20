using System;
using System.Collections.Generic;

namespace ControleDeEstoqueUP.Models {
    class Compra {
        public Compra() {
            this.ItensCompra = new HashSet<ItemCompra>();
        }

        public int Codigo { get; set; }
        public Nullable<DateTime> Data { get; set; }
        public Nullable<int> NotaFiscal { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<int> QtdeParcelas { get; set; }
        public Nullable<int> Status { get; set; }
        public virtual ICollection<ItemCompra> ItensCompra { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual TipoPagamento TipoPagamento { get; set; }
    }
}
