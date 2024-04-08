namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    internal class UpdateProductCommandHandler
        (IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductCommandHandler.Handle called with {@Command}", command);

            var productToUpdate = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (productToUpdate is null)
            {
                throw new ProductNotFoundException();
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
