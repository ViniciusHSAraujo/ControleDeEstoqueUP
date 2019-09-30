using ControleDeEstoqueUP.Models;
using System.Data.Entity;

namespace ControleDeEstoqueUP.Data {
    internal class ApplicationDbContext : DbContext {
        //protected ApplicationDbContext(DbCompiledModel model) : base(model) {
        //}

        //public ApplicationDbContext() {
        //}

        //protected override void OnModelCreating(DbModelBuilder modelBuilder) {
        //    base.OnModelCreating(modelBuilder);
        //}

        public ApplicationDbContext() : base("ControleDeEstoqueUP") {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<SubProduto> SubProdutos { get; set; }
        public DbSet<ProdutoCompra> ProdutosCompra { get; set; }
        public DbSet<ProdutoVenda> ProdutosVenda { get; set; }
        public DbSet<UnidadeDeMedida> UnidadeDeMedidas { get; set; }
        public DbSet<Venda> Vendas { get; set; }
    }
}
