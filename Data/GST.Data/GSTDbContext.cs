namespace GST.Data
{
    using Common.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
    using Models;
    using System;
    using System.Linq;

    public class GSTDbContext : IdentityDbContext<User>
    {
        private DbContextOptions options;

        public GSTDbContext(DbContextOptions<GSTDbContext> options)
            : base(options)
        {
            this.options = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var sqlServerOptions = options.GetExtension<SqlServerOptionsExtension>();
            optionsBuilder.UseSqlServer(sqlServerOptions.ConnectionString, b => b.MigrationsAssembly("GST.Data"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            SetupIndexesForTables(builder);
        }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Video> Videos { get; set; }

        public DbSet<Page> Pages { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Log> Logs { get; set; }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletableEntityRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            foreach (var entry in 
                this.ChangeTracker.Entries()
                .Where( 
                    e => e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void ApplyDeletableEntityRules()
        {
            foreach (
                var entry in
                    this.ChangeTracker.Entries()
                        .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Deleted)))
            {
                var entity = (IDeletableEntity)entry.Entity;

                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted= true;
                entry.State = EntityState.Modified;
            }
        }

        private void SetupIndexesForTables(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(b => b.IsDeleted);

            builder.Entity<Picture>()
                .HasIndex(b => b.IsDeleted);

            builder.Entity<Video>()
                .HasIndex(b => b.IsDeleted);

            builder.Entity<Page>()
                .HasIndex(b => b.IsDeleted);

            builder.Entity<Post>()
                .HasIndex(b => b.IsDeleted);
        }
    }
}
