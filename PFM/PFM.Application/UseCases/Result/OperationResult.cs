using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFM.Application.UseCases.Resault
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public string? ErrorMessage { get; }

        internal OperationResult(bool success, T? value, string? error)
        {
            IsSuccess = success;
            Value = value;
            ErrorMessage = error;
        }


        public static OperationResult<T> Success(T value)
            => new(true, value, null);

        public static OperationResult<T> Fail(string errorMessage)
            => new(false, default, errorMessage);
    }

    public class OperationResult : OperationResult<Unit>
    {
        private OperationResult(bool success, Unit? value, string? error)
            : base(success, value: (Unit)value, error) { }

        public static OperationResult Success()
            => new(true, Unit.Value, null);

        public static OperationResult Fail(string errorMessage)
            => new(false, Unit.Value, errorMessage);
    }
}
