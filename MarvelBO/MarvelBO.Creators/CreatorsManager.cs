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
        ICreatorsComparator _creatorsComparator;

        public CreatorsManager(ICreatorsCache creatorsCache, IMarvelClient marvelClient, 
            ReaderWriterLockSlim readerWriterLock, ICreatorsComparator creatorsComparator)
        {
            _creatorsCache = creatorsCache;
            _marvelClient = marvelClient;
            _readerWriterLock = readerWriterLock;
            _creatorsComparator = creatorsComparator;
        }

        public CreatorsComparison CompareCreators(int firstId, int secondId)
        {
            return ExecuteOperation(
                () =>
                {
                    var firstExists = _creatorsCache.Exists(firstId);
                    var secondExists = _creatorsCache.Exists(secondId);

                    if (!firstExists && !secondExists)
                        return new CreatorsComparison()
                        {
                            ComparisonStatus = CreatorsComparisonStatus.BothCreatorsDoNotExist
                        };

                    if (!firstExists)
                        return new CreatorsComparison()
                        {
                            ComparisonStatus = CreatorsComparisonStatus.FirstCreatorDoesNotExist
                        };

                    if (!secondExists)
                        return new CreatorsComparison()
                        {
                            ComparisonStatus = CreatorsComparisonStatus.SecondCreatorDoesNotExist
                        };

                    var firstToCompare = _creatorsCache.Get(firstId);
                    var secondToCompare = _creatorsCache.Get(secondId);

                    return _creatorsComparator.Compare(firstToCompare, secondToCompare);
                });
        }

        public Creator GetCreator(int id)
        {
            return ExecuteOperation(() => ModelMapper.Map(_creatorsCache.Get(id)));
        }

        public bool Exists(int id)
        {
            return ExecuteOperation(() => _creatorsCache.Exists(id));
        }

        public IEnumerable<Creator> ListCreators()
        {
            return ExecuteOperation(() => ModelMapper.Map(_creatorsCache.List()));
        }

        private T ExecuteOperation<T>(Func<T> operation)
        {
            _readerWriterLock.EnterUpgradeableReadLock();
            try
            {
                FillCacheIfNeeded();

                return operation();
            }
            finally
            {
                _readerWriterLock.ExitUpgradeableReadLock();
            }
        }

        private void FillCacheIfNeeded()
        {
            if (!_creatorsCache.IsEmpty()) return;

            _readerWriterLock.EnterWriteLock();
            try
            {
                if (!_creatorsCache.IsEmpty()) return;

                _creatorsCache.Fill(_marvelClient.GetCreators());
            }
            finally
            {
                _readerWriterLock.ExitWriteLock();
            }
        }
    }
}
