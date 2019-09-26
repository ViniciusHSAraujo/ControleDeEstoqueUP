using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ControleDeEstoqueUP.Models {
    [Table("Clientes")]
    public class Cliente : Pessoa {
        public string CPF { get; set; }
        public Cliente(string nome, string cpf, string cep, string endereco, string bairro, string cidade, string uf, string telefone, string celular, string email, int id) {
            Id = id;
            Nome = nome;
            CPF = cpf;
            CEP = cep;
            Endereco = endereco;
            Bairro = bairro;
            Cidade = cidade;
            UF = uf;
            Telefone = telefone;
            Celular = celular;
            Email = email;
        }

        public Cliente() {
        }
    }
}
