using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeEstoqueUP.Models {
    [Table("Clientes")]
    class Cliente {
        public Cliente() {
            this.Venda = new HashSet<Venda>();
        }

        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string CPFCNPJ { get; set; }
        public string RGIE { get; set; }
        public string RazaoSocial { get; set; }
        public Nullable<int> Tipo { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string EndNumero { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<Venda> Venda { get; set; }
    }
}
