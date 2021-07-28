using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFund.Domain.Extensions
{
    public static class CollectionExtensions
    {
        public static T RandomElement<T>(this IEnumerable<T> enumerable) => enumerable.RandomElementUsing(new Random());

        private static T RandomElementUsing<T>(this IEnumerable<T> enumerable, Random rand)
        {
            var enumerable1 = enumerable as T[] ?? enumerable.ToArray();
            var index = rand.Next(0, enumerable1.Length);
            return enumerable1.ElementAt(index);
        }
    }

}
