using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using EAccount = Domain.Entities.Account;
using EProject = Domain.Entities.Project;

namespace Application.Common
{
    public interface IDataContext
    {
        public DbSet<EAccount> Accounts { get; set; }
        public DbSet<EProject> Projects { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TimeKeeping> TimeKeep { get; set; }
        public DbSet<Punch> Punches { get; set; }

        Task AddEntityToGraph<T>(T entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}