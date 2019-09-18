using ControleDeEstoqueUP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeEstoqueUP.Data {
    class ApplicationDbContext : DbContext {
        public ApplicationDbContext() : base("ControleDeEstoqueUP") {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<ItemCompra> ItensCompra { get; set; }
        public DbSet<ItemVenda> ItensVenda { get; set; }
        public DbSet<ParcelaCompra> ParcelasCompra { get; set; }
        public DbSet<ParcelaVenda> ParcelasVenda { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<SubCategoria> SubCategorias { get; set; }
        public DbSet<TipoPagamento> TipoPagamentos { get; set; }
        public DbSet<UnidadeDeMedida> UnidadeDeMedidas { get; set; }
        public DbSet<Venda> Vendas { get; set; }
    }
}
