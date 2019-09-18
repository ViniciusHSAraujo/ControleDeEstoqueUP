using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeEstoqueUP.Models {
    [Table("UnidadesDeMedida")]
    class UnidadeDeMedida {
        public UnidadeDeMedida() {
            this.Produto = new HashSet<Produto>();
        }

        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Produto> Produto { get; set; }
    }
}
