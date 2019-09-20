using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ControleDeEstoqueUP.Models{
    [Table("UnidadesDeMedida")]
    public class UnidadeDeMedida{
		public int Id { get; set; }

        public string Nome { get; set; }

        public string Simbolo { get; set; }

        public bool Status { get; set; }
    }
}
