using StackExchange.Redis;
using System;
namespace subscriber {
    class Program {
        static void Main(String[] args){
            Console.WriteLine("Hello world");

            // Configuración de Redis
            string redisHost = Environment.GetEnvironmentVariable("REDIS_HOST")!;
            int redisPort = int.Parse(Environment.GetEnvironmentVariable("REDIS_PORT")!);  // Cambia al puerto correspondiente
            string redisPassword = Environment.GetEnvironmentVariable("REDIS_PASSWORD")!;
            string channel = "testChannel";
            DateTime fechaYhora = DateTime.Now;

            ConfigurationOptions options = new ConfigurationOptions{
                EndPoints = {$"{redisHost}:{redisPort}"},
                Password = redisPassword,
                AllowAdmin = true
            };

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(options);
            ISubscriber sub = redis.GetSubscriber();



            sub.Subscribe(channel, (channel, message) => {
                Console.WriteLine($"Recibido: {message} \n Recibido: {fechaYhora}");
            });

            Console.WriteLine("Esperando mensajes: ");
            Console.ReadLine();
        }
    }
}