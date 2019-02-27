using MarvelBO.ApiModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelBO.Creators
{
    public interface ICreatorsManager
    {
        Creator GetCreator(int id);
        IEnumerable<Creator> ListCreators();
        CreatorsComparison CompareCreators(int firstId, int secontId);
        bool Exists(int id);
    }
}
