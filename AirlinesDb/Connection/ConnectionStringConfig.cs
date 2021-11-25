using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AirlinesDb.Connection
{
    public class ConnectionStringConfig
    {
        public string ConnectionString { get; set; }

        public ConnectionStringConfig(string connectionStringName = "SaConnection",
            string environmentalVariableConnectionString = "AirlinesDb_ConnectionString") 
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>()
                .Build();

            var server = "";
            var database = "";
            var userId = "";
            var password = "";

            config.Providers.Any(p => p.TryGet("AirlinesDb:Server", out server));
            config.Providers.Any(p => p.TryGet("AirlinesDb:Database", out database));
            config.Providers.Any(p => p.TryGet("AirlinesDb:UserId", out userId));
            config.Providers.Any(p => p.TryGet("AirlinesDb:Password", out password));

            ConnectionString = string.Format(config.GetConnectionString(connectionStringName)
                ?? Environment.GetEnvironmentVariable(environmentalVariableConnectionString),
                server,
                database,
                userId,
                password);
        }
    }
}
