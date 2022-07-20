# NewLife.Redis.Core

<h1>一、项目说明</h1>
<p>NewLife.Redis.Core基于新生命团队NewLife.Redis的封装,支持.NETCore3/.NET5/.NET6。</p>
<p dir="auto"><span style="color: #e03e2d;">NewLife.Redis</span> 是一个Redis客户端组件，以高性能处理大数据实时计算为目标。</p>
<p dir="auto">源码：&nbsp;<a href="https://github.com/NewLifeX/NewLife.Redis">https://github.com/NewLifeX/NewLife.Redis</a><br />Nuget：NewLife.Redis<br />教程：<a href="https://newlifex.com/core/redis" rel="nofollow">https://newlifex.com/core/redis</a></p>
<h1>二、使用说明</h1>
<h2>2.1 通过New的方式安装使用</h2>

```csharp
using NewLife.Redis.Core;


NewLifeRedis redis = new NewLifeRedis("server=127.0.0.1:6379;password=Shiny123456;db=4");

//普通操作
redis.Set("test", "1");
Console.WriteLine(redis.Get&lt;string&gt;("test"));

//列表
redis.ListAdd("listtest", 1);
redis.ListGetAll&lt;string&gt;("listtest");

//SortedSet
redis.SortedSetAdd("sortsettest", "1", 1.0);
redis.SortedSetIncrement("sortsettest", "1", 1.0);


//set
redis.SetAdd("settest", "2");

//哈希
redis.HashAdd("hashtest", "1", "2");
redis.HashGet&lt;string&gt;("hashtest", new string[] { "1" });
```

<h2>2.2 通过IOC注入</h2>
<p>ConfigureServices里注册组件</p>

```csharp
services.AddRedisCacheManager("server=127.0.0.1:6379;password=xxxx;db=4");
```

<p>构造函数里注入IRedisCacheManager</p>

```csharp
        private readonly ILogger<Worker> _logger;
        private readonly IRedisCacheManager _redisCacheManager;

        public Worker(ILogger<Worker> logger, IRedisCacheManager redisCacheManager)
        {
            _logger = logger;
            this._redisCacheManager = redisCacheManager;

            var data = _redisCacheManager.Get<string>("test");
        }
```

