using FluentValidation;

namespace CloudDrive.Dto;

public class NoteDtoValidator : AbstractValidator<NoteDto>
{
    public NoteDtoValidator()
    {
        RuleFor(note => note.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(note => note.Description)
            .NotEmpty()
            .MaximumLength(1000);
    }
}