using Microsoft.Extensions.Configuration;
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
    /// <inheritdoc cref="IRedisCacheManager" />
    public partial class NewLifeRedis : IRedisCacheManager
    {
        private readonly string redisConnenctionString;
        /// <summary>
        /// Redis实例
        /// </summary>
        public volatile FullRedis redisConnection;
        private readonly object redisConnectionLock = new object();


        /// <summary>
        /// 配置文件注入
        /// </summary>
        /// <param name="configuration"></param>
        /// <exception cref="ArgumentException"></exception>
        public NewLifeRedis(IConfiguration configuration)
        {
            string redisConfiguration = configuration["ConnectionStrings:Redis"];//获取连接字符串
            if (string.IsNullOrWhiteSpace(redisConfiguration))
            {
                throw new ArgumentException("redis config [ConnectionStrings: Redis] is empty", nameof(redisConfiguration));
            }
            this.redisConnenctionString = redisConfiguration;
            this.redisConnection = GetRedisConnection();

        }

        /// <summary>
        /// 通过字符串注入
        /// </summary>
        /// <param name="redisConfiguration">连接字符串</param>
        /// <exception cref="ArgumentException"></exception>
        public NewLifeRedis(string redisConfiguration)
        {
            if (string.IsNullOrWhiteSpace(redisConfiguration))
            {
                throw new ArgumentException("redis config is empty", nameof(redisConfiguration));
            }
            this.redisConnenctionString = redisConfiguration;
            this.redisConnection = GetRedisConnection();
        }


        /// <summary>
        /// 核心代码，获取连接实例
        /// 通过双if 夹lock的方式，实现单例模式
        /// </summary>
        /// <returns></returns>
        private FullRedis GetRedisConnection()
        {
            //如果已经连接实例，直接返回
            if (this.redisConnection != null)
            {
                return this.redisConnection;
            }
            //加锁，防止异步编程中，出现单例无效的问题
            lock (redisConnectionLock)
            {
                if (this.redisConnection != null)
                {
                    //释放redis连接
                    this.redisConnection.Dispose();
                }
                try
                {

                    this.redisConnection = FullRedis.Create(redisConnenctionString);
                    Console.WriteLine("redis启动成功!");

                }
                catch (Exception ex)
                {

                    throw new Exception("Redis服务未启用，请开启该服务");
                }
            }
            return this.redisConnection;
        }



        #region 普通方法

        /// <inheritdoc />
        public FullRedis GetFullRedis()
        {
            return redisConnection;
        }

        /// <inheritdoc />
        public List<string> AllKeys()
        {
            return redisConnection.Keys.ToList();
        }

        /// <inheritdoc />
        public bool ContainsKey(string key)
        {

            return redisConnection.ContainsKey(key);
        }

        /// <inheritdoc />
        public List<string> Search(SearchModel model)
        {
            return redisConnection.Search(model).ToList();
        }

        /// <inheritdoc />
        public long DelByPattern(string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
                return 0;
            //pattern = Regex.Replace(pattern, @"\{*.\}", "(.*)");
            //var keys = redisConnection.Search(new SearchModel { Pattern = pattern });
            var keys = redisConnection.Keys.Where(k => k.StartsWith(pattern));
            //var keys = GetAllKeys().Where(k => k.StartsWith(pattern));
            if (keys != null && keys.Any())
                return redisConnection.Remove(keys.ToArray());
            return 0;
        }

        /// <inheritdoc />
        public void Clear()
        {
            redisConnection.Clear();
        }

        /// <inheritdoc />
        public void SetExpire(string key, TimeSpan timeSpan)
        {
            redisConnection.SetExpire(key, timeSpan);
        }

        /// <inheritdoc />
        public int RemoveByKey(string key, int count)
        {
            var result = 0;
            var keys = redisConnection.Search(key, count).ToList();
            foreach (var k in keys)
                result += redisConnection.Remove(k);
            return result;
        }

        /// <inheritdoc />
        public int RemoveAllByKey(string key, int count = 999)
        {
            var result = 0;
            while (true)
            {
                var keyList = redisConnection.Search(key, count).ToList();
                if (keyList.Count > 0)
                {
                    foreach (var k in keyList)
                        result += redisConnection.Remove(k);
                }
                else
                {
                    break;
                }
            }
            return result;
        }

        /// <inheritdoc />
        public bool Set<TEntity>(string key, TEntity value, TimeSpan cacheTime)
        {
            return redisConnection.Set(key, value, cacheTime);
        }

        /// <inheritdoc />
        public bool Set<TEntity>(string key, TEntity value, int expire = -1)
        {
            return redisConnection.Set(key, value, expire);
        }

        /// <inheritdoc />
        public TEntity Get<TEntity>(string key)
        {
            return redisConnection.Get<TEntity>(key);
        }

        /// <inheritdoc />
        public void Remove(string key)
        {
            redisConnection.Remove(key);
        }

        /// <inheritdoc />
        public bool Rename(string key, string newKey, bool overwrire = true)
        {
            return redisConnection.Rename(key, newKey, overwrire);
        }

        #endregion
    }
}
