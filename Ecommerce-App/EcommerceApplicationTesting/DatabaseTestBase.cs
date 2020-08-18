using Ecommerce_App.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApplicationTesting
{
    public abstract class DatabaseTestBase : IDisposable
    {

        private readonly SqliteConnection _connection;
        protected readonly StoreDbContext _db;

        public DatabaseTestBase()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new StoreDbContext(
                  new DbContextOptionsBuilder<StoreDbContext>()
                  .UseSqlite(_connection)
                  .Options);

            _db.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }
    }
}
