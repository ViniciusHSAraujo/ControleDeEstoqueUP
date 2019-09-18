using System.Collections.Generic;

namespace ControleDeEstoqueUP.Models {
    class Fornecedor {
        public Fornecedor() {
            this.Compra = new HashSet<Compra>();
        }

        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string RazaoSocial { get; set; }
        public string IE { get; set; }
        public string CNPJ { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string EndNumero { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public virtual ICollection<Compra> Compra { get; set; }
    }
}
