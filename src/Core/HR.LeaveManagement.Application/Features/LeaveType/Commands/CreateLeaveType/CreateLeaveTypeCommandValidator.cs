using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
    {
        readonly ILeaveTypeRepository _repository;
        public CreateLeaveTypeCommandValidator(ILeaveTypeRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 charachters");

            RuleFor(x => x.DefaultDays)
                .LessThan(100).WithMessage("{PropertyName} cannot exceed 100")
                .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1");

            RuleFor(x => x)
                .MustAsync(LeaveTypeNameUnique)
                .WithMessage("Leave type already exists");
        }

        private async Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken token)
            => await _repository.IsLeaveTypeUnique(command.Name);
    }
}