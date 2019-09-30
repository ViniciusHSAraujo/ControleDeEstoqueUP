

using ControleDeEstoqueUP.Data;
using ControleDeEstoqueUP.Models;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeEstoqueUP.DAL {
    internal class SubProdutoDAO {

        private static readonly ApplicationDbContext database = SingletonContext.GetInstance();

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
