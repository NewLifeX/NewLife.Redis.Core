using NewLife.Caching;
using NewLife.Caching.Models;

namespace NewLife.Redis.Core
{
    /// <summary>
    /// HashMap
    /// </summary>
    public partial class NewLifeRedis : INewLifeRedis
    {

        /// <inheritdoc />
        public RedisHash<string, T> GetHashMap<T>(string key)
        {
            return redisConnection.GetDictionary<T>(key) as RedisHash<string, T>;
        }

        /// <inheritdoc />
        public bool HashSet<T>(string key, Dictionary<string, T> dic)
        {
            var hash = GetHashMap<T>(key);
            return hash.HMSet(dic);
        }
        /// <inheritdoc />
        public void HashAdd<T>(string key, string hashKey, T value)
        {
            var hash = GetHashMap<T>(key);
            hash.Add(hashKey, value);
        }

        /// <inheritdoc />
        public List<T> HashGet<T>(string key, params string[] fields)
        {
            var hash = GetHashMap<T>(key);
            var result = hash.HMGet(fields);
            return result.ToList();
        }

        /// <inheritdoc />
        public T HashGetOne<T>(string key, string field)
        {
            var hash = GetHashMap<T>(key);
            var result = hash.HMGet(new string[] { field });
            return result[0];
        }

        /// <inheritdoc />
        public IDictionary<string, T> HashGetAll<T>(string key)
        {
            var hash = GetHashMap<T>(key);
            return hash.GetAll();

        }

        /// <inheritdoc />
        public int HashDel<T>(string key, params string[] fields)
        {
            var hash = GetHashMap<T>(key);
            return hash.HDel(fields);
        }

        /// <inheritdoc />
        public List<KeyValuePair<string, T>> HashSearch<T>(string key, SearchModel searchModel)
        {
            var hash = GetHashMap<T>(key);
            return hash.Search(searchModel).ToList();
        }


        /// <inheritdoc />
        public List<KeyValuePair<string, T>> HashSearch<T>(string key, string pattern, int count)
        {
            var hash = GetHashMap<T>(key);
            return hash.Search(pattern, count).ToList();
        }

    }
}
