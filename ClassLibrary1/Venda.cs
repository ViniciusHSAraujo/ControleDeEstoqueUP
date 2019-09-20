using System;
using System.Collections.Generic;

namespace ControleDeEstoqueUP.Models {
    class Venda {
        public Venda() {
            this.ItensVenda = new HashSet<ItemVenda>();
        }
        public int Codigo { get; set; }
        public Nullable<DateTime> Data { get; set; }
        public Nullable<int> NotaFiscal { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<int> QtdeParcelas { get; set; }
        public Nullable<int> Status { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual TipoPagamento TipoPagamento { get; set; }
        public virtual ICollection<ItemVenda> ItensVenda { get; set; }
    }
}
