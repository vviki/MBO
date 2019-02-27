using System.Collections.Generic;
using MarvelBO.ApiModel;

namespace MarvelBO.Notes
{
    public interface INotesManager
    {
        NoteOperationStatus AddNote(int creatorId, string content);
        NoteOperationStatus DeleteNote(int creatorId);
        bool TryGetNote(int creatorId, out Note note);
        IEnumerable<Note> ListNotes();
        NoteOperationStatus UpdateNote(int creatorId, string content);
    }
}