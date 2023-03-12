using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(HrDatabaseContext context)
            : base(context)
        {
        }

        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
            await _context.LeaveAllocations.AddRangeAsync(allocations);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
            => await _context.LeaveAllocations
                             .AsNoTracking()
                             .AllAsync(x => x.EmployeeId == userId
                             && x.LeaveTypeId == leaveTypeId
                             && x.Period == period);

        public async Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetails()
            => await _context.LeaveAllocations
                             .AsNoTracking()
                             .Include(x => x.LeaveType)
                             .ToListAsync();

        public async Task<IReadOnlyList<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
            => await _context.LeaveAllocations
                             .AsNoTracking()
                             .Where(x => x.EmployeeId == userId)
                             .Include(x => x.LeaveType)
                             .ToListAsync();

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
            => await _context.LeaveAllocations
                             .AsNoTracking()
                             .Include(x => x.LeaveType)
                             .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
            => await _context.LeaveAllocations
                             .AsNoTracking()
                             .FirstOrDefaultAsync(x => x.EmployeeId == userId
                             && x.LeaveTypeId == leaveTypeId);
    }
}