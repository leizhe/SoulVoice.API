using System;

namespace SV.Application.Exceptions
{
    public abstract class BaseException : Exception
    {
        public abstract int ExceptionCode { get; }
    }
}
