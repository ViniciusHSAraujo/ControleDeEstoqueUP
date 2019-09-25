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

        public UnidadeDeMedida() {

        }

        public UnidadeDeMedida(string nome, string simbolo, int id = 0) {
            Id = id;
            Nome = nome;
            Simbolo = simbolo;
        }

        public override string ToString() {
            return Simbolo;
        }

    }
}
