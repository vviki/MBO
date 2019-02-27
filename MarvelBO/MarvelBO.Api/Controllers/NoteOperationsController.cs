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
    public class NoteOperationsController : ControllerBase
    {
        INotesManager _notesManager;
        ICreatorsManager _creatorsManager;

        public NoteOperationsController(ICreatorsManager creatorsManager, INotesManager notesManager)
        {
            _creatorsManager = creatorsManager;
            _notesManager = notesManager;
        }

        [HttpPost]
        public NoteOperationResponse Post([FromBody]  NoteOperationRequest request)
        {
            var result = new NoteOperationResponse()
            {
                OperationStatus = NoteOperationStatus.CreatorNotFound
            };

            if (_creatorsManager.Exists(request.Id))
            {
                result.OperationStatus = _notesManager.AddNote(request.Id, request.Content);
            }

            return result;
        }

        [HttpPut]
        public NoteOperationResponse Put([FromBody]  NoteOperationRequest request)
        {
            return new NoteOperationResponse()
            {
                OperationStatus = _notesManager.UpdateNote(request.Id, request.Content)
            };
        }

        [HttpDelete]
        public NoteOperationResponse Delete([FromBody] NoteOperationRequest request)
        {
            return new NoteOperationResponse()
            {
                OperationStatus = _notesManager.DeleteNote(request.Id)
            };
        }
    }
}
