using static ControleSeuEstoque.Utils.WPFUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeEstoqueUP.Models {
    [Table("SubProduto")]
    class SubProduto {

        public SubProduto() {
        }

        public SubProduto(Produto produto, int id = 0) {
            Id = id;
            Produto = produto;
            SKU = "";
            Status = true;
        }

        public SubProduto(Produto produto, string sku, bool status, int id) {
            Id = id;
            Produto = produto;
            SKU = sku;
            Status = status;
        }

        public int Id { get; set; }

        public Produto Produto { get; set; }

        public string SKU { get; set; }

        public bool Status { get; set; }

    }
}
