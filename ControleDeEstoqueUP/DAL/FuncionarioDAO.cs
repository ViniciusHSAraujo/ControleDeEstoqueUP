using ControleDeEstoqueUP.Data;
using ControleDeEstoqueUP.Models;
using ControleSeuEstoque.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeEstoqueUP.DAL {
    class FuncionarioDAO {

        private static ApplicationDbContext database = SingletonContext.GetInstance();

        /**
         * Método que faz a inserção de um funcionário no banco de dados.
         */
        public Funcionario Inserir(Funcionario funcionario) {
            try {
                database.Funcionarios.Add(funcionario);
                database.SaveChanges();
                return funcionario;
            } catch (Exception e) {
                throw new Exception("Ocorreu um erro ao cadastrar:\n" + e.Message);
            }

        }


        /**
         * Método que recebe um funcionário (editado) e realiza as edições do mesmo no banco de dados.
         */
        public void Editar(Funcionario funcionario) {
            try {
                Funcionario funcionarioBuscado = database.Funcionarios.FirstOrDefault(f => f.Id == funcionario.Id);
                funcionarioBuscado.Nome = funcionario.Nome;
                database.SaveChanges();

            } catch (Exception e) {
                throw new Exception("Ocorreu um erro ao cadastrar:\n" + e.Message);
            }

        }

        /**
         * Método que recebe um funcionário (editado) e realiza as edições do mesmo no banco de dados.
         */
        public void Excluir(int id) {
            try {
                var funcionario = database.Funcionarios.FirstOrDefault(f => f.Id == id);
                database.Funcionarios.Remove(funcionario);
                database.SaveChanges();

            } catch (Exception e) {
                throw new Exception("Ocorreu um erro ao inativar:\n" + e.Message);
            }

        }

        /**
        * Método que recebe um funcionário (editado) e realiza as edições do mesmo no banco de dados.
        */
        public List<Funcionario> ListarFuncionarios() {
            return database.Funcionarios.ToList();
        }

        public Funcionario BuscarFuncionarioPeloId(int id) {
            return database.Funcionarios.FirstOrDefault(f => f.Id == id);
        }
    }
}
