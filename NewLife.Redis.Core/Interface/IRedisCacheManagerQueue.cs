using NewLife.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLife.Redis.Core
{
    /// <summary>
    /// Redis管理中心
    /// </summary>
    public partial interface IRedisCacheManager
    {
        /// <summary>
        /// 添加到队列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值数组</param>
        /// <returns>添加数量</returns>
        int AddQueue<T>(string key, T[] value);

        /// <summary>
        /// 添加到队列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>添加数量</returns>
        int AddQueue<T>(string key, T value);

        /// <summary>
        /// 获取队列实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <returns>队列实例</returns>
        RedisQueue<T> GetRedisQueue<T>(string key);

        /// <summary>
        /// 从队列中获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="Count">数量</param>
        /// <returns>数据列表</returns>
        List<T> GetQueue<T>(string key, int Count = 1);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <returns>数据</returns>
        T GetQueueOne<T>(string key);

        /// <summary>
        /// 异步取一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <returns>数据</returns>
        Task<T> GetQueueOneAsync<T>(string key);
    }
}
