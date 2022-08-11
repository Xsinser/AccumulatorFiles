using AccumulatorFiles.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace AccumulatorFiles.Context
{
    public class SQLiteContext : DbContext, IContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<FileModel> File{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=D:\AccumulatorFiles\AccumulatorFiles\AccumulatorFiles\AFDb.db;");
        }
    }
}
