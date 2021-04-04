using Microsoft.EntityFrameworkCore;
using Database.Link;
using Database.User;

namespace Database {
    public sealed class DatabaseContext : DbContext {
        public DatabaseContext(DbContextOptions options) : base(options) {
            Database.EnsureCreated();
        }

        public DatabaseContext() { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<LinkModel> Links { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new LinkConfiguration());
        }
    }
}