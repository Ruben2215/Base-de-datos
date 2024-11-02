using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace Farmacia__vida
{
    internal class Metodos_de_validacion
    {
        public static bool EsEntero(string valor)
        {
            int resultado;
            return int.TryParse(valor, out resultado);
        }

        public static bool EsDecimal(string valor)
        {
            decimal resultado;
            return decimal.TryParse(valor, out resultado);
        }

        public static bool EsEnteroValido10Digitos(string valor)
        {
            long resultado;
            return long.TryParse(valor, out resultado) && valor.Length == 10;
        }

        public static bool EsTextoValido(string valor)
        {
            return Regex.IsMatch(valor, @"^[a-zA-Z\sñ]+$");
        }

        public static bool EsCorreoValido(string valor)
        {
            return Regex.IsMatch(valor, @"^[a-z\s@._0-9ñ]+$");
        }

        public static bool EsFechaValida(string valor)
        {
            DateTime fecha;
            return DateTime.TryParseExact(valor, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out fecha);
        }
    }
}

