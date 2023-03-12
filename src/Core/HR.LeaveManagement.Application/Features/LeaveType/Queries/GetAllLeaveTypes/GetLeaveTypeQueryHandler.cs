using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public class GetLeaveTypeQueryHandler : IRequestHandler<GetLeaveTypeQuery, List<LeaveTypeDto>>
    {
        readonly ILeaveTypeRepository _repository;
        readonly IMapper _mapper;

        public GetLeaveTypeQueryHandler(ILeaveTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypeQuery request, CancellationToken cancellationToken)
        {
            var leaveTypes = await _repository.GetAsync();

            var data = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

            return data;
        }
    }
}