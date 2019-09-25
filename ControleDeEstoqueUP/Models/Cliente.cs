using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ControleDeEstoqueUP.Models{
    [Table("Clientes")]
    public class Cliente : Pessoa{
        public string CPF { get; set; }
    }
}
