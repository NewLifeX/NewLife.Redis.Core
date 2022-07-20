using NewLife.Caching;

namespace NewLife.Redis.Core
{
    /// <inheritdoc cref="IRedisCacheManagerDelay" />
    public partial class NewLifeRedis : IRedisCacheManagerDelay
    {


        /// <inheritdoc />
        public RedisDelayQueue<T> GetDelayQueue<T>(string key)
        {
            var queue = redisConnection.GetDelayQueue<T>(key);
            return queue;
        }

        /// <inheritdoc />
        public int AddDelayQueue<T>(string key, T value, int delay)
        {

            var queue = GetDelayQueue<T>(key);
            return queue.Add(value, delay);
        }

        /// <inheritdoc />
        public int AddDelayQueue<T>(string key, List<T> value, int delay)
        {
            var queue = GetDelayQueue<T>(key);
            queue.Delay = delay;
            return queue.Add(value.ToArray());
        }
    }
}
