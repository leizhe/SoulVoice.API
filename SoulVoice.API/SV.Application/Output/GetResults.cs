using System.Collections.Generic;

namespace SV.Application.Output
{
    public class GetResults<T> : OutputBase
    {
        public long Total { get; set; }

        public List<T> Data { get; set; }
    }
}