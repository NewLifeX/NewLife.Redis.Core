using NewLife.Caching;

namespace NewLife.Redis.Core
{
    /// <summary>
    /// Redis实例
    /// </summary>
    public partial interface INewLifeRedis
    {
        /// <summary>
        /// 添加一条数据到延迟队列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="delay">延迟时间</param>
        /// <returns>添加成功数量</returns>
        int AddDelayQueue<T>(string key, T value, int delay);

        /// <summary>
        /// 批量添加数据到延迟队列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="delay">延迟时间</param>
        /// <returns>添加成功数量</returns>
        int AddDelayQueue<T>(string key, List<T> value, int delay);

        /// <summary>
        /// 获取延迟队列实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        RedisDelayQueue<T> GetDelayQueue<T>(string key);
    }
}
