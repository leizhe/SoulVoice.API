using SV.Application.Status;

namespace SV.Application.Exceptions
{
    public class AddFailedException : BaseException
    {
        public override int ExceptionCode => (int)StatusCode.AddFailed;
    }
}
