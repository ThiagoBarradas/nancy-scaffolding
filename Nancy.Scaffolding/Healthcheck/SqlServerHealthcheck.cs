using Dapper;
using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace Nancy.Scaffolding.Healthcheck
{
    public class SqlServerHealthcheck : IHealthcheck
    {
        public SqlServerHealthcheck(string connString, string query = "select 1;", int timeoutInSeconds = 5, string name = "sqlserver")
        {
            this.Name = name;
            this.ConnectionString = connString;
            this.Query = query;
            this.TimeoutInSeconds = timeoutInSeconds;
        }

        public string Name { get; set; }

        private string ConnectionString { get; set; }

        private string Query { get; set; }

        private int TimeoutInSeconds { get; set; }

        public (bool result, string description) IsHealth()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(this.ConnectionString);
                builder.ConnectTimeout = this.TimeoutInSeconds;

                using (var connection = new SqlConnection(builder.ToString()))
                {
                    connection.Open();

                    var result = connection.QueryFirst(@"
                        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;"
                        + this.Query,
                        commandTimeout: this.TimeoutInSeconds);

                    return ((result != null), "execute query");
                }
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }
    }
}
