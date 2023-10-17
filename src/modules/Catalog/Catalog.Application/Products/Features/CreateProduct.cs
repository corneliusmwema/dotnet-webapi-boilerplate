using FluentValidation;
using FSH.Framework.Application.Persistence;
using FSH.Framework.Application.Storage;
using FSH.Framework.Application.Validation;
using FSH.Framework.Domain.Events;
using FSH.WebApi.Catalog.Application.Products.Specifications;
using FSH.WebApi.Catalog.Domain.Brands;
using FSH.WebApi.Catalog.Domain.Products;
using MediatR;
using Microsoft.Extensions.Localization;

namespace FSH.WebApi.Catalog.Application.Products.Features;
public static class CreateProduct
{
    public sealed class Request : IRequest<Guid>
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Rate { get; set; }
        public Guid BrandId { get; set; }
        public FileUploadRequest? Image { get; set; }
    }
    protected sealed class Validator : FshValidator<Request>
    {
        public Validator(IReadRepository<Product> productRepo, IReadRepository<Brand> brandRepo, IStringLocalizer<Validator> T)
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(75)
                .MustAsync(async (name, ct) => await productRepo.FirstOrDefaultAsync(new ProductByNameSpec(name), ct) is null)
                    .WithMessage((_, name) => T["Product {0} already Exists.", name]);
            RuleFor(p => p.Rate)
                .GreaterThanOrEqualTo(1);
            RuleFor(p => p.Image);
            RuleFor(p => p.BrandId)
                .NotEmpty()
                .MustAsync(async (id, ct) => await brandRepo.GetByIdAsync(id, ct) is not null)
                    .WithMessage((_, id) => T["Brand {0} Not Found.", id]);
        }
    }
    protected sealed class Handler : IRequestHandler<Request, Guid>
    {
        private readonly IRepository<Product> _repository;
        private readonly IFileStorageService _file;

        public Handler(IRepository<Product> repository, IFileStorageService file) =>
            (_repository, _file) = (repository, file);

        public async Task<Guid> Handle(Request req, CancellationToken cancellationToken)
        {
            string productImagePath = await _file.UploadAsync<Product>(req.Image, FileType.Image, cancellationToken);
            var product = new Product(req.Name, req.Description, req.Rate, req.BrandId, productImagePath);
            product.DomainEvents.Add(EntityCreatedEvent.WithEntity(product));
            await _repository.AddAsync(product, cancellationToken);
            return product.Id;
        }
    }
}
