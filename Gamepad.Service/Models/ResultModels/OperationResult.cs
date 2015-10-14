using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Gamepad.Service.Models.ResultModels
{
    public class OperationResult
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


        public bool Succeeded { get; }
        public string[] Errors { get; }


        public static OperationResult Success => new OperationResult(true);

        public static OperationResult Failed(params string[] errors) => new OperationResult(errors);

        public OperationResult Clone() => Succeeded ? new OperationResult(true) : new OperationResult(Errors);
    }
}
