namespace SV.Application.Output
{
    public class GetResult<T> : OutputBase
    {
        public T Data { get; set; }
    }
}