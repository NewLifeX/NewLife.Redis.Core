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
    public partial interface IRedisCacheManager
    {
        /// <summary>
        /// 获取HashMap实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <returns>HashMap实例</returns>
        RedisHash<string, T> GetHashMap<T>(string key);

        /// <summary>
        /// 添加一条数据到HashMap
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="hashKey">hash列表里的Key</param>
        /// <param name="value">值</param>
        void HashAdd<T>(string key, string hashKey, T value);

        /// <summary>
        /// 添加多条数据到HashMap
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="dic">键值对字典</param>
        /// <returns></returns>
        bool HashSet<T>(string key, Dictionary<string, T> dic);

        /// <summary>
        /// 从HashMap中删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="fields">键列表</param>
        /// <returns>执行结果</returns>
        int HashDel<T>(string key, params string[] fields);

        /// <summary>
        /// 根据键获取hash列表中的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="fields">键列表</param>
        /// <returns>数据列表</returns>
        List<T> HashGet<T>(string key, params string[] fields);

        /// <summary>
        /// 获取所有键值对
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <returns>数据字典</returns>
        IDictionary<string, T> HashGetAll<T>(string key);

        /// <summary>
        /// 搜索
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="searchModel"></param>
        /// <returns>查询结果</returns>
        List<KeyValuePair<string, T>> HashSearch<T>(string key, SearchModel searchModel);

        /// <summary>
        /// 根据键搜索
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="pattern">hash键</param>
        /// <param name="count">返回数量</param>
        /// <returns>查询结果</returns>
        List<KeyValuePair<string, T>> HashSearch<T>(string key, string pattern, int count);


    }
}
