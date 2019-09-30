using ControleDeEstoqueUP.Data;
using ControleDeEstoqueUP.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeEstoqueUP.DAL {
    internal class UnidadeMedidaDAO {

        private static readonly ApplicationDbContext database = SingletonContext.GetInstance();

        /*
         * Método que Realiza a inserção da Unidade de Medida no Banco de Dados
         */

        public UnidadeDeMedida Inserir(UnidadeDeMedida unidadeMed) {
            try {
                database.UnidadeDeMedidas.Add(unidadeMed);
                database.SaveChanges();
                return unidadeMed;
            } catch (Exception e) {
                throw new Exception("Erro ao Cadastrar a Unidade de Medida:\n" + e.Message);
            }
        }

        /*
         * Método que irá Edita a Unidade de Medida já cadastrada no Banco de Dados 
         */
        public void Editar(UnidadeDeMedida unidadeMed) {
            try {
                UnidadeDeMedida unidadeBanco = database.UnidadeDeMedidas.FirstOrDefault(un => un.Id == unidadeMed.Id);
                unidadeBanco.Nome = unidadeMed.Nome;
                unidadeBanco.Simbolo = unidadeMed.Simbolo;
                database.SaveChanges();
            } catch (Exception e) {
                throw new Exception("Ocorreu um erro ao Editar a Unidade de Medida:\n" + e.Message);
            }
        }

        /*
         * Método que irá Excluir a Unidade de Medida que já está cadastrada no Banco de Dados 
         */
        public void Excluir(int id) {
            try {
                UnidadeDeMedida unidadeMedida = database.UnidadeDeMedidas.FirstOrDefault(un => un.Id == id);
                database.UnidadeDeMedidas.Remove(unidadeMedida);
                database.SaveChanges();
            } catch (Exception e) {
                throw new Exception("Erro ao Excluir a Unidade de Medida:\n" + e.Message);
            }
        }

        /*
         * Método que irá listar todas as Unidades de Medidas Cadastradas No Banco de Dados
         */
        public List<UnidadeDeMedida> ListadeUnidades() {
            return database.UnidadeDeMedidas.ToList();
        }

        /*
         * Método que irá Buscar a Unidade de Medida no Banco de Dados com base no id INFORMADO
         */
        public UnidadeDeMedida BuscarUnidadePorId(int id) {
            return database.UnidadeDeMedidas.FirstOrDefault(un => un.Id == id);
        }
    }
}
