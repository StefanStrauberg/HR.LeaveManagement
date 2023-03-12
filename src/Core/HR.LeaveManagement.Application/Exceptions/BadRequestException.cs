using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace HR.LeaveManagement.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        readonly List<string> _validationErrors;

        public BadRequestException(string message)
            : base(message)
        {
        }

        public BadRequestException(string message, ValidationResult validationResult)
            : base(message)
        {
            _validationErrors = new();

            foreach (var error in validationResult.Errors)
                _validationErrors.Add(error.ErrorMessage);
        }

        public List<string> ValidationErrors { get => _validationErrors; }
    }
}