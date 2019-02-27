using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelBO.ApiModel
{
    public class FilterOption<T>
        where T : struct, IComparable
    {
        public FilterType? Type { get; set; }
        public T? Value { get; set; }
    }
}
