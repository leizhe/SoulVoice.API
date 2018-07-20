using SV.Application.Status;

namespace SV.Application.Exceptions
{
    public class ExistedException : BaseException
    {
        public override int ExceptionCode => (int)StatusCode.ExistedFailed;
    }
}
