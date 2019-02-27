using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarvelBO.ApiModel;
using MarvelBO.Creators;
using MarvelBO.Notes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarvelBO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatorsComparisonController : ControllerBase
    {
        ICreatorsManager _creatorsManager;
        INotesManager _notesManager;

        public CreatorsComparisonController(
            ICreatorsManager creatorsManager,
            INotesManager notesManager)
        {
            _creatorsManager = creatorsManager;
            _notesManager = notesManager;
        }

        [HttpGet]
        public CreatorsComparison Get(int firstId, int secondId)
        {
            return PrepareResponse(firstId, secondId);
        }

        [HttpPost]
        public CreatorsComparison Post([FromBody] CreatorsComparisonRequest request)
        {
            return PrepareResponse(request.FirstId, request.SecondId);
        }

        CreatorsComparison PrepareResponse(int firstId, int secondId)
        {
            var comparation = _creatorsManager.CompareCreators(firstId, secondId);

            Note note;

            if (_notesManager.TryGetNote(comparation.IdOfFirst, out note))
            {
                comparation.NoteOfFirst = note.Content;
            }
            if (_notesManager.TryGetNote(comparation.IdOfSecond, out note))
            {
                comparation.NoteOfSecond = note.Content;
            }
            return comparation;
        }
    }
}
