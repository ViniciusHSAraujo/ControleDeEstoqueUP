using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeEstoqueUP.Models {
    public abstract class Movimentacao{
		public int Id { get; set; }

        public DateTime Data { get; set; }

        public decimal Total { get; set; }

        public virtual Funcionario Funcionario { get; set; }

        public override bool Equals(object obj) {
            return obj is Movimentacao movimentacao &&
                   Id == movimentacao.Id;
        }

        public override int GetHashCode() {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
