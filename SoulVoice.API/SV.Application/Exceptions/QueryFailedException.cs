using System;
using SV.Application.Status;

namespace SV.Application.Exceptions
{
    public class QueryFailedException : BaseException
    {
        public override int ExceptionCode => (int)StatusCode.QueryFailed;
    }
}
