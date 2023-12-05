using NewLife.Caching;
using NewLife.Caching.Queues;
namespace NewLife.Redis.Core
{
    /// <summary>
    /// 普通队列 
    /// </summary>
    public partial class NewLifeRedis : INewLifeRedis
    {

        /// <inheritdoc />
        public RedisQueue<T> GetRedisQueue<T>(string key)
        {
            return redisConnection.GetQueue<T>(key) as RedisQueue<T>;
        }

        /// <inheritdoc />
        public int AddQueue<T>(string key, T[] value)
        {
            var queue = GetRedisQueue<T>(key);
            return queue.Add(value);
        }

        /// <inheritdoc />
        public int AddQueue<T>(string key, T value)
        {
            var queue = GetRedisQueue<T>(key);
            return queue.Add(value);
        }

        /// <inheritdoc />
        public List<T> GetQueue<T>(string key, int Count = 1)
        {
            var queue = GetRedisQueue<T>(key);
            var result = queue.Take(Count).ToList();

            return result;
        }

        /// <inheritdoc />
        public T GetQueueOne<T>(string key, int timeout = 1)
        {
            var queue = GetRedisQueue<T>(key);
            return queue.TakeOne(timeout);
        }

        /// <inheritdoc />
        public async Task<T> GetQueueOneAsync<T>(string key, int timeout = 1)
        {
            var queue = GetRedisQueue<T>(key);
            return await queue.TakeOneAsync(1);
        }

    }
}
