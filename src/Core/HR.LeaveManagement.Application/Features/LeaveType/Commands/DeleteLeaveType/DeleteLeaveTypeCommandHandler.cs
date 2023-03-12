using System.Threading;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {
        readonly ILeaveTypeRepository _repository;

        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository repository)
            => _repository = repository;

        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveTypeToDelete = await _repository.GetByIdAsync(request.Id);

            if (leaveTypeToDelete is null)
                throw new NotFoundException(nameof(LeaveType), request.Id);

            await _repository.DeleteAsync(leaveTypeToDelete);

            return Unit.Value;
        }
    }
}