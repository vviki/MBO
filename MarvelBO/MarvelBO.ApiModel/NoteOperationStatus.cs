using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelBO.ApiModel
{
    public enum NoteOperationStatus
    {
        NoteSuccessfullyAdded,
        NoteSuccessfullyUpdated,
        NoteSuccessfullyDeleted,
        NoteAlreadyExists,
        NoteNotFound,
        CreatorNotFound,
    }
}
