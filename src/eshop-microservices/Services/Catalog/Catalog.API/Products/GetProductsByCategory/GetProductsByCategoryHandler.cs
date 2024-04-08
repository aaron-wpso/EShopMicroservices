﻿using Catalog.API.Products.GetProductById;
using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProductsByCategory
{
    public record GetProductsByCategoryQuery(string category) : IQuery<GetProductsByCategoryQueryResult>;
    public record GetProductsByCategoryQueryResult(IEnumerable<Product> Products);
    internal class GetProductsByCategoryQueryHandler 
        (IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryQueryResult>
    {
        public async Task<GetProductsByCategoryQueryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCategoryQueryHandler.Handle called with {@Query}", query);

            var products = await session.Query<Product>()
                .Where(x => x.Category.Contains(query.category))
                .ToListAsync();

            return new GetProductsByCategoryQueryResult(products);
        }
    }
}
