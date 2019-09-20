using System;

namespace ControleDeEstoqueUP.Models {
    class ItemVenda {
        public int Codigo { get; set; }
        public Nullable<double> Quantidade { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public virtual Venda Venda { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
