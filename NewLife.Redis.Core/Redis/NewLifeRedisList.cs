using NewLife.Caching;
using NewLife.Redis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLife.Redis.Core
{
    /// <summary>
    /// 列表
    /// </summary>
    public partial class NewLifeRedis : INewLifeRedis
    {
        /// <inheritdoc />
        public RedisList<T> GetRedisList<T>(string key)
        {
            return redisConnection.GetList<T>(key) as RedisList<T>;
        }

        /// <inheritdoc />
        public void ListAdd<T>(string key, T values)
        {
            var list = GetRedisList<T>(key);
            list.Add(values);
        }

        /// <inheritdoc />
        public int ListAddRange<T>(string key, IEnumerable<T> values)
        {
            var list = GetRedisList<T>(key);
            return list.AddRange(values);
        }

        /// <inheritdoc />
        public T ListGet<T>(string key, T value)
        {
            var list = GetRedisList<T>(key);
            return list[list.IndexOf(value)];
        }

        /// <inheritdoc />
        public T[] ListGetRange<T>(string key, int start, int end)
        {
            var list = GetRedisList<T>(key);
            return list.LRange(start, end);
        }

        /// <inheritdoc />
        public List<T> ListGetAll<T>(string key)
        {
            var list = GetRedisList<T>(key);
            return list.GetAll().ToList();
        }

        /// <inheritdoc />
        public bool ListContains<T>(string key, T value)
        {
            var list = GetRedisList<T>(key);
            return list.Contains(value);
        }

        /// <inheritdoc />
        public bool ListRemove<T>(string key, T value)
        {
            var list = GetRedisList<T>(key);
            return list.Remove(value);
        }

        /// <inheritdoc />
        public void ListRemove<T>(string key, int index)
        {
            var list = GetRedisList<T>(key);
            list.RemoveAt(index);
        }

        /// <inheritdoc />
        public void ListClear<T>(string key)
        {
            var list = GetRedisList<T>(key);
            list.Clear();
        }

        /// <inheritdoc />
        public void ListCopyTo<T>(string key, T[] array, int arrayIndex)
        {
            var list = GetRedisList<T>(key);
            list.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc />
        public int ListIndexOf<T>(string key, T value)
        {
            var list = GetRedisList<T>(key);
            return list.IndexOf(value);
        }

        /// <inheritdoc />
        public int ListRightPush<T>(string key, IEnumerable<T> values)
        {
            var list = GetRedisList<T>(key);
            return list.RPUSH(values);
        }

        /// <inheritdoc />
        public int ListLeftPush<T>(string key, IEnumerable<T> values)
        {
            var list = GetRedisList<T>(key);
            return list.LPUSH(values);
        }

        /// <inheritdoc />
        public T ListLeftPop<T>(string key)
        {
            var list = GetRedisList<T>(key);
            return list.LPOP();
        }

        /// <inheritdoc />
        public T ListRightPop<T>(string key)
        {
            var list = GetRedisList<T>(key);
            return list.RPOP();
        }

    }
}
