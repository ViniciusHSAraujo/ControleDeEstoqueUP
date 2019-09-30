using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeEstoqueUP.Models {
    [Table("Produtos")]
    public class Produto {

        public Produto() {
        }

        public Produto(string nome, decimal valorvenda, UnidadeDeMedida unidadeDeMedida, Categoria categoria, int id = 0, decimal valorPago = 0) {
            Nome = nome;
            ValorVenda = valorvenda;
            UnidadeDeMedida = unidadeDeMedida;
            Categoria = categoria;
            Id = id;
            ValorPago = valorPago;
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public decimal ValorPago { get; set; }

        public decimal ValorVenda { get; set; }

        public virtual UnidadeDeMedida UnidadeDeMedida { get; set; }

        public virtual Categoria Categoria { get; set; }

        public override bool Equals(object obj) {
            return obj is Produto produto &&
                   Id == produto.Id;
        }

        public override int GetHashCode() {
            return 2108858624 + Id.GetHashCode();
        }

        public override string ToString() {
            return Nome;
        }
    }
}
