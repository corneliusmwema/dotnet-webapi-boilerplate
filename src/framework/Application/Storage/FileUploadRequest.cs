using FluentValidation;
using FSH.Framework.Application.Validation;
using Microsoft.Extensions.Localization;

namespace FSH.Framework.Application.Storage;

public class FileUploadRequest
{
    public string Name { get; set; } = default!;
    public string Extension { get; set; } = default!;
    public string Data { get; set; } = default!;
}

public class FileUploadRequestValidator : FshValidator<FileUploadRequest>
{
    public FileUploadRequestValidator(IStringLocalizer<FileUploadRequestValidator> T)
    {
        RuleFor(p => p.Name)
            .NotEmpty()
                .WithMessage(T["File Name cannot be empty!"])
            .MaximumLength(150);

        RuleFor(p => p.Extension)
            .NotEmpty()
                .WithMessage(T["File Extension cannot be empty!"])
            .MaximumLength(5);

        RuleFor(p => p.Data)
            .NotEmpty()
                .WithMessage(T["File Data cannot be empty!"]);
    }
}