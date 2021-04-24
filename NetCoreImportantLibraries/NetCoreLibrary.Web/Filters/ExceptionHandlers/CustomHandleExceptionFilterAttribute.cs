using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace NetCoreLibrary.Web.Filters.ExceptionHandlers
{
    public class CustomHandleExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var viewResult = new ViewResult
            {
                ViewName = "Hata1",
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState)
            };

            viewResult.ViewData.Add("Exception", context.Exception);
            context.Result = viewResult;
        }
    }
}
