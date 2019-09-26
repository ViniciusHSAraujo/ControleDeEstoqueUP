namespace ControleDeEstoqueUP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteradoCampoDeDataDemissaoParaNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Funcionario", "Demissao", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Funcionario", "Demissao", c => c.DateTime(nullable: false));
        }
    }
}
