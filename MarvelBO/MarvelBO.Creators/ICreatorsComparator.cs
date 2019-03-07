using MarvelBO.ApiModel;
using MarvelBO.Creators.MarvelModel;

namespace MarvelBO.Creators
{
    public interface ICreatorsComparator
    {
        CreatorsComparison Compare(MarvelModel.Creator firstToCompare, MarvelModel.Creator secondToCompare);
    }
}