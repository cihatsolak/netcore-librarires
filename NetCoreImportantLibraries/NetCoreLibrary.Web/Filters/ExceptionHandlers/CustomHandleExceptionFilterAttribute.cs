using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace NetCoreLibrary.Web.Filters.ExceptionHandlers
{
    public class CustomHandleExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public string ErrorViewName { get; set; }

        public override void OnException(ExceptionContext context)
        {
            var viewResult = new ViewResult
            {
                ViewName = ErrorViewName,
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState)
            };

            viewResult.ViewData.Add("Exception", context.Exception);
            viewResult.ViewData.Add("Url", context.HttpContext.Request.Path.Value);

            context.Result = viewResult;
        }
    }
}
