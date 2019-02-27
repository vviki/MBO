using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelBO.Creators.MarvelModel
{
    public class ComicList
    {
        public string Available { get; set; }
        public List<Comic> Items { get; set; }
    }
}
