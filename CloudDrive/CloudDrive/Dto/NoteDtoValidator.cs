using FluentValidation;

namespace CloudDrive.Dto;

public class NoteDtoValidator : AbstractValidator<NoteDto>
{
    public NoteDtoValidator()
    {
        RuleFor(note => note.Name)
            .NotEmpty()
            .MaximumLength(80);

        RuleFor(note => note.Description)
            .NotEmpty()
            .MaximumLength(255);
    }
}