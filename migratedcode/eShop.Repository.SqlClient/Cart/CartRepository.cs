using eShop.Domain.Cart;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Repository.SqlClient.Cart
{
    public class CartRepository : ICartRepository
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public CartRepository(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = redis.GetDatabase();
        }

        public bool DeleteCart(string cartId)
        {
            return _database.KeyDelete(cartId);
        }

        public async Task<bool> DeleteCartAsync(string cartId)
        {
            return await _database.KeyDeleteAsync(cartId);
        }

        public CustomerCart GetCart(string customerId)
        {
            var data = _database.StringGet(customerId);

            if (data.IsNullOrEmpty)
            {
                return null;
            }

            return JsonSerializer.Deserialize<CustomerCart>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<CustomerCart> GetCartAsync(string customerId)
        {
            var data = await _database.StringGetAsync(customerId);
            if (data.IsNullOrEmpty)
            {
                return null;
            }

            return JsonSerializer.Deserialize<CustomerCart>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        private IServer GetServer()
        {
            var endpoint = _redis.GetEndPoints();
            return _redis.GetServer(endpoint.First());
        }

        public IEnumerable<string> GetUsers()
        {
            var server = GetServer();
            var redisKeys = server.Keys();

            return redisKeys?.Select(key => key.ToString());
        }

        public CustomerCart UpdateCart(CustomerCart cart)
        {
            var successfulCreate = _database.StringSet(cart.BuyerId, JsonSerializer.Serialize(cart));

            if (!successfulCreate)
            {
                // log the error
                return null;
            }
            return GetCart(cart.BuyerId);
        }

        public async Task<CustomerCart> UpdateCartAsync(CustomerCart cart)
        {
            var customerCart = await _database.StringSetAsync(cart.BuyerId, JsonSerializer.Serialize(cart));

            if (!customerCart)
            {
                return null;
            }

            return await GetCartAsync(cart.BuyerId);
        }
    }
}
