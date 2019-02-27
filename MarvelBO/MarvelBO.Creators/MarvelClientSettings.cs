using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelBO.Creators
{
    public class MarvelClientSettings
    {
        public string Url { get; set; }
        public string PublicKey { get; set; }
        public string PriveteKey { get; set; }
        public int ImportLimit { get; set; }

    }
}
