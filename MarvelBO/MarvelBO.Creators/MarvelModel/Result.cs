using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelBO.Creators.MarvelModel
{
    public class Result
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public string Copyright { get; set; }
        public string AttributionText { get; set; }
        public string AttributionHtml { get; set; }
        public Container Data { get; set; }
        public string Etag { get; set; }
    }
}
