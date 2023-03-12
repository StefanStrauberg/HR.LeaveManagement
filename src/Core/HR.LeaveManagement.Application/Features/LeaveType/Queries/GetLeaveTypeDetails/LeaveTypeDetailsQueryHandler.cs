using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
    public class LeaveTypeDetailsQueryHandler : IRequestHandler<LeaveTypeDetailsQuery, LeaveTypeDetailDto>
    {
        readonly ILeaveTypeRepository _repository;
        readonly IMapper _mapper;

        public LeaveTypeDetailsQueryHandler(ILeaveTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<LeaveTypeDetailDto> Handle(LeaveTypeDetailsQuery request, CancellationToken cancellationToken)
        {
            var leaveType = await _repository.GetByIdAsync(request.Id);

            if (leaveType is null)
                throw new NotFoundException(nameof(LeaveType), request.Id);

            var data = _mapper.Map<LeaveTypeDetailDto>(leaveType);

            return data;
        }
    }
}