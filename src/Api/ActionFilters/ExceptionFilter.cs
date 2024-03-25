using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.ActionFilters;

public class ExceptionFilter : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ValidationException exception:
                HandleBadRequest(context, exception);
                return;
            default:
                HandleInternatError(context);
                return;
        }
    }

    private static void HandleInternatError(ExceptionContext context)
    {
        var actionResult = new JsonResult(
            new ErrorResponse
            (
                HttpStatusCode.InternalServerError,
                "возникла ошибка, уже чиним"
            ));

        actionResult.StatusCode = (int?)HttpStatusCode.InternalServerError;
        context.Result = actionResult;
    }

    private static void HandleBadRequest(ExceptionContext context, ValidationException exception)
    {
        var contextResult = new JsonResult(
            new ErrorResponse
            (
                HttpStatusCode.BadRequest,
                exception.Message
            ));

        contextResult.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = contextResult;
    }
}