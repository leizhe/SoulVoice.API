using SV.Application.Status;

namespace SV.Application.Exceptions
{
    public class UpdateFailedException : BaseException
    {
        public override int ExceptionCode => (int)StatusCode.UpdateFailed;
    }
}
