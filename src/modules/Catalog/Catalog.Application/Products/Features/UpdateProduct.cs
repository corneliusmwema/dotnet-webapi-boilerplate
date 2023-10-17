using FluentValidation;
using FSH.Framework.Application.Persistence;
using FSH.Framework.Application.Storage;
using FSH.Framework.Application.Validation;
using FSH.Framework.Domain.Events;
using FSH.WebApi.Catalog.Application.Products.Exceptions;
using FSH.WebApi.Catalog.Application.Products.Specifications;
using FSH.WebApi.Catalog.Domain.Brands;
using FSH.WebApi.Catalog.Domain.Products;
using MediatR;
using Microsoft.Extensions.Localization;

namespace FSH.WebApi.Catalog.Application.Products.Features;
internal class UpdateProduct
{
    internal sealed class Request : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Rate { get; set; }
        public Guid BrandId { get; set; }
        public bool DeleteCurrentImage { get; set; }
        public FileUploadRequest? Image { get; set; }
    }

    internal sealed class Validator : FshValidator<Request>
    {
        public Validator(IReadRepository<Product> productRepo, IReadRepository<Brand> brandRepo, IStringLocalizer<Validator> T)
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(75)
                .MustAsync(async (product, name, ct) =>
                        await productRepo.FirstOrDefaultAsync(new ProductByNameSpec(name), ct)
                            is not Product existingProduct || existingProduct.Id == product.Id)
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

    internal sealed class Handler : IRequestHandler<Request, Guid>
    {
        private readonly IRepository<Product> _repository;
        private readonly IStringLocalizer _t;
        private readonly IFileStorageService _file;

        public Handler(IRepository<Product> repository, IStringLocalizer<Handler> localizer, IFileStorageService file) =>
            (_repository, _t, _file) = (repository, localizer, file);

        public async Task<Guid> Handle(Request request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id, cancellationToken);
            _ = product ?? throw new ProductNotFoundException(request.Id);
            if (request.DeleteCurrentImage)
            {
                string? currentProductImagePath = product.ImagePath;
                if (!string.IsNullOrEmpty(currentProductImagePath))
                {
                    string root = Directory.GetCurrentDirectory();
                    _file.Remove(Path.Combine(root, currentProductImagePath));
                }
                product = product.ClearImagePath();
            }
            string? productImagePath = request.Image is not null
                ? await _file.UploadAsync<Product>(request.Image, FileType.Image, cancellationToken)
                : null;
            var updatedProduct = product.Update(request.Name, request.Description, request.Rate, request.BrandId, productImagePath);
            product.DomainEvents.Add(EntityUpdatedEvent.WithEntity(product));
            await _repository.UpdateAsync(updatedProduct, cancellationToken);
            return request.Id;
        }
    }
}
