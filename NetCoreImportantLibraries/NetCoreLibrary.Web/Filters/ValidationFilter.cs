using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NetCoreLibrary.Web.Models;
using System;
using System.Linq;

namespace NetCoreLibrary.Web.Filters
{
    /// <summary>
    /// FluentValidation'i bir filter olarak kullanmak istersek,
    /// ModelState.IsValid kontrolünü filter aracılığı ile yapmak istediğimizde.
    /// </summary>
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errorResponseModel = new ErrorResponseModel
                {
                    Status = 400,
                    Errors = context.ModelState.Values.Where(p => p.Errors.Count > 0).SelectMany(p => p.Errors).Select(p => p.ErrorMessage).ToList()
                };

                context.Result = new BadRequestObjectResult(errorResponseModel);
            }
        }
    }
}
