using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NewLife.Redis.Core
{
    /// <summary>
    /// redis服务拓展类
    /// </summary>
    public static class RedisSetUp
    {
        /// <summary>
        /// 添加redis服务
        /// </summary>
        /// <param name="services"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddNewLifeRedis(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddSingleton<INewLifeRedis, NewLifeRedis>();

        }

        /// <summary>
        /// 添加redis服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="redisConfiguration">Redis链接字符串</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddNewLifeRedis(this IServiceCollection services, string redisConfiguration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddSingleton<INewLifeRedis, NewLifeRedis>(x => new NewLifeRedis(redisConfiguration));

        }

        /// <summary>
        /// 添加Redis缓存中心服务
        /// </summary>
        /// <param name="services"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddRedisCacheManager(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSingleton<IRedisCacheManager, RedisCacheManager>();

        }


        /// <summary>
        /// 添加Redis缓存中心服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">IConfiguration</param>
        /// <param name="section">配置文件节点</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddRedisCacheManager(this IServiceCollection services, IConfiguration configuration, string section = "ConnectionStrings:Redis")
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            List<RedisConfig> configs = configuration.GetSection(section).Get<List<RedisConfig>>();//获取配置
            if (configs == null) throw new ArgumentNullException("Redis配置文件未找到", nameof(configs));
            services.AddSingleton<IRedisCacheManager, RedisCacheManager>(x => new RedisCacheManager(configs));

        }


        /// <summary>
        /// 添加Redis缓存中心服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configs">配置列表</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddRedisCacheManager(this IServiceCollection services, List<RedisConfig> configs)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configs == null) throw new ArgumentNullException(nameof(configs));
            services.AddSingleton<IRedisCacheManager, RedisCacheManager>(x => new RedisCacheManager(configs));
        }
    }
}
