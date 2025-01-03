using FlatFinder.Application.Abstractions.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FlatFinder.Infrastructure.Implementations.Data
{
    internal sealed class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
