using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeEstoqueUP.Models {
    [Table("ItensVenda")]
    class ItemVenda {
        [Key]
        public int Codigo { get; set; }
        public Nullable<double> Quantidade { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public int ven_codigo { get; set; }
        public virtual Venda Venda { get; set; }
        public int pro_codigo { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
