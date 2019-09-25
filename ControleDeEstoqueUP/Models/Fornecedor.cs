using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ControleDeEstoqueUP.Models{
    [Table("Fornecedores")]
    public class Fornecedor : Pessoa {

        public Fornecedor() {

        }

        public Fornecedor(string cnpj, string razaoSocial, string inscricaoestadual, string cep, string endereco, string bairro, string cidade, string uf, string telefone, string celular, string email, int id = 0) {
            Id = id;
            CPFCNPJ = cnpj;
            RazaoSocial = razaoSocial;
            RGIE = inscricaoestadual;
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
