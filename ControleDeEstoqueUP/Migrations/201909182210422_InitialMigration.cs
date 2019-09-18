namespace ControleDeEstoqueUP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Codigo);
            
            CreateTable(
                "dbo.Produtos",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Descricao = c.String(),
                        Foto = c.Binary(),
                        ValorPago = c.Decimal(precision: 18, scale: 2),
                        ValorVenda = c.Decimal(precision: 18, scale: 2),
                        Quantidade = c.Double(),
                        umed_Codigo = c.Int(),
                        cat_Codigo = c.Int(),
                        scat_Codigo = c.Int(),
                        Categoria_Codigo = c.Int(),
                        SubCategoria_Codigo = c.Int(),
                        UnidadeDeMedida_Codigo = c.Int(),
                    })
                .PrimaryKey(t => t.Codigo)
                .ForeignKey("dbo.Categorias", t => t.Categoria_Codigo)
                .ForeignKey("dbo.SubCategorias", t => t.SubCategoria_Codigo)
                .ForeignKey("dbo.UnidadesDeMedida", t => t.UnidadeDeMedida_Codigo)
                .Index(t => t.Categoria_Codigo)
                .Index(t => t.SubCategoria_Codigo)
                .Index(t => t.UnidadeDeMedida_Codigo);
            
            CreateTable(
                "dbo.ItensCompra",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Quantidade = c.Double(),
                        Valor = c.Decimal(precision: 18, scale: 2),
                        com_codigo = c.Int(nullable: false),
                        pro_codigo = c.Int(nullable: false),
                        Compra_Codigo = c.Int(),
                        Produto_Codigo = c.Int(),
                    })
                .PrimaryKey(t => t.Codigo)
                .ForeignKey("dbo.Compras", t => t.Compra_Codigo)
                .ForeignKey("dbo.Produtos", t => t.Produto_Codigo)
                .Index(t => t.Compra_Codigo)
                .Index(t => t.Produto_Codigo);
            
            CreateTable(
                "dbo.Compras",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(),
                        NotaFiscal = c.Int(),
                        Total = c.Decimal(precision: 18, scale: 2),
                        QtdeParcelas = c.Int(),
                        Status = c.Int(),
                        for_codigo = c.Int(),
                        tpa_codigo = c.Int(),
                        Fornecedor_Codigo = c.Int(),
                        TipoPagamento_Codigo = c.Int(),
                    })
                .PrimaryKey(t => t.Codigo)
                .ForeignKey("dbo.Fornecedores", t => t.Fornecedor_Codigo)
                .ForeignKey("dbo.TiposPagamentos", t => t.TipoPagamento_Codigo)
                .Index(t => t.Fornecedor_Codigo)
                .Index(t => t.TipoPagamento_Codigo);
            
            CreateTable(
                "dbo.Fornecedores",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        RazaoSocial = c.String(),
                        IE = c.String(),
                        CNPJ = c.String(),
                        CEP = c.String(),
                        Endereco = c.String(),
                        Bairro = c.String(),
                        Telefone = c.String(),
                        Celular = c.String(),
                        Email = c.String(),
                        EndNumero = c.String(),
                        Cidade = c.String(),
                        Estado = c.String(),
                    })
                .PrimaryKey(t => t.Codigo);
            
            CreateTable(
                "dbo.ParcelasCompra",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Valor = c.Decimal(precision: 18, scale: 2),
                        DataPagamento = c.DateTime(),
                        DataVencimento = c.DateTime(),
                        Com_codigo = c.Int(nullable: false),
                        Compra_Codigo = c.Int(),
                    })
                .PrimaryKey(t => t.Codigo)
                .ForeignKey("dbo.Compras", t => t.Compra_Codigo)
                .Index(t => t.Compra_Codigo);
            
            CreateTable(
                "dbo.TiposPagamentos",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Codigo);
            
            CreateTable(
                "dbo.Vendas",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(),
                        NotaFiscal = c.Int(),
                        Total = c.Decimal(precision: 18, scale: 2),
                        QtdeParcelas = c.Int(),
                        Status = c.Int(),
                        cli_Codigo = c.Int(),
                        tpa_Codigo = c.Int(),
                        Cliente_Codigo = c.Int(),
                        TipoPagamento_Codigo = c.Int(),
                    })
                .PrimaryKey(t => t.Codigo)
                .ForeignKey("dbo.Clientes", t => t.Cliente_Codigo)
                .ForeignKey("dbo.TiposPagamentos", t => t.TipoPagamento_Codigo)
                .Index(t => t.Cliente_Codigo)
                .Index(t => t.TipoPagamento_Codigo);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        CPFCNPJ = c.String(),
                        RGIE = c.String(),
                        RazaoSocial = c.String(),
                        Tipo = c.Int(),
                        CEP = c.String(),
                        Endereco = c.String(),
                        Bairro = c.String(),
                        Telefone = c.String(),
                        Celular = c.String(),
                        Email = c.String(),
                        EndNumero = c.String(),
                        Cidade = c.String(),
                        Estado = c.String(),
                    })
                .PrimaryKey(t => t.Codigo);
            
            CreateTable(
                "dbo.ItensVenda",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Quantidade = c.Double(),
                        Valor = c.Decimal(precision: 18, scale: 2),
                        ven_codigo = c.Int(nullable: false),
                        pro_codigo = c.Int(nullable: false),
                        Produto_Codigo = c.Int(),
                        Venda_Codigo = c.Int(),
                    })
                .PrimaryKey(t => t.Codigo)
                .ForeignKey("dbo.Produtos", t => t.Produto_Codigo)
                .ForeignKey("dbo.Vendas", t => t.Venda_Codigo)
                .Index(t => t.Produto_Codigo)
                .Index(t => t.Venda_Codigo);
            
            CreateTable(
                "dbo.ParcelasVenda",
                c => new
                    {
                        ven_Codigo = c.Int(nullable: false, identity: true),
                        Codigo = c.Int(nullable: false),
                        Valor = c.Decimal(precision: 18, scale: 2),
                        DataPagamento = c.DateTime(),
                        DataVencimento = c.DateTime(),
                    })
                .PrimaryKey(t => t.ven_Codigo)
                .ForeignKey("dbo.Vendas", t => t.Codigo, cascadeDelete: true)
                .Index(t => t.Codigo);
            
            CreateTable(
                "dbo.SubCategorias",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        cat_Codigo = c.Int(),
                        Categoria_Codigo = c.Int(),
                    })
                .PrimaryKey(t => t.Codigo)
                .ForeignKey("dbo.Categorias", t => t.Categoria_Codigo)
                .Index(t => t.Categoria_Codigo);
            
            CreateTable(
                "dbo.UnidadesDeMedida",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Codigo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produtos", "UnidadeDeMedida_Codigo", "dbo.UnidadesDeMedida");
            DropForeignKey("dbo.Produtos", "SubCategoria_Codigo", "dbo.SubCategorias");
            DropForeignKey("dbo.SubCategorias", "Categoria_Codigo", "dbo.Categorias");
            DropForeignKey("dbo.ItensCompra", "Produto_Codigo", "dbo.Produtos");
            DropForeignKey("dbo.Vendas", "TipoPagamento_Codigo", "dbo.TiposPagamentos");
            DropForeignKey("dbo.ParcelasVenda", "Codigo", "dbo.Vendas");
            DropForeignKey("dbo.ItensVenda", "Venda_Codigo", "dbo.Vendas");
            DropForeignKey("dbo.ItensVenda", "Produto_Codigo", "dbo.Produtos");
            DropForeignKey("dbo.Vendas", "Cliente_Codigo", "dbo.Clientes");
            DropForeignKey("dbo.Compras", "TipoPagamento_Codigo", "dbo.TiposPagamentos");
            DropForeignKey("dbo.ParcelasCompra", "Compra_Codigo", "dbo.Compras");
            DropForeignKey("dbo.ItensCompra", "Compra_Codigo", "dbo.Compras");
            DropForeignKey("dbo.Compras", "Fornecedor_Codigo", "dbo.Fornecedores");
            DropForeignKey("dbo.Produtos", "Categoria_Codigo", "dbo.Categorias");
            DropIndex("dbo.SubCategorias", new[] { "Categoria_Codigo" });
            DropIndex("dbo.ParcelasVenda", new[] { "Codigo" });
            DropIndex("dbo.ItensVenda", new[] { "Venda_Codigo" });
            DropIndex("dbo.ItensVenda", new[] { "Produto_Codigo" });
            DropIndex("dbo.Vendas", new[] { "TipoPagamento_Codigo" });
            DropIndex("dbo.Vendas", new[] { "Cliente_Codigo" });
            DropIndex("dbo.ParcelasCompra", new[] { "Compra_Codigo" });
            DropIndex("dbo.Compras", new[] { "TipoPagamento_Codigo" });
            DropIndex("dbo.Compras", new[] { "Fornecedor_Codigo" });
            DropIndex("dbo.ItensCompra", new[] { "Produto_Codigo" });
            DropIndex("dbo.ItensCompra", new[] { "Compra_Codigo" });
            DropIndex("dbo.Produtos", new[] { "UnidadeDeMedida_Codigo" });
            DropIndex("dbo.Produtos", new[] { "SubCategoria_Codigo" });
            DropIndex("dbo.Produtos", new[] { "Categoria_Codigo" });
            DropTable("dbo.UnidadesDeMedida");
            DropTable("dbo.SubCategorias");
            DropTable("dbo.ParcelasVenda");
            DropTable("dbo.ItensVenda");
            DropTable("dbo.Clientes");
            DropTable("dbo.Vendas");
            DropTable("dbo.TiposPagamentos");
            DropTable("dbo.ParcelasCompra");
            DropTable("dbo.Fornecedores");
            DropTable("dbo.Compras");
            DropTable("dbo.ItensCompra");
            DropTable("dbo.Produtos");
            DropTable("dbo.Categorias");
        }
    }
}
