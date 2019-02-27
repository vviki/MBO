using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelBO.Creators.MarvelModel
{
    public class Container
    {
        public string Offset { get; set; }
        public string Limit { get; set; }
        public string Total { get; set; }
        public string Count { get; set; }
        public List<Creator> Results { get; set; }
    }
}
