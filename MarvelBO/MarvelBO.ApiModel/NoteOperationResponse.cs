using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelBO.ApiModel
{
    public class NoteOperationResponse
    {
        public NoteOperationStatus OperationStatus { get; set; }

        public override string ToString()
        {
            switch (OperationStatus)
            {
                case NoteOperationStatus.CreatorNotFound:
                    return "Creator not found!";
                case NoteOperationStatus.NoteAlreadyExists:
                    return "Note already exists!";
                case NoteOperationStatus.NoteNotFound:
                    return "Note not found!";
                case NoteOperationStatus.NoteSuccessfullyAdded:
                    return "Note successfully added!";
                case NoteOperationStatus.NoteSuccessfullyDeleted:
                    return "Note successfully deleted!";
                case NoteOperationStatus.NoteSuccessfullyUpdated:
                    return "Note successfully updated!";
                default:
                    return "???";
            }
        }
    }
}
