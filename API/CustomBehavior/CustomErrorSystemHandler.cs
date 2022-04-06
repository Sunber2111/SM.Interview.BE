using System;
using System.Linq;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API.CustomBehavior
{
    public static class CustomErrorSystemHandler
    {
        public static BadRequestObjectResult Handler(this ActionContext actionContext)
        {
            var errors = actionContext.ModelState
                       .Where(e => e.Value.Errors.Count > 0)
                       .Select(e => {
                           var key = e.Key.Contains(".") ? e.Key.Split(".").TakeLast(1).First() : e.Key;
                           var value = e.Value.Errors[e.Value.Errors.Count - 1].ErrorMessage;
                           var errorMessage = value.Contains("$") ? String.Format("Wrong Params {0}", key) : String.Format("Wrong Params {0} : {1}", key, value);
                           return new
                           {
                               status = "Fail",
                               error = errorMessage,
                               code = ErrorCodeBase.AppError
                           };

                       }).FirstOrDefault();

            return new BadRequestObjectResult(errors);
        }
    }
}
