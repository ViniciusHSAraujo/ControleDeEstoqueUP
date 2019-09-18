using System.Collections.Generic;

namespace ControleDeEstoqueUP.Models {
    class TipoPagamento {
        public TipoPagamento() {
            this.Compra = new HashSet<Compra>();
            this.Venda = new HashSet<Venda>();
        }

        public int Codigo { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Compra> Compra { get; set; }
        public virtual ICollection<Venda> Venda { get; set; }
    }
}
