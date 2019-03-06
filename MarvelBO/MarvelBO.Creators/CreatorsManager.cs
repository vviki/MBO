using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MarvelBO.ApiModel;
using System.Threading;

namespace MarvelBO.Creators
{
    public class CreatorsManager : ICreatorsManager
    {
        ICreatorsCache _creatorsCache;
        IMarvelClient _marvelClient;
        ReaderWriterLockSlim _readerWriterLock;

        public CreatorsManager(ICreatorsCache creatorsCache, IMarvelClient marvelClient, 
            ReaderWriterLockSlim readerWriterLock)
        {
            _creatorsCache = creatorsCache;
            _marvelClient = marvelClient;
            _readerWriterLock = readerWriterLock;
        }

        public CreatorsComparison CompareCreators(int firstId, int secontId)
        {
            _readerWriterLock.EnterUpgradeableReadLock();
            try
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
            finally
            {
                _readerWriterLock.ExitUpgradeableReadLock();
            }
        }

        public Creator GetCreator(int id)
        {
            _readerWriterLock.EnterUpgradeableReadLock();
            try
            {
                FillCacheIfNeeded();

                return ModelMapper.Map(_creatorsCache.Get(id));
            }
            finally
            {
                _readerWriterLock.ExitUpgradeableReadLock();
            }
        }

        public bool Exists(int id)
        {
            _readerWriterLock.EnterUpgradeableReadLock();
            try
            {
                FillCacheIfNeeded();

                return _creatorsCache.Exists(id);
            }
            finally
            {
                _readerWriterLock.ExitUpgradeableReadLock();
            }
        }

        public IEnumerable<Creator> ListCreators()
        {
            _readerWriterLock.EnterUpgradeableReadLock();
            try
            {
                FillCacheIfNeeded();

                return ModelMapper.Map(_creatorsCache.List());
            }
            finally
            {
                _readerWriterLock.ExitUpgradeableReadLock();
            }
        }

        void FillCacheIfNeeded()
        {
            if (!_creatorsCache.IsEmpty()) return;

            _readerWriterLock.EnterWriteLock();
            try
            {
                _creatorsCache.Fill(_marvelClient.GetCreators());
            }
            finally
            {
                _readerWriterLock.ExitWriteLock();
            }
        }
    }
}
