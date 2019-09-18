using System;

namespace ControleDeEstoqueUP.Models {
    class ParcelaVenda {
        public int ven_Codigo { get; set; }
        public virtual Venda Venda { get; set; }
        public int Codigo { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public Nullable<System.DateTime> DataPagamento { get; set; }
        public Nullable<System.DateTime> DataVencimento { get; set; }
    }
}
