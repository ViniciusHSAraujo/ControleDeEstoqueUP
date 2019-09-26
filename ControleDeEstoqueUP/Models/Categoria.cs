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

        public override bool Equals(object obj) {
            return obj is Categoria categoria &&
                   Id == categoria.Id;
        }

        public override int GetHashCode() {
            return 2108858624 + Id.GetHashCode();
        }

        public override string ToString() {
            return Nome;
        }


    }
}
