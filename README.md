# NewLife.Redis.Core

<h1>一、项目说明</h1>
<p>NewLife.Redis.Core基于新生命团队NewLife.Redis的封装,支持.NETCore3/.NET5/.NET6/.NET7。</p>
<p dir="auto"><span style="color: #e03e2d;">NewLife.Redis</span> 是一个Redis客户端组件，以高性能处理大数据实时计算为目标。</p>
<p dir="auto">源码：&nbsp;<a href="https://github.com/NewLifeX/NewLife.Redis">https://github.com/NewLifeX/NewLife.Redis</a><br />Nuget：NewLife.Redis<br />教程：<a href="https://newlifex.com/core/redis" rel="nofollow">https://newlifex.com/core/redis</a></p>
<h1>二、使用说明</h1>
<h2>2.1 通过New的方式安装使用</h2>

```csharp
using NewLife.Redis.Core;

NewLifeRedis redis = new NewLifeRedis("server=127.0.0.1:6379;password=xxx;db=4");

//普通操作
redis.Set("test", "1");
Console.WriteLine(redis.Get<string>("test"));

//列表
redis.ListAdd("listtest", 1);
redis.ListGetAll<string>("listtest");

//SortedSet
redis.SortedSetAdd("sortsettest", "1", 1.0);
redis.SortedSetIncrement("sortsettest", "1", 1.0);


//set
redis.SetAdd("settest", "2");

//哈希
redis.HashAdd("hashtest", "1", "2");
redis.HashGet<string>("hashtest", new string[] { "1" });


//队列操作
//方式1
var queue = redis.GetRedisQueue<string>("queue");
queue.Add("test");
var data = queue.Take(1);
//方式2
redis.AddQueue("queue", "1");
redis.GetQueueOne<string>("queue");
```

<h2>2.2 通过IOC注入</h2>
<p>ConfigureServices里注册组件</p>

```csharp
        //默认读取配置文件：ConnectionStrings:Redis
        services.AddRedisCacheManager();

        //指定链接字符串
        services.AddRedisCacheManager("server=127.0.0.1:6379;password=xxx;db=4");
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

