using Domain;

namespace CloudDrive.Dto;

public static class NoteExtension
{
    public static Note ToDomain(this CreateNoteDto note)
    {
        return new Note() { 
            Name = note.Title,
            Description = note.Description,
        };
    }
}