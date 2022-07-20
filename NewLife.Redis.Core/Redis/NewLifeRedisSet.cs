using NewLife.Caching;
using NewLife.Caching.Models;
using NewLife.Redis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLife.Redis.Core
{
    /// <summary>
    /// Set
    /// </summary>
    public partial class NewLifeRedis : IRedisCacheManager
    {

        /// <inheritdoc />
        public RedisSet<T> GetRedisSet<T>(string key)
        {
            return redisConnection.GetSet<T>(key) as RedisSet<T>;
        }

        /// <inheritdoc />
        public void SetAdd<T>(string key, T value)
        {
            var set = GetRedisSet<T>(key);
            set.Add(value);
        }

        /// <inheritdoc />
        public int SetAddList<T>(string key, T[] value)
        {
            var set = GetRedisSet<T>(key);
            return set.SAdd(value);
        }

        /// <inheritdoc />
        public bool SetDel<T>(string key, T value)
        {
            var set = GetRedisSet<T>(key);
            return set.Remove(value);
        }

        /// <inheritdoc />
        public int SetDelRange<T>(string key, T[] value)
        {
            var set = GetRedisSet<T>(key);
            return set.SDel(value);
        }

        /// <inheritdoc />
        public T[] SetGetAll<T>(string key)
        {
            var set = GetRedisSet<T>(key);
            return set.GetAll();
        }

        /// <inheritdoc />
        public T[] SetRandom<T>(string key, int count)
        {
            var set = GetRedisSet<T>(key);
            return set.RandomGet(count);
        }

        /// <inheritdoc />
        public T[] SetPop<T>(string key, int count)
        {
            var set = GetRedisSet<T>(key);
            return set.Pop(count);
        }

        /// <inheritdoc />
        public List<string> Search<T>(string key, SearchModel model)
        {
            var set = GetRedisSet<T>(key);
            return set.Search(model).ToList();
        }

        /// <inheritdoc />
        public List<string> Search<T>(string key, string pattern, int count)
        {
            var set = GetRedisSet<T>(key);
            return set.Search(pattern, count).ToList();
        }


        /// <inheritdoc />
        public bool SetContains<T>(string key, T value)
        {
            var set = GetRedisSet<T>(key);
            return set.Contains(value);
        }

        /// <inheritdoc />
        public void SetClear<T>(string key)
        {
            var set = GetRedisSet<T>(key);
            set.Clear();
        }

        /// <inheritdoc />
        public void SetCopyTo<T>(string key, T[] array, int arrayIndex)
        {
            var set = GetRedisSet<T>(key);
            set.CopyTo(array, arrayIndex);
        }
    }
}
