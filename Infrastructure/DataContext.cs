using Application.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TimeKeeping> TimeKeep { get; set; }
        public DbSet<Punch> Punches { get; set; }

        public Task AddEntityToGraph<T>(T entity)
        {
            this.ChangeTracker.TrackGraph(entity, e => e.Entry.State = e.Entry.IsKeySet ? EntityState.Modified : EntityState.Added);
            return Task.CompletedTask;
        }

        #region SAVING SUPPORT

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            //foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            //{
            //    switch (entry.State)
            //    {
            //        case EntityState.Added:
            //            entry.Entity.Created = DateTime.Now;
            //            entry.Entity.Active = true;
            //            break;
            //        case EntityState.Modified:
            //            entry.Property("CreatedBy").IsModified = false;
            //            entry.Property("Created").IsModified = false;
            //            entry.Property("Active").CurrentValue = entry.Property("Active").CurrentValue ?? true;
            //            entry.Entity.LastModified = DateTime.Now;
            //            break;
            //    }
            //}

            return base.SaveChangesAsync(cancellationToken);
        }

        #endregion SAVING SUPPORT
    }
}