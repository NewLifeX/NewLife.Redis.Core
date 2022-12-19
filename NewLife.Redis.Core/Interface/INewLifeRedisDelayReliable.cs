using NewLife.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLife.Redis.Core
{
    /// <summary>
    /// Redis实例
    /// </summary>
    public partial interface INewLifeRedis
    {
        /// <summary>
        /// 添加到可信队列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>添加数量</returns>
        int AddReliableQueue<T>(string key, T value);

        /// <summary>
        /// 批量添加到可信队列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>添加数量</returns>
        int AddReliableQueueList<T>(string key, List<T> value);

        /// <summary>
        /// 获取可信队列实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <returns>可信队列实例</returns>
        RedisReliableQueue<T> GetRedisReliableQueue<T>(string key);

        /// <summary>
        /// 从可信队列获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="count">数量</param>
        /// <returns>数据列表</returns>
        List<T> ReliableTake<T>(string key, int count);

        /// <summary>
        /// 从可信队列获取一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <returns>数据</returns>
        T ReliableTakeOne<T>(string key);

        /// <summary>
        /// 异步从可信队列获取一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <returns>数据</returns>
        Task<T> ReliableTakeOneAsync<T>(string key);

        /// <summary>
        /// 回滚所有未消费完成的数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="retryInterval">间隔</param>
        /// <returns>回滚数量</returns>
        int RollbackAllAck(string key, int retryInterval = 60);
    }
}
