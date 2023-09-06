using Newtonsoft.Json;
using StackExchange.Redis;

namespace Cache
{
    public class Redis
    {
        private static Redis instance;
        private readonly IDatabase database;

        private Redis(int connectionProfile)
        {
            ConfigurationOptions configurationOptions = new();

            if (connectionProfile == 0)
            {
                configurationOptions = new ConfigurationOptions
                {
                    SyncTimeout = 120000,
                    ConnectTimeout = 5000,
                    AbortOnConnectFail = false,
                    EndPoints =
                    {
                        {"redis", 6379}
                    }
                };
            }

            database = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configurationOptions)).Value.GetDatabase();
        }

        public static Redis TakeInstance()
        {
            instance = new Redis(0);

            return instance;
        }

        public void InsertCache(string key, object value, TimeSpan? expiration, When replaceValue = When.Always)
        {
            if (value == null)
            {
                return;
            }

            if (value.GetType() == typeof(string))
            {
                database.StringSet(key, (string)value, expiration, replaceValue);
            }
            else if (value.GetType() == typeof(int))
            {
                database.StringSet(key, (int)value, expiration, replaceValue);
            }
            else if (value.GetType() == typeof(bool))
            {
                database.StringSet(key, (bool)value, expiration, replaceValue);
            }
            else
            {
                string serializedValue = JsonConvert.SerializeObject(value, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Include,
                    Formatting = Formatting.None
                });

                database.StringSet(key, serializedValue, expiration, replaceValue);
            }
        }

        public async Task InsertCacheAsync(string key, object value, TimeSpan expiration, When replaceValue = When.Always)
        {
            if (value == null)
            {
                return;
            }

            if (value.GetType() == typeof(string))
            {
                await database.StringSetAsync(key, (string)value, expiration, replaceValue);
            }
            else if (value.GetType() == typeof(int))
            {
                await database.StringSetAsync(key, (int)value, expiration, replaceValue);
            }
            else if (value.GetType() == typeof(bool))
            {
                await database.StringSetAsync(key, (bool)value, expiration, replaceValue);
            }
            else
            {
                string serializedValue = JsonConvert.SerializeObject(value, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Include,
                    Formatting = Formatting.None
                });

                await database.StringSetAsync(key, serializedValue, expiration, replaceValue);
            }
        }

        /// <exception cref=""></exception>
        public string? ReadCache(string key)
        {
            try
            {
                RedisValue query = database.StringGet(key);

                if (query.HasValue == false)
                {
                    return string.Empty;
                }

                return query;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <exception cref=""></exception>
        public async Task<string?> ReadCacheAsync(string key)
        {
            try
            {
                RedisValue query = await database.StringGetAsync(key);

                if (query.HasValue == false)
                {
                    return string.Empty;
                }

                return query;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public void RemoveCache(string key)
        {
            database.KeyDelete(key);
        }

        public async Task RemoveCacheAsync(string key)
        {
            await database.KeyDeleteAsync(key);
        }
    }
}