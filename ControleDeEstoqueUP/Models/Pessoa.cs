using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeEstoqueUP.Models {

    public abstract class Pessoa{
		public int Id { get; set; }

        public string Nome { get; set; }

        public string CPFCNPJ { get; set; }

        public string RGIE { get; set; }

        public string RazaoSocial { get; set; }

        public int Tipo { get; set; }

        public string CEP { get; set; }

        public string Endereco { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string UF { get; set; }

        public string Telefone { get; set; }

        public string Celular { get; set; }

        public string Email { get; set; }

    }
}
