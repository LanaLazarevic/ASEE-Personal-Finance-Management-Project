using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PFM.Application.Result
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public IEnumerable<Error>? Error { get; }
        public int code { get; }

        internal OperationResult(bool success, T? value, int code, IEnumerable<Error>? erorrs)
        {
            IsSuccess = success;
            Value = value;
            this.code = code;
            Error = erorrs;
        }


        public static OperationResult<T> Success(T value, int code)
            => new(true, value, code, default);

        public static OperationResult<T> Fail( int code, IEnumerable<Error> error)
            => new(false, default, code, error);
    }

    public class OperationResult : OperationResult<Unit>
    {
        private OperationResult(bool success, Unit? value, int code, IEnumerable<Error>? errors)
            : base(success, (Unit)value, code, errors) { }

        public static OperationResult Success()
            => new(true, Unit.Value, 200, default);

        public static OperationResult Fail(int code, IEnumerable<Error> errors)
            => new(false, Unit.Value, code, errors);
    }
}
