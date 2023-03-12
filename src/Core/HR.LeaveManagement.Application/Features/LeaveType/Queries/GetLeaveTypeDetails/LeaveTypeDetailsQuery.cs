using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
    public record LeaveTypeDetailsQuery(int Id) : IRequest<LeaveTypeDetailDto>;
}