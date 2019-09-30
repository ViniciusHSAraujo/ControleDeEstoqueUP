﻿

using ControleDeEstoqueUP.Data;
using ControleDeEstoqueUP.Models;
using ControleDeEstoqueUP.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeEstoqueUP.DAL {
    class SubProdutoDAO {

        private static ApplicationDbContext database = SingletonContext.GetInstance();

        /**
        * Método que recebe uma categoria (editada) e realiza as edições da mesma no banco de dados.
        */
        public List<SubProduto> ListarSubProdutosAtivos() {
            return database.SubProdutos.Where(p => p.Status).ToList();
        }
        public List<SubProduto> ListarSubProdutos() {
            return database.SubProdutos.ToList();
        }

        public SubProduto BuscarSubProdutoPeloId(int id) {
            return database.SubProdutos.FirstOrDefault(p => p.Id == id);
        }

    }
}
