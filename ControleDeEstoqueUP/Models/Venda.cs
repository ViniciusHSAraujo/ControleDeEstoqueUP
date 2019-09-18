using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeEstoqueUP.Models {
    [Table("Vendas")]
    class Venda {
        public Venda() {
            this.ItensVenda = new HashSet<ItemVenda>();
            this.ParcelasVenda = new HashSet<ParcelaVenda>();
        }
        [Key]
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
        public virtual ICollection<ItemVenda> ItensVenda { get; set; }
        public virtual ICollection<ParcelaVenda> ParcelasVenda { get; set; }
    }
}
