using ControleDeEstoqueUP.Data;
using ControleDeEstoqueUP.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeEstoqueUP.DAL {
    internal class ClienteDAO {

        private static readonly ApplicationDbContext database = SingletonContext.GetInstance();

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
                Cliente clienteBuscado = database.Clientes.FirstOrDefault(c => c.Id == cliente.Id);
                clienteBuscado.Nome = cliente.Nome;
                clienteBuscado.CPF = cliente.CPF;
                clienteBuscado.CEP = cliente.CEP;
                clienteBuscado.Endereco = cliente.Endereco;
                clienteBuscado.Bairro = cliente.Bairro;
                clienteBuscado.Cidade = cliente.Cidade;
                clienteBuscado.UF = cliente.UF;
                clienteBuscado.Telefone = cliente.Telefone;
                clienteBuscado.Celular = cliente.Celular;
                clienteBuscado.Email = cliente.Email;
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
                Cliente cliente = database.Clientes.FirstOrDefault(c => c.Id == id);
                database.Clientes.Remove(cliente);
                database.SaveChanges();

            } catch (Exception e) {
                throw new Exception("Ocorreu um erro ao excluir:\n" + e.Message);
            }

        }

        /**
        * Método que recebe um cliente (editado) e realiza as edições do mesmo no banco de dados.
        */
        public List<Cliente> ListarClientes() {
            return database.Clientes.ToList();
        }

        public Cliente BuscarClientePeloId(int id) {
            return database.Clientes.FirstOrDefault(c => c.Id == id);
        }
    }
}
