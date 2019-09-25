namespace ControleDeEstoqueUP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Banco2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Funcionario", "CPF", c => c.String());
            DropColumn("dbo.Funcionario", "CNPJ");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Funcionario", "CNPJ", c => c.String());
            DropColumn("dbo.Funcionario", "CPF");
        }
    }
}
