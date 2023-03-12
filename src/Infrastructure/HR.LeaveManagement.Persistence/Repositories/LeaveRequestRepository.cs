using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        public LeaveRequestRepository(HrDatabaseContext context)
            : base(context)
        {
        }

        public async Task<IReadOnlyList<LeaveRequest>> GetLeaveRequestsWithDetails()
            => await _context.LeaveRequests
                             .AsNoTracking()
                             .Include(x => x.LeaveType)
                             .ToListAsync();

        public async Task<IReadOnlyList<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
            => await _context.LeaveRequests
                             .AsNoTracking()
                             .Where(x => x.RequestingEmployeeId == userId)
                             .Include(x => x.LeaveType)
                             .ToListAsync();

        public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
            => await _context.LeaveRequests
                             .AsNoTracking()
                             .Include(x => x.LeaveType)
                             .FirstOrDefaultAsync(x => x.Id == id);
    }
}