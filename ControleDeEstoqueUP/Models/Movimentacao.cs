using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeEstoqueUP.Models {
    public abstract class Movimentacao{
		public int Id { get; set; }

        public DateTime Data { get; set; }

        public decimal Total { get; set; }

        public int Status { get; set; }

        public virtual Funcionario Funcionario { get; set; }
    }
}
