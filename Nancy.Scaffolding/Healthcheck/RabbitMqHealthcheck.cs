using RabbitMQ.Client;
using System;

namespace Nancy.Scaffolding.Healthcheck
{
    public class RabbitMqHealthcheck : IHealthcheck
    {
        public RabbitMqHealthcheck(string connString, int timeoutInMs = 2000, string name = "rabbitmq")
        {
            this.Name = name;
            this.ConnectionString = connString;
            this.TimeoutInMs = timeoutInMs;
        }

        public string Name { get; set; }

        private string ConnectionString { get; set; }

        private int TimeoutInMs { get; set; }

        public (bool result, string description) IsHealth()
        {
            try
            {
                ConnectionFactory factory = new ConnectionFactory();
                factory.Uri = new Uri(this.ConnectionString);
                factory.SocketReadTimeout = this.TimeoutInMs;
                factory.SocketWriteTimeout = this.TimeoutInMs;
                IConnection conn = null;

                conn = factory.CreateConnection();
                conn.Close();
                conn.Dispose();

                return (true, "connected");
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }
    }
}
