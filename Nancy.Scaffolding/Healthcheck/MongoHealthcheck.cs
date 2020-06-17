using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace Nancy.Scaffolding.Healthcheck
{
    public class MongoHealthcheck : IHealthcheck
    {
        public MongoHealthcheck(string connString, string database, int timeoutInMs = 5000, string name = "mongodb")
        {
            this.Name = name;
            this.ConnectionString = connString;
            this.Database = database;
            this.TimeoutInMs = timeoutInMs;
        }

        public string Name { get; set; }

        private string ConnectionString { get; set; }

        private string Database { get; set; }

        private int TimeoutInMs { get; set; }

        public (bool result, string description) IsHealth()
        {
            try
            {
                var client = new MongoClient(this.ConnectionString);
                var database = client.GetDatabase(this.Database);

                bool isMongoLive = database.RunCommandAsync((Command<BsonDocument>)"{ping:1}")
                        .Wait(this.TimeoutInMs);

                return (isMongoLive, "ping:1");
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }
    }
}
