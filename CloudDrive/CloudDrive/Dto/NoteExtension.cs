﻿using Domain;

namespace CloudDrive.Dto;

public static class NoteExtension
{
    public static Note ToDomain(this NoteDto note)
    {
        return new Note() { 
            Id = note.Id,
            Name = note.Name,
            Description = note.Description,
        };
    }
}