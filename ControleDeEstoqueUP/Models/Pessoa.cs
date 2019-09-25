using System;
using System.Collections.Generic;
using System.Text;

namespace ControleDeEstoqueUP.Models {

    public abstract class Pessoa {
        protected Pessoa() {
        }

        protected Pessoa(string nome, int tipo, string cEP, string endereco, string bairro, string cidade, string uF, string telefone, string celular, string email) {
            Nome = nome;
            Tipo = tipo;
            CEP = cEP;
            Endereco = endereco;
            Bairro = bairro;
            Cidade = cidade;
            UF = uF;
            Telefone = telefone;
            Celular = celular;
            Email = email;
        }

        public int Id { get; set; }

        public string Nome { get; set; }

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
