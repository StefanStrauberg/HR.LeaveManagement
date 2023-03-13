using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public class GetLeaveTypeQueryHandler : IRequestHandler<GetLeaveTypeQuery, List<LeaveTypeDto>>
    {
        readonly ILeaveTypeRepository _repository;
        readonly IMapper _mapper;
        readonly IAppLogger<GetLeaveTypeQueryHandler> _logger;

        public GetLeaveTypeQueryHandler(ILeaveTypeRepository repository,
                                        IMapper mapper,
                                        IAppLogger<GetLeaveTypeQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypeQuery request, CancellationToken cancellationToken)
        {
            // Query the database
            var leaveTypes = await _repository.GetAsync();

            // Convert data objects to DTO objects
            var data = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

            // return list of DTO object
            _logger.LogInformation("Leave types were retrived successfully");
            return data;
        }
    }
}