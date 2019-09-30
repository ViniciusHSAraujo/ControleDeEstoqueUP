using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeEstoqueUP.Models {
    [Table("Funcionario")]
    public class Funcionario : Pessoa {

        public string Senha { get; set; }

        public string CPF { get; set; }

        public string Cargo { get; set; }

        public double Salario { get; set; }

        public DateTime Admissao { get; set; }

        public Nullable<DateTime> Demissao { get; set; }
        public Funcionario() {
        }

        public Funcionario(string nome, string cpf, string cep, string endereco, string bairro, string cidade, string uf, string telefone, string celular, string email, string senha, string cargo, double salario, DateTime admissao, DateTime? demissao, int id) {
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
            Senha = senha;
            Cargo = cargo;
            Salario = salario;
            Admissao = admissao;
            Demissao = demissao;
        }

        public override string ToString() {
            return Nome;
        }
    }
}
