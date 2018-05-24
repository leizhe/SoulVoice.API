using System;
using SV.Common.IoC;

namespace SV.Common.Extensions
{
    public static class IoCContainerExtensions
    {
        public static void AddAspect(this IocContainer container)
        {
            if(container==null)
                throw new ArgumentNullException(nameof(container));
        }
    }
}
