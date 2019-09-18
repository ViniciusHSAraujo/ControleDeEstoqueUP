using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeEstoqueUP.Models {
    [Table("Produtos")]
    class Produto {
        public Produto() {
            this.ItensCompra = new HashSet<ItemCompra>();
            this.ItensVenda = new HashSet<ItemVenda>();
        }

        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public byte[] Foto { get; set; }
        public Nullable<decimal> ValorPago { get; set; }
        public Nullable<decimal> ValorVenda { get; set; }
        public Nullable<double> Quantidade { get; set; }
        public Nullable<int> umed_Codigo { get; set; }
        public virtual UnidadeDeMedida UnidadeDeMedida { get; set; }
        public Nullable<int> cat_Codigo { get; set; }
        public virtual Categoria Categoria { get; set; }
        public Nullable<int> scat_Codigo { get; set; }
        public virtual SubCategoria SubCategoria { get; set; }
        public virtual ICollection<ItemCompra> ItensCompra { get; set; }
        public virtual ICollection<ItemVenda> ItensVenda { get; set; }
    }
}
