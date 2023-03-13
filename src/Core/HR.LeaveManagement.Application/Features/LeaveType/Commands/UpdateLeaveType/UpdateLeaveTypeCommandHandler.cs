using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        readonly ILeaveTypeRepository _repository;
        readonly IMapper _mapper;
        readonly IAppLogger<UpdateLeaveTypeCommandHandler> _logger;

        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository repository,
                                             IMapper mapper,
                                             IAppLogger<UpdateLeaveTypeCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // Validate incoming data
            var validator = new UpdateLeaveTypeCommandValidator(_repository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
            {
                _logger.LogWarning("Validation errors in update request for {0} - {1}",
                                   nameof(LeaveType),
                                   request.Id);
                throw new BadRequestException("Invalid Leave type", validationResult);
            }

            // Convert to domain entity object
            var leaveTypeTpUpdate = _mapper.Map<Domain.LeaveType>(request);

            // Add to database
            await _repository.UpdateAsync(leaveTypeTpUpdate);

            // Return Unit value
            return Unit.Value;
        }
    }
}