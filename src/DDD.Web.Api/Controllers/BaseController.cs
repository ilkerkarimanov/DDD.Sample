using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DDD.Common.Cqs;
using DDD.Common.Domain;
using DDD.Common;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DDD.Web.Api.Controllers
{
    public abstract class BaseController : Controller
    {
        protected virtual IActionResult ErrorResult(string message)
        {
            return ErrorResult("request_error", message);
        }

        protected virtual IActionResult ErrorResult(string error, string message)
        {
            return new ObjectResult(
                            new
                            {
                                Status = "Failed",
                                Error = error,
                                Message = message
                            })
            { StatusCode = StatusCodes.Status400BadRequest };
        }

        protected virtual IActionResult ErrorResult(FailureResult ex) {
            if (ex.Errors != null && ex.Errors.Count() > 0)
            {
                var result = Result.Fail(ex.Errors.ToArray());
                return ToResult(result);
            }
            else
            {
                return ErrorResult(ex.Message);
            }
        }

        protected virtual IActionResult ErrorResult(Exception ex)
        {
            return new ObjectResult(
                            new
                            {
                                Status = "Failed",
                                Error = "server_error",
                                Message = "Internal server error"
                            })
            { StatusCode = StatusCodes.Status500InternalServerError };
        }

        protected virtual IActionResult ErrorModelResult()
        {
            var errors = ModelState
                    .SelectMany(value => value.Value.Errors)
                    .Select(error => error.ErrorMessage)
                    .ToArray();

            var modelValidationResult = errors.Length == 0 ? Common.Result.Ok() : Common.Result.Fail(errors.ToArray());

            return modelValidationResult.Succeeded ?  OkResult() : new ObjectResult(
                new
                {
                    Status = "Failed",
                    Error = "request_error",
                    Message = modelValidationResult.Errors
                }) { StatusCode = StatusCodes.Status400BadRequest };
        }

        protected virtual IActionResult ToResult(Result result)
        {
            return result.Succeeded ? OkResult() : new ObjectResult(
                new
                {
                    Status = "Failed",
                    Error = "request_error",
                    Message = result.Errors
                })
            { StatusCode = StatusCodes.Status400BadRequest };
        }

        protected virtual IActionResult OkResult()
        {
            return new OkObjectResult(
                new {
                Status = "Success"
            });
        }

        protected virtual IActionResult OkResult(object value)
        {
            return Ok(value);
        }
        
    }
}
