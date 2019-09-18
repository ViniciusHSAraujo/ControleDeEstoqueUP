using System;

namespace ControleDeEstoqueUP.Models {
    class ItensVenda {
        public int Codigo { get; set; }
        public Nullable<double> Quantidade { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public int ven_codigo { get; set; }
        public virtual Venda Venda { get; set; }
        public int pro_codigo { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
