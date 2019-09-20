namespace ControleDeEstoqueUP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemocaoDosStatus : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categorias", "Status");
            DropColumn("dbo.Produtos", "Status");
            DropColumn("dbo.Fornecedores", "Status");
            DropColumn("dbo.Funcionario", "Status");
            DropColumn("dbo.Clientes", "Status");
            DropColumn("dbo.UnidadesDeMedida", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UnidadesDeMedida", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Clientes", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Funcionario", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Fornecedores", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Produtos", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Categorias", "Status", c => c.Boolean(nullable: false));
        }
    }
}
