using SV.Application.Status;

namespace SV.Application.Exceptions
{
    public class NotExistException : BaseException
    {
        public override int ExceptionCode => (int)StatusCode.NotExistFailed;
    }
}
