using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelBO.Creators.MarvelModel
{
    public class Creator
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Modified { get; set; }

        public SeriesList Series { get; set; }

        public ComicList Comics { get; set; }

    }
}
