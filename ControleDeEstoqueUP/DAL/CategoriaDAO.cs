using ControleDeEstoqueUP.Data;
using ControleDeEstoqueUP.Models;
using ControleSeuEstoque.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeEstoqueUP.DAL {
    class CategoriaDAO {

        private static ApplicationDbContext database = SingletonContext.GetInstance();

        /**
         * Método que faz a inserção da categoria no banco de dados.
         */
        public Categoria Inserir(Categoria categoria) {
            try {
                database.Categorias.Add(categoria);
                database.SaveChanges();
                return categoria;
            } catch (Exception e) {
                throw new Exception("Ocorreu um erro ao cadastrar:\n" + e.Message);
            }
            
        }


        /**
         * Método que recebe uma categoria (editada) e realiza as edições da mesma no banco de dados.
         */
        public void Editar(Categoria categoria) {
            try {
                Categoria categoriaBanco = database.Categorias.First(c => c.Id == categoria.Id);
                categoriaBanco.Nome = categoria.Nome;
                database.SaveChanges();

            } catch (Exception e) {
                throw new Exception("Ocorreu um erro ao cadastrar:\n" + e.Message);
            }

        }

        /**
         * Método que recebe uma categoria (editada) e realiza as edições da mesma no banco de dados.
         */
        public void Inativar(int id) {
            try {
                Categoria categoriaBanco = database.Categorias.First(c => c.Id == id);
                categoriaBanco.Status = false;
                database.SaveChanges();

            } catch (Exception e) {
                throw new Exception("Ocorreu um erro ao inativar:\n" + e.Message);
            }

        }
    }
}
