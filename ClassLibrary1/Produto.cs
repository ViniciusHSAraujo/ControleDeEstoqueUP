using System;
using System.Collections.Generic;

namespace ControleDeEstoqueUP.Models {
    class Produto {
        public Produto() {
            this.ItensCompra = new HashSet<ItemCompra>();
            this.ItensVenda = new HashSet<ItemVenda>();
        }

        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public byte[] Foto { get; set; }
        public Nullable<decimal> ValorPago { get; set; }
        public Nullable<decimal> ValorVenda { get; set; }
        public Nullable<double> Quantidade { get; set; }
        public virtual UnidadeDeMedida UnidadeDeMedida { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual SubCategoria SubCategoria { get; set; }
        public virtual ICollection<ItemCompra> ItensCompra { get; set; }
        public virtual ICollection<ItemVenda> ItensVenda { get; set; }
    }
}
