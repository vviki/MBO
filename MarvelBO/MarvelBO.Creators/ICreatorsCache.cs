using MarvelBO.Creators.MarvelModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelBO.Creators
{
    public interface ICreatorsCache
    {
        bool IsEmpty();
        void Fill(IEnumerable<Creator> creators);
        IEnumerable<Creator> List();
        bool Exists(int id);
        Creator Get(int id);
    }
}
