using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public record UpdateLeaveTypeCommand : IRequest<Unit>
    {
        public string Name { get; set; } = string.Empty;
        public int DefaultDays { get; set; }
    }
}