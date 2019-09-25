using System;
using ControleDeEstoqueUP.Data;
using ControleDeEstoqueUP.Models;
using ControleDeEstoqueUP.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeEstoqueUP.DAL {
    class FornecedorDAO {

        private static ApplicationDbContext database = SingletonContext.GetInstance();

        /*
         * Método que Realiza a inserção do Fornecedor no Banco de Dados
         */
        public Fornecedor Inserir(Fornecedor fornecedor) {
            try {
                database.Fornecedores.Add(fornecedor);
                database.SaveChanges();
                return fornecedor;
            } catch (Exception e){
                throw new Exception("Erro ao cadastrar Fornecedor:\n" + e.Message);
            }
        }

        /*
         * Método que realiza a edição do Fornecedor já Cadastrado.
         */
        public void Editar(Fornecedor fornecedor) {
            try {
                Fornecedor fornecedorBanco = database.Fornecedores.FirstOrDefault(f => f.Id == fornecedor.Id);
                fornecedorBanco.RazaoSocial = fornecedor.RazaoSocial;
                fornecedorBanco.RGIE = fornecedor.RGIE;
                fornecedorBanco.CPFCNPJ = fornecedor.CPFCNPJ;
                fornecedorBanco.Endereco = fornecedor.Endereco;
                fornecedorBanco.Bairro = fornecedor.Bairro;
                fornecedorBanco.Cidade = fornecedor.Cidade;
                fornecedorBanco.CEP = fornecedor.CEP;
                fornecedorBanco.UF = fornecedor.UF;
                fornecedorBanco.Email = fornecedor.Email;
                fornecedorBanco.Telefone = fornecedor.Telefone;
                fornecedorBanco.Celular = fornecedor.Celular;
                database.SaveChanges();
            }catch(Exception e) {
                throw new Exception("Ocorrou um erro ao editar o fornecedor:\n" + e.Message);
            }
        }

        /*
         * Método que irá realizar a Exclusão de um fornecedor já cadastrado no Banco de Dados 
         */

        public void Excluir(int id) {
            try {
                var fornecedor = database.Fornecedores.FirstOrDefault(f => f.Id == id);
                database.Fornecedores.Remove(fornecedor);
                database.SaveChanges();
            } catch (Exception e) {
                throw new Exception("Erro ao Inativar o Fornecedor:\n" + e.Message);
            }
        }

        /*
         * Método para Listar os Fornecedores Cadastrados no Banco de Dados;
         */

        public List<Fornecedor> ListarFornecedores() {
            return database.Fornecedores.ToList();
        }

        /*
         * Método que irá Buscar o Fornecedor no Banco de Dados com base no id INFORMADO
         */
        public Fornecedor BuscarFornecedorPorId(int id) {
            return database.Fornecedores.FirstOrDefault(f => f.Id == id);
        }
    }
}
