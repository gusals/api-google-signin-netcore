using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Modules
{
    /// <summary>
    /// Exception Filter.
    /// </summary>
    public sealed class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Add Problem Details when occurs Domain Exception.
        /// </summary>
        /// <param name="exceptionContext">A context for exception filters.</param>
        public void OnException(ExceptionContext exceptionContext)
        {
            var problemDetails = new ProblemDetails { Status = 500, Title = "Bad Request" };
            exceptionContext.Result = new JsonResult(value: problemDetails);
            exceptionContext.Exception = null!;
        }
    }
}