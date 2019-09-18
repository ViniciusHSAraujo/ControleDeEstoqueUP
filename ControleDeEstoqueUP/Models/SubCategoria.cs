using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeEstoqueUP.Models {
    [Table("SubCategorias")]
    class SubCategoria {
        public SubCategoria() {
            this.Produto = new HashSet<Produto>();
        }
        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public Nullable<int> cat_Codigo { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual ICollection<Produto> Produto { get; set; }
    }
}
