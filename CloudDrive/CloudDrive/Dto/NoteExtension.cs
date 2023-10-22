using Domain;

namespace CloudDrive.Dto;

public static class NoteExtension
{
    public static Note ToDomain(this NoteDto note)
    {
        return new Note() { 
            Id = 0.ToString(),
            Name = note.Name,
            Description = note.Description,
        };
    }
}