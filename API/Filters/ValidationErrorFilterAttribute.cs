using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters {
    public class ValidationErrorFilter: Attribute, IExceptionFilter {
        public void OnException(ExceptionContext context) {
            var exceptionType = context.Exception.GetType();

            if (exceptionType == typeof(ArgumentException)) {
                Console.WriteLine(context.Exception.Message);
                
                var error = "validation-error";
                if (context.Exception.Message == "Email already in used") error = "email-unique-error";
                
                context.Result = new JsonResult(new Dictionary<string, dynamic> {
                    { "success", false },
                    { "error", error }
                });
                context.HttpContext.Response.StatusCode = 400;
                context.ExceptionHandled = true;
            }
        }
    }
}