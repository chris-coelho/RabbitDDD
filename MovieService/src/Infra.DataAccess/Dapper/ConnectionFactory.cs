using System.Data;
using Npgsql;

namespace Infra.DataAccess.Dapper;

public static class ConnectionFactory
{
    private static string _connectionString;
        
    public static void InitPostgres(string connectionString)
    {
        _connectionString = connectionString;
    }

    public static async Task<IDbConnection> GetPostgresConnectionAsync()
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        return connection;
    }
}