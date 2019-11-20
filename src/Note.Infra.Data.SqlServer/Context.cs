using Microsoft.EntityFrameworkCore;
using Note.Core.Entities;
using Note.Infra.Data.SqlServer.Specs;

namespace Note.Infra.Data.SQLServer
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Page> Pages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies(); // Needs Microsoft.EntityFrameworkCore.Proxies nuget package.
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Specs
            new UserSpec(modelBuilder.Entity<User>());
            new BookSpec(modelBuilder.Entity<Book>());
            new PageSpec(modelBuilder.Entity<Page>());
        }
    }
}
