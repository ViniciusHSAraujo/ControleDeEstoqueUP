using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ControleSeuEstoque.Utils {
    public static class WPFUtils {

        /**
         * Método que exibe uma Caixa de Alerta de erro.
         */
        public static void MostrarCaixaDeTextoDeErro(string mensagem) {
            MessageBoxResult result = MessageBox.Show($"{mensagem}", "Ocorreu um erro!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /**
         * Método que exibe uma Caixa de Alerta de alerta.
         */
        public static void MostrarCaixaDeTextoDeAlerta(string mensagem) {
            MessageBoxResult result = MessageBox.Show($"{mensagem}", "Atenção!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /**
         * Método que exibe uma Caixa de Alerta de informação.
         */
        public static void MostrarCaixaDeTextoDeInformação(string mensagem) {
            MessageBoxResult result = MessageBox.Show($"{mensagem}", "Informação!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /**
         * Método que exibe uma Caixa de Alerta de confirmação.
         */
        public static bool MostrarCaixaDeTextoDeConfirmação(string mensagem) {

            MessageBoxResult result = MessageBox.Show($"{mensagem}", "Informação!", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK) {
                return true;
            } else {
                return false;
            }

        }
    }
}
