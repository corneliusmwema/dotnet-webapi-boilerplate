using FluentValidation;
using FSH.Framework.Application.Persistence;
using FSH.Framework.Application.Validation;
using FSH.WebApi.Catalog.Application.Brands.Exceptions;
using FSH.WebApi.Catalog.Application.Brands.Specifications;
using FSH.WebApi.Catalog.Domain.Brands;
using MediatR;
using Microsoft.Extensions.Localization;

namespace FSH.WebApi.Catalog.Application.Brands.Features;
public static class UpdateBrand
{
    public sealed class Request : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }

    internal sealed class Validator : FshValidator<Request>
    {
        public Validator(IRepository<Brand> repository, IStringLocalizer<Validator> T) =>
            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(75)
                .MustAsync(async (brand, name, ct) =>
                        await repository.FirstOrDefaultAsync(new BrandByNameSpec(name), ct)
                            is not Brand existingBrand || existingBrand.Id == brand.Id)
                    .WithMessage((_, name) => T["Brand {0} already Exists.", name]);
    }

    internal sealed class Handler : IRequestHandler<Request, Guid>
    {
        private readonly IRepositoryWithEvents<Brand> _repository;
        public Handler(IRepositoryWithEvents<Brand> repository)
        {
            _repository = repository;
        }
        public async Task<Guid> Handle(Request request, CancellationToken cancellationToken)
        {
            var brand = await _repository.GetByIdAsync(request.Id, cancellationToken);
            _ = brand ?? throw new BrandNotFoundException(request.Id);
            brand.Update(request.Name, request.Description);
            await _repository.UpdateAsync(brand, cancellationToken);
            return request.Id;
        }
    }
}
