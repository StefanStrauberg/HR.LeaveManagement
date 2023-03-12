using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.DatabaseContext
{
    public class HrDatabaseContext : DbContext
    {
        public HrDatabaseContext(DbContextOptions<HrDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entity in base.ChangeTracker.Entries<BaseEntity>()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified))
            {
                entity.Entity.DateModified = DateTime.Now;

                if (entity.State == EntityState.Added)
                    entity.Entity.DateCreated = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}