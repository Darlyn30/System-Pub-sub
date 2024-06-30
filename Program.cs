using StackExchange.Redis;
using System;
namespace realTimeData
{
    class Program
        //publisher
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world");

            

            // Configuración de Redis
            string redisHost = Environment.GetEnvironmentVariable("REDIS_HOST")!;
            int redisPort = int.Parse(Environment.GetEnvironmentVariable("REDIS_PORT")!); 
            string redisPassword = Environment.GetEnvironmentVariable("REDIS_PASSWORD")!;
            string channel = "testChannel";

            ConfigurationOptions options = new ConfigurationOptions
            {
                EndPoints = { $"{redisHost}:{redisPort}" },
                Password = redisPassword,
                AllowAdmin = true
            };

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(options);
            IDatabase db = redis.GetDatabase();
            ISubscriber sub = redis.GetSubscriber();

            while (true)
            {
                Console.Write("Introduce un mensaje para publicar: ");
                string message = Console.ReadLine()!;
                sub.Publish(channel, message);
                Console.WriteLine($"Publicado: {message}\n");
            }
        }
    }
}