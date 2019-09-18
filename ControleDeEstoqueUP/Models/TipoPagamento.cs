using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeEstoqueUP.Models {
    [Table("TiposPagamentos")]
    class TipoPagamento {
        public TipoPagamento() {
            this.Compra = new HashSet<Compra>();
            this.Venda = new HashSet<Venda>();
        }

        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Compra> Compra { get; set; }
        public virtual ICollection<Venda> Venda { get; set; }
    }
}
