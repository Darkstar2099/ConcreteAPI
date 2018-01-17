using System;
using System.Text.RegularExpressions;

namespace ConcreteAPI.Core.Extensions
{
    public static class StringExtensions
    {
        //Retorna somente os números de uma string
        public static string OnlyNumbers(this string value)
        {
            return Regex.Replace(value ?? "", "[^0-9.]", "");
        }

        //Comparação de strings sem levar em conta caixa alta e/ou baixa (case sensitivity)
        public static bool CaselessCompare (this string value, string valueCompared)
        {
            return value?.Equals(valueCompared, StringComparison.OrdinalIgnoreCase) == true;
        }
    }
}