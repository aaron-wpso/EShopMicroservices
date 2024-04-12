namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasketAsync(string Username, CancellationToken cancellationToken = default);
        Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default);
        Task<bool> DeleteBasketAsync(string Username, CancellationToken cancellationToken = default);
    }
}
