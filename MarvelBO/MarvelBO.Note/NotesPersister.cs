using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MarvelBO.ApiModel;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MarvelBO.Notes
{
    public class NotesPersister : INotesPersister
    {
        IDatabase _database;
        IServer _server;
        const string NOTE_PREFIX = "n";

        public NotesPersister(IDatabase database, IServer server)
        {
            _server = server;
            _database = database;
        }

        public void Add(Note note)
        {
            _database.StringSet(NOTE_PREFIX + note.Id, JsonConvert.SerializeObject(note));
        }

        public void Delete(int creatorId)
        {
            _database.KeyDelete(NOTE_PREFIX + creatorId);
        }

        public bool TryGet(int creatorId, out Note note)
        {
            var noteString = _database.StringGet(NOTE_PREFIX + creatorId);
            if (!noteString.HasValue)
            {
                note = null;
                return false;
            }

            note = JsonConvert.DeserializeObject<Note>(noteString);
            return true;
        }

        public IEnumerable<Note> ListNotes()
        {
            var keys = _server.Keys(pattern: NOTE_PREFIX + "*");
            return keys.Select(
                key => JsonConvert.DeserializeObject<Note>(_database.StringGet(key)));
        }
    }
}
