using System.Collections.Generic;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public record GetLeaveTypeQuery : IRequest<List<LeaveTypeDto>>;
}