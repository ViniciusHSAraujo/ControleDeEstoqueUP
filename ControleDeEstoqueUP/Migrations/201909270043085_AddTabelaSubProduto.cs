namespace ControleDeEstoqueUP.Migrations {
    using System.Data.Entity.Migrations;

    public partial class AddTabelaSubProduto : DbMigration {
        public override void Up() {
            CreateTable(
                "dbo.SubProduto",
                c => new {
                    Id = c.Int(nullable: false, identity: true),
                    SKU = c.String(),
                    Status = c.Boolean(nullable: false),
                    Produto_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produtos", t => t.Produto_Id)
                .Index(t => t.Produto_Id);

        }

        public override void Down() {
            DropForeignKey("dbo.SubProduto", "Produto_Id", "dbo.Produtos");
            DropIndex("dbo.SubProduto", new[] { "Produto_Id" });
            DropTable("dbo.SubProduto");
        }
    }
}
