using MarvelBO.Creators.MarvelModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelBO.Creators
{
    public interface IMarvelClient
    {
        IEnumerable<Creator> GetCreators();
    }
}
