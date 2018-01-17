using System.Collections.Generic;
using System.Linq;

namespace ConcreteAPI.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        //Retorna True se o item do Enumerable é nulo ou inexistente
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || enumerable.Any() == false;
        }
    }

}