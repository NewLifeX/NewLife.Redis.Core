using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

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
        public static void AddRedisCacheManager(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddSingleton<IRedisCacheManager, NewLifeRedis>();

        }

        /// <summary>
        /// 添加redis服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="redisConfiguration">Redis链接字符串</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddRedisCacheManager(this IServiceCollection services, string redisConfiguration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddSingleton<IRedisCacheManager, NewLifeRedis>(x => new NewLifeRedis(redisConfiguration));

        }
    }
}
