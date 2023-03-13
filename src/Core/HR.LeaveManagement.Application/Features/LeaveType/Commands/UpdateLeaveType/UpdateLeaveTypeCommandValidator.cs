using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
    {
        readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(x => x.Id)
                .NotNull()
                .MustAsync(LeaveTypeMustExist);

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");

            RuleFor(x => x.DefaultDays)
                .GreaterThan(100).WithMessage("{PropertyName} cannot exceed 100")
                .LessThan(1).WithMessage("{PropertyName} cannot be less than 1");

            RuleFor(x => x)
                .MustAsync(LeaveTypeNameUnique)
                .WithMessage("Leave type already exists");
        }

        private async Task<bool> LeaveTypeMustExist(int id,
                                                    CancellationToken arg2)
            => await _leaveTypeRepository.GetByIdAsync(id) is not null;

        private async Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand command,
                                                     CancellationToken token)
            => await _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
    }
}