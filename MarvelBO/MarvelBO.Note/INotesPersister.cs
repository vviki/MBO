using System.Collections.Generic;
using MarvelBO.ApiModel;

namespace MarvelBO.Notes
{
    public interface INotesPersister
    {
        void Add(Note note);
        void Delete(int creatorId);
        bool TryGet(int creatorId, out Note note);
        IEnumerable<Note> ListNotes();
    }
}