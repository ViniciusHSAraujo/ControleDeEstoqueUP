using System;

namespace ControleDeEstoqueUP.Models {
    class ItemCompra {
        public int Codigo { get; set; }
        public Nullable<double> Quantidade { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public virtual Compra Compra { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
