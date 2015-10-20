using System;
using System.Linq.Expressions;

namespace Gamepad.Utility.Models
{
    public class Ordering<TSource, TKey> where TSource : class
    {
        public Ordering()
        {
            IsAscending = true;
            Skip = 0;
            Take = 10;
        }


        //public Func<TSource, TKey> OrderByKeySelectorFunc { get; set; }
        public Expression<Func<TSource, TKey>> OrderByKeySelector { get; set; }
        public bool IsAscending { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }

    public class Ordering<TSource> : Ordering<TSource, string> where TSource : class
    {
    }
}
