<h1>一、项目说明</h1>
<p>NewLife.Redis.Core基于新生命团队NewLife.Redis的封装,支持.NETCore3/.NET6/7。</p>
<p dir="auto"><span style="color: #e03e2d;">NewLife.Redis</span> 是一个Redis客户端组件，以高性能处理大数据实时计算为目标。</p>
<p dir="auto">源码：&nbsp;<a href="https://github.com/NewLifeX/NewLife.Redis">https://github.com/NewLifeX/NewLife.Redis</a><br />Nuget：NewLife.Redis<br />教程：<a href="https://newlifex.com/core/redis" rel="nofollow">https://newlifex.com/core/redis</a></p>
<h1>二、使用说明</h1>
<h2>2.1 通过New的方式安装使用</h2>
<h3>2.1.1 单客户端模式</h3>
<pre class="language-csharp highlighter-hljs"><code>using NewLife.Redis.Core;
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
//队列操作
//方式1
var queue = redis.GetRedisQueue&lt;string&gt;("queue");
queue.Add("test");
var data = queue.Take(1);
//方式2
redis.AddQueue("queue", "1");
redis.GetQueueOne&lt;string&gt;("queue");</code></pre>
<h2>2.2 通过IOC注入（推荐）</h2>
<h3>2.2.1 单客户端注入</h3>
<p>ConfigureServices里注册组件</p>
<pre class="language-csharp highlighter-hljs"><code>//默认读取配置文件：ConnectionStrings:Redis
services.AddNewLifeRedis();
//指定链接字符串
services.AddNewLifeRedis("server=127.0.0.1:6379;password=xxx;db=4");</code></pre>
<p>构造函数里注入INewLifeRedis</p>
<pre class="language-csharp highlighter-hljs"><code>private readonly INewLifeRedis newLifeRedis;
public Worker(ILogger&lt;Worker&gt; logger, INewLifeRedis newLifeRedis)
{
  _logger = logger;
  this.newLifeRedis = newLifeRedis;
  newLifeRedis.Set("test", "2");
}</code></pre>
<h3>2.2.2 多客户端注入</h3>
<p>配置文件不能再是字符串格式而要改成下面格式</p>
<pre class="language-csharp highlighter-hljs"><code>"ConnectionStrings": {
    "Redis": [
      {
        "Name": "1",
        "ConnectionString": "server=127.0.0.1:6379;password=123456;db=4"
      },
      {
        "Name": "2",
        "ConnectionString": "server=127.0.0.1:6379;password=123456;db=5"
      }
    ]
  },</code></pre>
<p>ConfigureServices里注册组件</p>
<pre class="language-csharp highlighter-hljs"><code>services.AddRedisCacheManager();
services.AddRedisCacheManager(hostContext.Configuration, "xxx");//第二种</code></pre>
<p>构造函数里注入IRedisCacheManager</p>
<pre class="language-csharp highlighter-hljs"><code>private readonly ISimpleRedis newLifeRedis;
public Worker(ILogger&lt;Worker&gt; logger, IRedisCacheManager redisCacheManager)
{
 _logger = logger;
newLifeRedis = redisCacheManager.GetRedis("1");
newLifeRedis.Set("TEST", "test");
newLifeRedis = redisCacheManager.GetRedis("2");
newLifeRedis.Set("TEST", "test");
}</code></pre>
<h1>三、实现消息队列</h1>
<p><span style="font-size: 24px;">详情可以看我的这篇文章：<a href="https://www.cnblogs.com/huguodong/p/16434717.html" target="_blank" rel="noopener">.Net大杀器之基于Newlife.Redis的可重复消费+共享订阅队列来替换第三方MQ</a></span></p>
<h1>四、源码地址</h1>
<p><span style="font-size: 24px;">Github:<a href="https://github.com/NewLifeX/NewLife.Redis.Core" target="_blank" rel="noopener">https://github.com/NewLifeX/NewLife.Redis.Core</a></span></p>
<p><span style="font-size: 24px;">Gitee:<a href="https://gitee.com/huguodong520/NewLife.Redis.Core.git" target="_blank" rel="noopener">https://gitee.com/huguodong520/NewLife.Redis.Core.git</a></span></p>