using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeEstoqueUP.Models {
    [Table("SubProduto")]
    public class SubProduto {

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

        public virtual Produto Produto { get; set; }

        public string SKU { get; set; }

        public bool Status { get; set; }

        public string StatusString => Status == true ? "Disponível" : "Vendido";

    }
}
