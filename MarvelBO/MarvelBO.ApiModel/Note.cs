using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelBO.ApiModel
{
    public class Note
    {
        public int Id { get; set; }
        public string CreatorName { get; set; }
        public string Content { get; set; }

        public override string ToString()
        {
            return String.Format(
                "Id: {0}, Author: {1}, Content: {2}.",
                Id, CreatorName, Content);
        }
    }
}
