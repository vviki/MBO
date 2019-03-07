using MarvelBO.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarvelBO.Creators
{
    public class CreatorsComparator : ICreatorsComparator
    {
        public CreatorsComparison Compare(MarvelModel.Creator firstToCompare, MarvelModel.Creator secondToCompare)
        {
            var commonComicsCount = firstToCompare.Comics.Items.Count(
                comicOfFirst => secondToCompare.Comics.Items.Exists(
                    comicOfSecond => comicOfSecond.ResourceURI == comicOfFirst.ResourceURI));

            var commonSeriesCount = firstToCompare.Series.Items.Count(
                serieOfFirst => secondToCompare.Series.Items.Exists(
                    serieOfSecond => serieOfSecond.ResourceURI == serieOfFirst.ResourceURI));

            var first = ModelMapper.Map(firstToCompare);
            var second = ModelMapper.Map(secondToCompare);

            return new CreatorsComparison()
            {
                FullNameOfFirst = first.FullName,
                FullNameOfSecond = second.FullName,
                IdOfFirst = first.Id,
                IdOfSecond = second.Id,
                ModifiedDateOfFirst = first.ModifiedDate,
                ModifiedDateOfSecond = second.ModifiedDate,
                NumberOfCommonComics = commonComicsCount,
                NumberOfCommonSeries = commonSeriesCount,
                ComparisonStatus = CreatorsComparisonStatus.ComparisonSuccessful,
            };

        }
    }
}
