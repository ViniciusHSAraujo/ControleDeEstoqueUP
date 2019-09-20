using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ControleDeEstoqueUP.Models{
    [Table("Categorias")]
	public class Categoria{
        public Categoria() {
        }
        public Categoria(string nome, int id) {
            this.Id = id;
            this.Nome = nome;
        }

        public int Id { get; set; }

        public string Nome { get; set; }

    }
}
