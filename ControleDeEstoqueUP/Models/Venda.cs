using System;
using System.Collections.Generic;

namespace ControleDeEstoqueUP.Models {
    class Venda {
        public Venda() {
            this.ItensVenda = new HashSet<ItensVenda>();
            this.ParcelasVenda = new HashSet<ParcelasVenda>();
        }

        public int Codigo { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public Nullable<int> NotaFiscal { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<int> QtdeParcelas { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> cli_Codigo { get; set; }
        public virtual Cliente Cliente { get; set; }
        public Nullable<int> tpa_Codigo { get; set; }
        public virtual TipoPagamento TipoPagamento { get; set; }
        public virtual ICollection<ItensVenda> ItensVenda { get; set; }
        public virtual ICollection<ParcelasVenda> ParcelasVenda { get; set; }
    }
}
