using System;
using System.Collections.Generic;
using System.Linq;
using MarvelBO.ApiModel;
using MarvelBO.Creators;
using MarvelBO.Notes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarvelBO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        INotesManager _notesManager;
        ICreatorsManager _creatorsManager;

        public NotesController(ICreatorsManager creatorsManager, INotesManager notesManager)
        {
            _creatorsManager = creatorsManager;
            _notesManager = notesManager;
        }

        [HttpGet]
        public IEnumerable<Note> Get()
        {
            return _notesManager.ListNotes()
                .AddCreatorNames(_creatorsManager)
                .OrderBy(creator => creator.Id);
        }

        [HttpPost]
        public IEnumerable<Note> Post([FromBody]NotesRequest request)
        {
            return _notesManager.ListNotes()
                .Where(note => request.ContentPart == null 
                    || note.Content.Contains(request.ContentPart))
                .FilterBy(request.Id, note => note.Id)
                .AddCreatorNames(_creatorsManager)
                .Where(note => request.NamePart == null 
                    || note.CreatorName.Contains(request.NamePart))
                .OrderBy(request.OrderBy, noteFieldsMap);
        }

        readonly Dictionary<DataField, Func<Note, IComparable>> noteFieldsMap 
            = new Dictionary<DataField, Func<Note, IComparable>>()
            {
                {DataField.id, note => note.Id },
                {DataField.note, note => note.Content },
                {DataField.name, note => note.CreatorName },
            };
    }
}
