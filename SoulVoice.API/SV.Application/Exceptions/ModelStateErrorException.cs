using SV.Application.Status;

namespace SV.Application.Exceptions
{
    public class ModelStateErrorException : BaseException
    {
        public override int ExceptionCode => (int)StatusCode.ModelStateError;
    }
}
