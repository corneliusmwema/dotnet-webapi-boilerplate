using FluentValidation;
using FSH.Framework.Application.Persistence;
using FSH.Framework.Application.Validation;
using FSH.WebApi.Catalog.Application.Brands.Specifications;
using FSH.WebApi.Catalog.Domain.Brands;
using MediatR;
using Microsoft.Extensions.Localization;

namespace FSH.WebApi.Catalog.Application.Brands.Features;
public static class CreateBrand
{
    public class Request : IRequest<Guid>
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }

    public class Validator : FshValidator<Request>
    {
        public Validator(IReadRepository<Brand> repository, IStringLocalizer<Validator> T) =>
            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(75)
                .MustAsync(async (name, ct) => await repository.FirstOrDefaultAsync(new BrandByNameSpec(name), ct) is null)
                    .WithMessage((_, name) => T["Brand {0} already Exists.", name]);
    }

    public class Handler : IRequestHandler<Request, Guid>
    {
        private readonly IRepositoryWithEvents<Brand> _repository;
        public Handler(IRepositoryWithEvents<Brand> repository) => _repository = repository;
        public async Task<Guid> Handle(Request request, CancellationToken cancellationToken)
        {
            var brand = new Brand(request.Name, request.Description);
            await _repository.AddAsync(brand, cancellationToken);
            return brand.Id;
        }
    }
}
