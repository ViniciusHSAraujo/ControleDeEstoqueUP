namespace ControleDeEstoqueUP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RetiradoCampoStatusCompraEVenda : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Compras", "Status");
            DropColumn("dbo.Vendas", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vendas", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Compras", "Status", c => c.Int(nullable: false));
        }
    }
}
