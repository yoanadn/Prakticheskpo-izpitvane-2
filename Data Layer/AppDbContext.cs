using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Layer;
using Microsoft.EntityFrameworkCore;

namespace Data_Layer
{
    public class AppDbContext : DbContext
    {
        public static AppDbContext dbContext;
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public AppDbContext(DbContextOptions options)
            : base(options) { }
        public AppDbContext() : base() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Server=127.0.0.1;Database=GamesDb;Uid=root;Pwd=root");
                base.OnConfiguring(optionsBuilder);
            }
        }
    }
}
