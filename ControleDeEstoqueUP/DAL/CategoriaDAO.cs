using ControleDeEstoqueUP.Data;
using ControleDeEstoqueUP.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeEstoqueUP.DAL {
    internal class CategoriaDAO {

        private static readonly ApplicationDbContext database = SingletonContext.GetInstance();

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
                Categoria categoriaBanco = database.Categorias.FirstOrDefault(c => c.Id == categoria.Id);
                categoriaBanco.Nome = categoria.Nome;
                database.SaveChanges();

            } catch (Exception e) {
                throw new Exception("Ocorreu um erro ao cadastrar:\n" + e.Message);
            }

        }

        /**
         * Método que recebe uma categoria (editada) e realiza as edições da mesma no banco de dados.
         */
        public void Excluir(int id) {
            try {
                Categoria categoria = database.Categorias.FirstOrDefault(c => c.Id == id);
                database.Categorias.Remove(categoria);
                database.SaveChanges();

            } catch (Exception e) {
                throw new Exception("Ocorreu um erro ao inativar:\n" + e.Message);
            }

        }

        /**
        * Método que recebe uma categoria (editada) e realiza as edições da mesma no banco de dados.
        */
        public List<Categoria> ListarCategorias() {
            return database.Categorias.ToList();
        }

        public Categoria BuscarCategoriaPeloId(int id) {
            return database.Categorias.FirstOrDefault(c => c.Id == id);
        }
    }
}
