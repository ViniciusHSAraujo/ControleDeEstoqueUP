namespace ControleDeEstoqueUP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Teste : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        CPFCNPJ = c.String(),
                        RGIE = c.String(),
                        RazaoSocial = c.String(),
                        Tipo = c.Int(nullable: false),
                        CEP = c.String(),
                        Endereco = c.String(),
                        Bairro = c.String(),
                        Cidade = c.String(),
                        UF = c.String(),
                        Telefone = c.String(),
                        Celular = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Compras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        Fornecedor_Id = c.Int(),
                        Funcionario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fornecedores", t => t.Fornecedor_Id)
                .ForeignKey("dbo.Funcionario", t => t.Funcionario_Id)
                .Index(t => t.Fornecedor_Id)
                .Index(t => t.Funcionario_Id);
            
            CreateTable(
                "dbo.Fornecedores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        CPFCNPJ = c.String(),
                        RGIE = c.String(),
                        RazaoSocial = c.String(),
                        Tipo = c.Int(nullable: false),
                        CEP = c.String(),
                        Endereco = c.String(),
                        Bairro = c.String(),
                        Cidade = c.String(),
                        UF = c.String(),
                        Telefone = c.String(),
                        Celular = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Funcionario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Senha = c.String(),
                        Cargo = c.String(),
                        Salario = c.Double(nullable: false),
                        Admissao = c.DateTime(nullable: false),
                        Demissao = c.DateTime(nullable: false),
                        Nome = c.String(),
                        CPFCNPJ = c.String(),
                        RGIE = c.String(),
                        RazaoSocial = c.String(),
                        Tipo = c.Int(nullable: false),
                        CEP = c.String(),
                        Endereco = c.String(),
                        Bairro = c.String(),
                        Cidade = c.String(),
                        UF = c.String(),
                        Telefone = c.String(),
                        Celular = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProdutosCompra",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantidade = c.Double(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Compra_Id = c.Int(),
                        Produto_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Compras", t => t.Compra_Id)
                .ForeignKey("dbo.Produtos", t => t.Produto_Id)
                .Index(t => t.Compra_Id)
                .Index(t => t.Produto_Id);
            
            CreateTable(
                "dbo.Produtos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        ValorPago = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorVenda = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantidade = c.Double(nullable: false),
                        Categoria_Id = c.Int(),
                        UnidadeDeMedida_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categorias", t => t.Categoria_Id)
                .ForeignKey("dbo.UnidadesDeMedida", t => t.UnidadeDeMedida_Id)
                .Index(t => t.Categoria_Id)
                .Index(t => t.UnidadeDeMedida_Id);
            
            CreateTable(
                "dbo.UnidadesDeMedida",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Simbolo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProdutosVenda",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantidade = c.Double(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Produto_Id = c.Int(),
                        Venda_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produtos", t => t.Produto_Id)
                .ForeignKey("dbo.Vendas", t => t.Venda_Id)
                .Index(t => t.Produto_Id)
                .Index(t => t.Venda_Id);
            
            CreateTable(
                "dbo.Vendas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        Cliente_Id = c.Int(),
                        Funcionario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.Cliente_Id)
                .ForeignKey("dbo.Funcionario", t => t.Funcionario_Id)
                .Index(t => t.Cliente_Id)
                .Index(t => t.Funcionario_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProdutosVenda", "Venda_Id", "dbo.Vendas");
            DropForeignKey("dbo.Vendas", "Funcionario_Id", "dbo.Funcionario");
            DropForeignKey("dbo.Vendas", "Cliente_Id", "dbo.Clientes");
            DropForeignKey("dbo.ProdutosVenda", "Produto_Id", "dbo.Produtos");
            DropForeignKey("dbo.ProdutosCompra", "Produto_Id", "dbo.Produtos");
            DropForeignKey("dbo.Produtos", "UnidadeDeMedida_Id", "dbo.UnidadesDeMedida");
            DropForeignKey("dbo.Produtos", "Categoria_Id", "dbo.Categorias");
            DropForeignKey("dbo.ProdutosCompra", "Compra_Id", "dbo.Compras");
            DropForeignKey("dbo.Compras", "Funcionario_Id", "dbo.Funcionario");
            DropForeignKey("dbo.Compras", "Fornecedor_Id", "dbo.Fornecedores");
            DropIndex("dbo.Vendas", new[] { "Funcionario_Id" });
            DropIndex("dbo.Vendas", new[] { "Cliente_Id" });
            DropIndex("dbo.ProdutosVenda", new[] { "Venda_Id" });
            DropIndex("dbo.ProdutosVenda", new[] { "Produto_Id" });
            DropIndex("dbo.Produtos", new[] { "UnidadeDeMedida_Id" });
            DropIndex("dbo.Produtos", new[] { "Categoria_Id" });
            DropIndex("dbo.ProdutosCompra", new[] { "Produto_Id" });
            DropIndex("dbo.ProdutosCompra", new[] { "Compra_Id" });
            DropIndex("dbo.Compras", new[] { "Funcionario_Id" });
            DropIndex("dbo.Compras", new[] { "Fornecedor_Id" });
            DropTable("dbo.Vendas");
            DropTable("dbo.ProdutosVenda");
            DropTable("dbo.UnidadesDeMedida");
            DropTable("dbo.Produtos");
            DropTable("dbo.ProdutosCompra");
            DropTable("dbo.Funcionario");
            DropTable("dbo.Fornecedores");
            DropTable("dbo.Compras");
            DropTable("dbo.Clientes");
            DropTable("dbo.Categorias");
        }
    }
}
