using System;
using System.Collections.Generic;
using System.Linq;

namespace Gamepad.Service.Models.ResultModels
{
    public class OperationResult
    {
        protected OperationResult(params string[] errors)
        {
            Succeeded = false;
            Errors = errors;
        }

        protected OperationResult(IEnumerable<string> errors)
        {
            Succeeded = false;
            Errors = errors.ToArray();
        }

        protected OperationResult(IEnumerable<string> errors, Exception exception)
        {
            Succeeded = false;
            Errors = errors.ToArray();
            Exception = exception;
        }

        protected OperationResult(bool succeeded)
        {
            Succeeded = succeeded;
            Errors = new string[] { };
        }


        public bool Succeeded { get; }
        public string[] Errors { get; }
        public Exception Exception { get; }
        public string LastError => Errors.FirstOrDefault();


        public static OperationResult Success => new OperationResult(true);
        public static OperationResult Failed(params string[] errors) => new OperationResult(errors);
        public static OperationResult Failed(Exception exception, params string[] errors) => new OperationResult(errors, exception);

        //public OperationResult Clone() => Succeeded ? new OperationResult(true) : new OperationResult(Errors);
    }

    public class OperationResult<T>
    {
        public OperationResult(params string[] errors)
        {
            Succeeded = false;
            Errors = errors;
        }

        public OperationResult(IEnumerable<string> errors)
        {
            Succeeded = false;
            Errors = errors.ToArray();
        }

        protected OperationResult(bool succeeded)
        {
            Succeeded = succeeded;
            Errors = new string[] { };
        }


        public T Value { get; set; }
        public bool Succeeded { get; }
        public string[] Errors { get; }


        public static OperationResult<T> Success => new OperationResult<T>(true);

        public static OperationResult<T> Failed(params string[] errors) => new OperationResult<T>(errors);

        public OperationResult<T> Clone() => Succeeded ? new OperationResult<T>(true) : new OperationResult<T>(Errors);


        public static implicit operator OperationResult<T>(OperationResult value)
        {
            return value.Succeeded ? new OperationResult<T>(true) : new OperationResult<T>(value.Errors);
        }
    }
}
