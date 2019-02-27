using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MarvelBO.ApiModel;

namespace MarvelBO.Creators
{
    public class CreatorsManager : ICreatorsManager
    {
        ICreatorsCache _creatorsCache;
        IMarvelClient _marvelClient;

        public CreatorsManager(ICreatorsCache creatorsCache, IMarvelClient marvelClient)
        {
            _creatorsCache = creatorsCache;
            _marvelClient = marvelClient;
        }

        public CreatorsComparison CompareCreators(int firstId, int secontId)
        {
            FillCacheIfNeeded();

            var firstToCompare = _creatorsCache.Get(firstId);
            var secondToCompare = _creatorsCache.Get(secontId);

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
                NoteOfFirst = first.Note,
                NoteOfSecond = second.Note,
                NumberOfCommonComics = commonComicsCount,
                NumberOfCommonSeries = commonSeriesCount,
            };
        }

        public Creator GetCreator(int id)
        {
            FillCacheIfNeeded();

            return ModelMapper.Map(_creatorsCache.Get(id));
        }

        public bool Exists(int id)
        {
            FillCacheIfNeeded();

            return _creatorsCache.Exists(id);
        }

        public IEnumerable<Creator> ListCreators()
        {
            FillCacheIfNeeded();

            return ModelMapper.Map(_creatorsCache.List());
        }

        void FillCacheIfNeeded()
        {
            if (!_creatorsCache.IsEmpty()) return;

            _creatorsCache.Fill(_marvelClient.GetCreators());
        }
    }
}
