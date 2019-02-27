using MarvelBO.ApiModel;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MarvelBO.Notes
{
    public class NotesManager : INotesManager
    {
        private INotesPersister _notePersister;

        public NotesManager(INotesPersister notePersister)
        {
            _notePersister = notePersister;
        }

        public IEnumerable<Note> ListNotes()
        {
            return _notePersister.ListNotes();
        }

        public bool TryGetNote(int creatorId, out Note note)
        {
            return _notePersister.TryGet(creatorId, out note);
        }

        public NoteOperationStatus AddNote(int creatorId, string content)
        {
            Note note;

            if (_notePersister.TryGet(creatorId, out note))
                return NoteOperationStatus.NoteAlreadyExists;

            _notePersister.Add(new Note() { Id = creatorId, Content = content });

            return NoteOperationStatus.NoteSuccessfullyAdded;
        }

        public NoteOperationStatus UpdateNote(int creatorId, string content)
        {
            Note note;

            if (!_notePersister.TryGet(creatorId, out note))
                return NoteOperationStatus.NoteNotFound;

            _notePersister.Delete(creatorId);

            _notePersister.Add(new Note() { Id = creatorId, Content = content });

            return NoteOperationStatus.NoteSuccessfullyUpdated;
        }

        public NoteOperationStatus DeleteNote(int creatorId)
        {
            Note note;

            if (!_notePersister.TryGet(creatorId, out note))
                return NoteOperationStatus.NoteNotFound;

            _notePersister.Delete(creatorId);

            return NoteOperationStatus.NoteSuccessfullyDeleted;
        }
    }
}
