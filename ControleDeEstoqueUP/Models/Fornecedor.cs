using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ControleDeEstoqueUP.Models {
    [Table("Fornecedores")]
    public class Fornecedor : Pessoa {
        public string CNPJ { get; set; }
        public Fornecedor() {
        }

        public Fornecedor(string cnpj, string nome, string cep, string endereco, string bairro, string cidade, string uf, string telefone, string celular, string email, int id) {
            Id = id;
            CNPJ = cnpj;
            Nome = nome;
            CEP = cep;
            Endereco = endereco;
            Bairro = bairro;
            Cidade = cidade;
            UF = uf;
            Telefone = telefone;
            Celular = celular;
            Email = email;
        }
    }
}
