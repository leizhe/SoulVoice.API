using Microsoft.AspNetCore.Mvc.Filters;
using SV.Common.Helpers;

namespace SV.Common.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var type = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            Log4NetHelper.WriteError(type, filterContext.Exception);
        }
    }
}
