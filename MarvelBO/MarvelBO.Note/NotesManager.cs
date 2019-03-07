using MarvelBO.ApiModel;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MarvelBO.Notes
{
    public class NotesManager : INotesManager
    {
        private INotesPersister _notePersister;
        private ReaderWriterLockSlim _readerWriterLock;

        public NotesManager(INotesPersister notePersister,
            ReaderWriterLockSlim readerWriterLock)
        {
            _notePersister = notePersister;
            _readerWriterLock = readerWriterLock;
        }

        public IEnumerable<Note> ListNotes()
        {
            return ExecuteReadOperation(() => _notePersister.ListNotes());
        }

        public bool TryGetNote(int creatorId, out Note note)
        {
            Note localNote = null;

            var result = ExecuteReadOperation(() => _notePersister.TryGet(creatorId, out localNote));

            note = localNote;

            return result;
        }

        public NoteOperationStatus AddNote(int creatorId, string content)
        {
            Note note;

            return ExecuteWriteOperation(
                () => _notePersister.TryGet(creatorId, out note),
                NoteOperationStatus.NoteAlreadyExists,
                () =>
                {
                    _notePersister.Add(new Note() { Id = creatorId, Content = content });

                    return NoteOperationStatus.NoteSuccessfullyAdded;
                });
        }

        public NoteOperationStatus UpdateNote(int creatorId, string content)
        {
            Note note;

            return ExecuteWriteOperation(
                () => !_notePersister.TryGet(creatorId, out note),
                NoteOperationStatus.NoteNotFound,
                () =>
                {
                    _notePersister.Delete(creatorId);

                    _notePersister.Add(new Note() { Id = creatorId, Content = content });

                    return NoteOperationStatus.NoteSuccessfullyUpdated;
                });
        }

        public NoteOperationStatus DeleteNote(int creatorId)
        {
            Note note;

            return ExecuteWriteOperation(
                () => !_notePersister.TryGet(creatorId, out note),
                NoteOperationStatus.NoteNotFound,
                () =>
                {
                    _notePersister.Delete(creatorId);

                    return NoteOperationStatus.NoteSuccessfullyDeleted;
                });
        }

        private T ExecuteReadOperation<T>(Func<T> operation)
        {
            _readerWriterLock.EnterReadLock();
            try
            {
                return operation();
            }
            finally
            {
                _readerWriterLock.ExitReadLock();
            }
        }

        private NoteOperationStatus ExecuteWriteOperation(
            Func<bool> existenceCheck, 
            NoteOperationStatus existenceCheckFailedStatus,
            Func<NoteOperationStatus> operation)
        {
            _readerWriterLock.EnterUpgradeableReadLock();
            try
            {
                if (existenceCheck())
                    return existenceCheckFailedStatus;

                _readerWriterLock.EnterWriteLock();
                try
                {
                    if (existenceCheck())
                        return existenceCheckFailedStatus;

                    return operation();
                }
                finally
                {
                    _readerWriterLock.ExitWriteLock();
                }
            }
            finally
            {
                _readerWriterLock.ExitUpgradeableReadLock();
            }
        }
    }
}
