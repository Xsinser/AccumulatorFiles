using AccumulatorFiles.Models;
using Microsoft.EntityFrameworkCore;

namespace AccumulatorFiles.Context
{
    public interface IContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<FileModel> File { get; set; } 
    }
}
