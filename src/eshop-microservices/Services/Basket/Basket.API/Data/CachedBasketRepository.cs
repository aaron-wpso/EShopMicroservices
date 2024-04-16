
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository
        (IBasketRepository repository, IDistributedCache cache) 
        : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasketAsync(string Username, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(Username, cancellationToken);
            if(!string.IsNullOrEmpty(cachedBasket))
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;

            var basket = await repository.GetBasketAsync(Username, cancellationToken);
            await cache.SetStringAsync(Username, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            await repository.StoreBasketAsync(basket, cancellationToken);

            await cache.SetStringAsync(basket.Username, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }
        public async Task<bool> DeleteBasketAsync(string Username, CancellationToken cancellationToken = default)
        {
            await repository.DeleteBasketAsync(Username, cancellationToken);

            await cache.RemoveAsync(Username, cancellationToken);

            return true;
        }
    }
}
