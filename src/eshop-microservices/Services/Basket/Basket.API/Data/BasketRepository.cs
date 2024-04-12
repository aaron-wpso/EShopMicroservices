namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) 
        : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasketAsync(string Username, CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<ShoppingCart>(Username, cancellationToken);
            return basket is null ? throw new BasketNotFoundException(Username) : basket;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            session.Store(basket);
            await session.SaveChangesAsync(cancellationToken);
            return basket;

        }
        public async Task<bool> DeleteBasketAsync(string Username, CancellationToken cancellationToken = default)
        {
            session.Delete<ShoppingCart>(Username);
            await session.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
