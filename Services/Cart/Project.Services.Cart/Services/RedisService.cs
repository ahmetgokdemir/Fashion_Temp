using StackExchange.Redis;

namespace Project.Services.Cart.Services
{
    public class RedisService
    {
        private readonly string _host;

        private readonly short _port;

        private ConnectionMultiplexer _ConnectionMultiplexer; // StackExchange

        public RedisService(string host, short port)
        {
            _host = host;
            _port = port;
        }

        public void Connect() => _ConnectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");
        public IDatabase GetDb(int db = 1) => _ConnectionMultiplexer.GetDatabase(db);

        // int asd (int a, int b) => a + b;

    }
}
