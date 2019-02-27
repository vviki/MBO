using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;

namespace MarvelBO.Creators
{
    public class ModelMapper
    {
        public static string DateFormat = "yyyy-MM-ddTHH:mm:ss";
        public static ApiModel.Creator Map(MarvelModel.Creator marvelCreator)
        {
            DateTime dateParsed = DateTime.Now;
            DateTime.TryParseExact(marvelCreator.Modified.Substring(0,19),
                DateFormat,
                CultureInfo.InvariantCulture, 
                DateTimeStyles.AdjustToUniversal, 
                out dateParsed);

            return new ApiModel.Creator()
            {
                FullName = marvelCreator.FullName,
                Id = int.Parse(marvelCreator.Id),
                ModifiedDate = dateParsed,
                NumberOfComics = int.Parse(marvelCreator.Comics.Available),
                NumberOfSeries = int.Parse(marvelCreator.Series.Available),
            };
        }
        public static IEnumerable<ApiModel.Creator> Map(IEnumerable<MarvelModel.Creator> marvelCreators)
        {
            return marvelCreators.Select(marvelCreator => Map(marvelCreator));
        }
    }
}
