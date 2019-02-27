using System;
using System.Collections.Generic;
using System.Linq;
using MarvelBO.ApiModel;
using MarvelBO.Creators;
using MarvelBO.Notes;
using Microsoft.AspNetCore.Mvc;

namespace MarvelBO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatorsController : ControllerBase
    {
        ICreatorsManager _creatorsManager;
        INotesManager _notesManager;

        public CreatorsController(ICreatorsManager creatorsManager, INotesManager notesManager)
        {
            _creatorsManager = creatorsManager;
            _notesManager = notesManager;
        }

        [HttpGet]
        public IEnumerable<Creator> Get()
        {
            return _creatorsManager
                .ListCreators()
                .AddNotes(_notesManager)
                .OrderBy(creator => creator.Id);
        }

        [HttpPost]
        public IEnumerable<Creator> Post([FromBody] CreatorsRequest request)
        {
            return _creatorsManager.ListCreators()
                .Where(creator => request.NamePart == null 
                    || creator.FullName.Contains(request.NamePart))
                .FilterBy(request.Id, creator => creator.Id)
                .FilterBy(request.NumberOfComics, creator => creator.NumberOfComics)
                .FilterBy(request.NumberOfSeries, creator => creator.NumberOfSeries)
                .FilterBy(request.ModifiedDate, creator => creator.ModifiedDate)
                .AddNotes(_notesManager)
                .Where(creator => request.NotePart == null 
                    || (creator.Note != null && creator.Note.Contains(request.NotePart)))
                .OrderBy(request.OrderBy, creatorFieldsMap);
        }

        readonly Dictionary<DataField, Func<Creator, IComparable>> creatorFieldsMap 
            = new Dictionary<DataField, Func<Creator, IComparable>>()
            {
                {DataField.id, creator => creator.Id },
                {DataField.name, creator => creator.FullName },
                {DataField.date, creator => creator.ModifiedDate },
                {DataField.comics, creator => creator.NumberOfComics },
                {DataField.series, creator => creator.NumberOfSeries },
                {DataField.note, creator => creator.Note },
            };
    }
}
