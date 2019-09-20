﻿using System.Collections.Generic;

namespace ControleDeEstoqueUP.Models {
    class Categoria {
        public Categoria() {
            this.Produto = new HashSet<Produto>();
        }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Produto> Produto { get; set; }
    }
}