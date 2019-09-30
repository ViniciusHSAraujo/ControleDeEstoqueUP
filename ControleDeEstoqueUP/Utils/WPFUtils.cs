using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ControleDeEstoqueUP.Utils {
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


        static IEnumerable<char> map = new char[] {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K',
        'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W',
        'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g',
        'h', 'j', 'k', 'm', 'n', 'p', 'q', 'r', 's', 't',
        'u', 'v', 'x', 'y', 'z', '2', '3', '4',
        };
        public static string Encode(long inp) {
            Debug.Assert(inp >= 0, "not implemented for negative numbers");

            var b = map.Count();
            // value -> character
            var toChar = map.Select((v, i) => new { Value = v, Index = i }).ToDictionary(i => i.Index, i => i.Value);
            var res = "";
            if (inp == 0) {
                return "" + toChar[0];
            }
            while (inp > 0) {
                // encoded least-to-most significant
                var val = (int)(inp % b);
                inp = inp / b;
                res += toChar[val];
            }
            return res;
        }

        public static long Decode(string encoded) {
            var b = map.Count();
            // character -> value
            var toVal = map.Select((v, i) => new { Value = v, Index = i }).ToDictionary(i => i.Value, i => i.Index);
            long res = 0;
            // go in reverse to mirror encoding
            for (var i = encoded.Length - 1; i >= 0; i--) {
                var ch = encoded[i];
                var val = toVal[ch];
                res = (res * b) + val;
            }
            return res;
        }
    }
}
