using System.Collections.Generic;
using System.Linq;

namespace JE.TurningPages.WebApi
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Page<T>(this IEnumerable<T> enumerable, int pageSize, int page)
        {
            return enumerable.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}