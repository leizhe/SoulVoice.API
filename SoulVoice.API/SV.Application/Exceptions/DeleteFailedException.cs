using SV.Application.Status;

namespace SV.Application.Exceptions
{
    public class DeleteFailedException : BaseException
    {
        public override int ExceptionCode => (int)StatusCode.DeleteFailed;
    }
}
