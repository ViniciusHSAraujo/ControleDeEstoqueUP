using ControleDeEstoqueUP.Data;
using ControleDeEstoqueUP.Models;
using ControleSeuEstoque.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeEstoqueUP.DAL {
    class ClienteDAO {

        private static ApplicationDbContext database = SingletonContext.GetInstance();

        /**
         * Método que faz a inserção de um cliente no banco de dados.
         */
        public Cliente Inserir(Cliente cliente) {
            try {
                database.Clientes.Add(cliente);
                database.SaveChanges();
                return cliente;
            } catch (Exception e) {
                throw new Exception("Ocorreu um erro ao cadastrar:\n" + e.Message);
            }

        }


        /**
         * Método que recebe um cliente (editado) e realiza as edições do mesmo no banco de dados.
         */
        public void Editar(Cliente cliente) {
            try {
                Funcionario clienteBuscado = database.Funcionarios.FirstOrDefault(c => c.Id == cliente.Id);
                clienteBuscado.Nome = cliente.Nome;
                database.SaveChanges();

            } catch (Exception e) {
                throw new Exception("Ocorreu um erro ao cadastrar:\n" + e.Message);
            }

        }

        /**
         * Método que recebe um cliente (editado) e realiza as edições do mesmo no banco de dados.
         */
        public void Excluir(int id) {
            try {
                var cliente = database.Clientes.FirstOrDefault(c => c.Id == id);
                database.Clientes.Remove(cliente);
                database.SaveChanges();

            } catch (Exception e) {
                throw new Exception("Ocorreu um erro ao inativar:\n" + e.Message);
            }

        }

        /**
        * Método que recebe um cliente (editado) e realiza as edições do mesmo no banco de dados.
        */
        public List<Cliente> ListarFuncionarios() {
            return database.Clientes.ToList();
        }

        public Cliente BuscarFuncionarioPeloId(int id) {
            return database.Clientes.FirstOrDefault(c => c.Id == id);
        }
    }
}
