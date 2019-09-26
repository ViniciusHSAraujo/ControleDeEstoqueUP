using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ControleDeEstoqueUP.Models{
    [Table("Produtos")]
    public class Produto{

        public Produto() {
        }

        public Produto(string nome, decimal valorvenda, UnidadeDeMedida unidadeDeMedida, Categoria categoria, int id = 0, decimal valorPago = 0) {
            this.Nome = nome;
            this.ValorVenda = valorvenda;
            this.UnidadeDeMedida = unidadeDeMedida;
            this.Categoria = categoria;
            this.Id = id;
            this.ValorPago = valorPago;
        }

		public int Id { get; set; }

        public string Nome { get; set; }

        public decimal ValorPago { get; set; }

        public decimal ValorVenda { get; set; }

        public virtual UnidadeDeMedida UnidadeDeMedida { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}
