using NewLife.Caching;
using NewLife.Caching.Models;
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
    public interface IRedisCacheManager
    {
        /// <summary>
        /// 获取FullRedis实例
        /// </summary>
        /// <returns></returns>
        FullRedis GetFullRedis();

        /// <summary>
        /// 获取所有的Key
        /// </summary>
        /// <returns></returns>
        List<string> AllKeys();

        /// <summary>
        /// 清空所有缓存项
        /// </summary>
        void Clear();

        /// <summary>
        /// 是否包含某一个key
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        bool ContainsKey(string key);

        /// <summary>
        /// 根据Key模糊删除，从key头开始匹配
        /// </summary>
        /// <param name="pattern">部分键</param>
        /// <returns>删除数量</returns>
        long DelByPattern(string pattern);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        TEntity Get<TEntity>(string key);

        /// <summary>
        /// 根据Key移除数据
        /// </summary>
        /// <param name="key">键</param>
        void Remove(string key);

        /// <summary>
        /// 根据key删除全部数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="count">删除数量</param>
        /// <returns>删除数量</returns>
        int RemoveAllByKey(string key, int count = 999);

        /// <summary>
        /// 根据key删除数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="count">删除数量</param>
        /// <returns>删除数量</returns>
        int RemoveByKey(string key, int count);

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<string> Search(SearchModel model);

        /// <summary>
        /// 插入数据,设置过期时间
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="cacheTime">过期时间</param>
        /// <returns></returns>
        bool Set<TEntity>(string key, TEntity value, TimeSpan cacheTime);

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expire">过期时间</param>
        /// <returns></returns>
        bool Set<TEntity>(string key, TEntity value, int expire = -1);

        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="timeSpan">过期时间</param>
        void SetExpire(string key, TimeSpan timeSpan);

        /// <summary>
        /// 重命名
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="newKey">新键</param>
        /// <param name="overwrire">是否覆盖</param>
        /// <returns></returns>
        bool Rename(string key, string newKey, bool overwrire = true);
    }
}
