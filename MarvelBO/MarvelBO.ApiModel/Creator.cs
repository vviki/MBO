using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelBO.ApiModel
{
    public class Creator
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int NumberOfSeries { get; set; }

        public int NumberOfComics { get; set; }

        public string Note { get; set; }

        public override string ToString()
        {
            return String.Format(
                "Id: {0}, FullName: {1}, ModifiedDate: {2}, NumberOfSeries: {3}, NumberOfComics: {4}, Note: {5}.", 
                Id, FullName, ModifiedDate, NumberOfSeries, NumberOfComics, Note);
        }
    }
}
