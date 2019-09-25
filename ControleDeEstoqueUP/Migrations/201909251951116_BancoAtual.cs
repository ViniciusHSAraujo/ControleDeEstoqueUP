namespace ControleDeEstoqueUP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BancoAtual : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clientes", "Celular", c => c.String());
            AddColumn("dbo.Clientes", "Email", c => c.String());
            AddColumn("dbo.Fornecedores", "Celular", c => c.String());
            AddColumn("dbo.Fornecedores", "Email", c => c.String());
            AddColumn("dbo.Funcionario", "Celular", c => c.String());
            AddColumn("dbo.Funcionario", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Funcionario", "Email");
            DropColumn("dbo.Funcionario", "Celular");
            DropColumn("dbo.Fornecedores", "Email");
            DropColumn("dbo.Fornecedores", "Celular");
            DropColumn("dbo.Clientes", "Email");
            DropColumn("dbo.Clientes", "Celular");
        }
    }
}
