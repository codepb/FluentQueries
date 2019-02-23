using System;
using System.Collections.Generic;
using System.Text;

namespace FluentQueries
{
    public static class ObjectExtensions
    {
        public static bool Satisfies<T>(this T obj, IQuery<T> query) => query.IsSatisfiedBy(obj);
    }
}
