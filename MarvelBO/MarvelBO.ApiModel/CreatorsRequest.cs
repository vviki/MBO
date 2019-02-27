using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelBO.ApiModel
{
    public class CreatorsRequest
    {
        public FilterOption<int> Id { get; set; }
        //public FilterType? IdFilterType { get; set; }
        public string NamePart { get; set; }
        public string NotePart { get; set; }
        public FilterOption<int> NumberOfSeries { get; set; }
        //public FilterType? NumberOfSeriesFilterType { get; set; }
        public FilterOption<int> NumberOfComics { get; set; }
        //public FilterType? NumberOfComicsFilterType { get; set; }
        public FilterOption<DateTime> ModifiedDate { get; set; }
        //public FilterType? ModifiedDateFilterType { get; set; }
        public List<OrderOption> OrderBy { get; set; }
    }
}
