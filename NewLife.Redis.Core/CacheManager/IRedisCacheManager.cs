namespace NewLife.Redis.Core
{
    /// <summary>
    /// Redis缓存中心
    /// </summary>
    public interface IRedisCacheManager
    {
        /// <summary>
        /// 添加redis实例
        /// </summary>
        /// <param name="config">配置文件</param>
        void AddRedis(RedisConfig config);

        /// <summary>
        /// 获取Redis实例
        /// </summary>
        /// <param name="name">实例名称</param>
        /// <returns></returns>
        NewLifeRedis GetRedis(string name);

        /// <summary>
        /// 删除redis实例
        /// </summary>
        /// <param name="name">实例名</param>
        /// <returns></returns>
        bool RemoveRedis(string name);
    }
}
