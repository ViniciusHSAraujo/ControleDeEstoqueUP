namespace ControleDeEstoqueUP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracoesSubProduto : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProdutosVenda", "Produto_Id", "dbo.Produtos");
            DropIndex("dbo.ProdutosVenda", new[] { "Produto_Id" });
            AddColumn("dbo.ProdutosVenda", "SubProduto_Id", c => c.Int());
            CreateIndex("dbo.ProdutosVenda", "SubProduto_Id");
            AddForeignKey("dbo.ProdutosVenda", "SubProduto_Id", "dbo.SubProduto", "Id");
            DropColumn("dbo.ProdutosVenda", "Quantidade");
            DropColumn("dbo.ProdutosVenda", "Produto_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProdutosVenda", "Produto_Id", c => c.Int());
            AddColumn("dbo.ProdutosVenda", "Quantidade", c => c.Double(nullable: false));
            DropForeignKey("dbo.ProdutosVenda", "SubProduto_Id", "dbo.SubProduto");
            DropIndex("dbo.ProdutosVenda", new[] { "SubProduto_Id" });
            DropColumn("dbo.ProdutosVenda", "SubProduto_Id");
            CreateIndex("dbo.ProdutosVenda", "Produto_Id");
            AddForeignKey("dbo.ProdutosVenda", "Produto_Id", "dbo.Produtos", "Id");
        }
    }
}
