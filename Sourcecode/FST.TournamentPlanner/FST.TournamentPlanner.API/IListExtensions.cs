using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FST.TournamentPlanner.API
{
    public static class IListExtensions
    {
        private static Random rng = new Random();
        public static List<T> ShuffleToNewList<T>(this List<T> list)
        {
            var res = new List<T>(list);
            int n = res.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = res[k];
                res[k] = res[n];
                res[n] = value;
            }
            return res;
        }
    }
}
