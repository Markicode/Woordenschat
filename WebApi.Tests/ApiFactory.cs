using Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Respawn;
using MySqlConnector;

namespace WebApi.Tests
{
    public class ApiFactory : WebApplicationFactory<Program>
    {
        private static bool _databaseInitialized = false;

        public MySqlConnection Connection { get; private set; } = null!;
        private Respawner _respawner = null!;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");

            builder.ConfigureServices(services =>
            {
                var sp = services.BuildServiceProvider();
                var config = sp.GetRequiredService<IConfiguration>();

                Connection = new MySqlConnection(config.GetConnectionString("LibraryDb"));
                Connection.Open();

                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<LibraryContext>();

                if (!_databaseInitialized)
                {
                    db.Database.EnsureDeleted();
                    db.Database.Migrate();
                    _databaseInitialized = true;
                }

                _respawner = Respawner.CreateAsync(Connection, new RespawnerOptions
                {
                    DbAdapter = DbAdapter.MySql
                }).GetAwaiter().GetResult();
            });
        }

        public async Task ResetDatabaseAsync()
        {
            await _respawner.ResetAsync(Connection);
        }
    }
}
