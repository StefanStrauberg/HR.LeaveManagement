using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        readonly ILeaveTypeRepository _repository;
        readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveTypeTpUpdate = _mapper.Map<Domain.LeaveType>(request);

            await _repository.UpdateAsync(leaveTypeTpUpdate);

            return Unit.Value;
        }
    }
}