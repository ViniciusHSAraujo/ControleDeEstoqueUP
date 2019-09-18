using System;
using System.Collections.Generic;

namespace ControleDeEstoqueUP.Models {
    class Produto {
        public Produto() {
            this.ItensCompra = new HashSet<ItensCompra>();
            this.ItensVenda = new HashSet<ItensVenda>();
        }

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
        public virtual ICollection<ItensCompra> ItensCompra { get; set; }
        public virtual ICollection<ItensVenda> ItensVenda { get; set; }
    }
}
