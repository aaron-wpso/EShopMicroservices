namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator() 
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Product ID is required.");

            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 150).WithMessage("Name must be between 2 and 150 characters.");

            RuleFor(command => command.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");
        }
    }

    internal class UpdateProductCommandHandler
        (IDocumentSession session)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var productToUpdate = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (productToUpdate is null)
            {
                throw new ProductNotFoundException(command.Id);
            }

            productToUpdate.Name = command.Name;
            productToUpdate.Category = command.Category;
            productToUpdate.Description = command.Description;
            productToUpdate.ImageFile = command.ImageFile;
            productToUpdate.Price = command.Price;

            session.Update(productToUpdate);
            await session.SaveChangesAsync();

            return new UpdateProductResult(true);
        }
    }
}
