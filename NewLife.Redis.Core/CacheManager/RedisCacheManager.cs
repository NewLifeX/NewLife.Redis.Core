using Microsoft.Extensions.Configuration;

namespace NewLife.Redis.Core
{
    /// <summary>
    /// <inheritdoc cref="IRedisCacheManager"/>
    /// </summary>
    public class RedisCacheManager : IRedisCacheManager
    {
        /// <summary>
        /// redis连接字典
        /// </summary>
        public Dictionary<string, NewLifeRedis> RedisConnections = new Dictionary<string, NewLifeRedis>();

        /// <summary>
        /// 配置文件注入
        /// </summary>
        /// <param name="configuration"></param>
        /// <exception cref="ArgumentException"></exception>
        public RedisCacheManager(IConfiguration configuration)
        {
            List<RedisConfig> configs = configuration.GetSection("ConnectionStrings:Redis").Get<List<RedisConfig>>();//获取配置
            if (configs == null) throw new ArgumentNullException("Redis配置文件未找到", nameof(configs));
            AddRedisConnections(configs);
        }

        /// <summary>
        /// 通过字符串注入
        /// </summary>
        /// <param name="configs">配置文件列表</param>
        /// <exception cref="ArgumentException"></exception>
        public RedisCacheManager(List<RedisConfig> configs)
        {
            AddRedisConnections(configs);
        }


        /// <inheritdoc />
        public NewLifeRedis GetRedis(string name)
        {
            if (!RedisConnections.ContainsKey(name))
                throw new ArgumentException($"Name为{name}的连接不存在", nameof(RedisConfig));
            return RedisConnections[name];
        }

        /// <inheritdoc />
        public void AddRedis(RedisConfig config)
        {
            AddRedisConnection(config);
        }

        /// <inheritdoc />
        public bool RemoveRedis(string name)
        {
            return RedisConnections.Remove(name);
        }


        /// <summary>
        /// 添加redis连接到列表
        /// </summary>
        /// <param name="configs">配置文件列表</param>
        private void AddRedisConnections(List<RedisConfig> configs)
        {
            configs.ForEach(it =>
            {
                AddRedisConnection(it);
            });
        }

        /// <summary>
        /// 添加redis连接到列表
        /// </summary>
        /// <param name="configs"></param>
        /// <exception cref="ArgumentException"></exception>
        private void AddRedisConnection(RedisConfig config)
        {

            if (string.IsNullOrEmpty(config.Name))
                throw new ArgumentException($"Name不能为空", nameof(RedisConfig));
            if (RedisConnections.ContainsKey(config.Name))
                throw new ArgumentException($"Name为{config.Name}的连接已存在", nameof(RedisConfig));
            var fullRedis = new NewLifeRedis(config.ConnectionString);
            RedisConnections.Add(config.Name, fullRedis);
        }


    }
}
