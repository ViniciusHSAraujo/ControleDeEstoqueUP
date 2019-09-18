using System;
using System.Collections.Generic;

namespace ControleDeEstoqueUP.Models {
    class SubCategoria {
        public SubCategoria() {
            this.Produto = new HashSet<Produto>();
        }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public Nullable<int> cat_Codigo { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual ICollection<Produto> Produto { get; set; }
    }
}
