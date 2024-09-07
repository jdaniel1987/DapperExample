﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace DapperExample.Infrastructure.Data.DatabaseSeed;

public static class DatabaseCreation
{
    public static void CreateDatabase(IConfiguration configuration)
    {
        var databaseName = "DapperExample";
        var connectionString = configuration.GetConnectionString("DbCreation");
        var scriptsDir = "../DapperExample.Infrastructure/Data/DatabaseSeed/Scripts";

        using var sqlConnection = new SqlConnection(connectionString);
        var serverConnection = new ServerConnection(sqlConnection);
        var server = new Server(serverConnection);

        if(server.Databases.Contains(databaseName))
        {
            server.KillDatabase(databaseName);
        }

        var database = new Database(server, databaseName);
        database.Create();

        connectionString = configuration.GetConnectionString("DbTablesCreation");
        foreach(var scriptFile in Directory.GetFiles(scriptsDir, "*.sql", SearchOption.AllDirectories))
        {
            var script = File.ReadAllText(scriptFile);
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using var command = new SqlCommand(script, connection);
            command.ExecuteNonQuery();
        }
    }
}
