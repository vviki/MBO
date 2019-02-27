using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelBO.ApiModel
{
    public class NotesRequest
    {
        public FilterOption<int> Id { get; set; }
        public string NamePart { get; set; }
        public string ContentPart { get; set; }
        public List<OrderOption> OrderBy { get; set; }
    }
}
