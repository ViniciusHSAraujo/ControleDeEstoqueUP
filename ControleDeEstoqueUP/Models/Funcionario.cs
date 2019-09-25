using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ControleDeEstoqueUP.Models{
    [Table("Funcionario")]
    public class Funcionario : Pessoa{
        public Funcionario() {
        }

        public string Senha { get; set; }

        public string Cargo { get; set; }

        public double Salario { get; set; }

        public DateTime Admissao { get; set; }

        public DateTime Demissao { get; set; }
    }
}
