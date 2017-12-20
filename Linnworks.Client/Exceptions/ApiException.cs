using System;
using Linnworks.Contract.Entities;

namespace Linnworks.Client.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string code, string message)
            : base(message)
        {
            Code = code;
        }

        public ApiException(ErrorMessage error)
            : this(error.Code, error.Message)
        {
        }

        public ApiException(string message)
            : base(message)
        {}

        public string Code { get; }
    }
}