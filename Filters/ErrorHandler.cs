using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

public class ErrorHandlerMiddleware
{
    public ErrorHandlerMiddleware(RequestDelegate next)
    
    {
        this.next = next;
    }

    private readonly RequestDelegate next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch(Exception ex){
            await HandleException(context, ex);
        }
    }

    private static Task HandleException(HttpContext context, Exception exception) {
        var code = HttpStatusCode.InternalServerError;

        if (exception.GetType().IsSubclassOf(typeof(BaseNotExistException))) {
            code = HttpStatusCode.NotFound;
        } else if(exception.GetType().IsSubclassOf(typeof(BaseAlreadyExistException))) {
            code = HttpStatusCode.Conflict;
        }
        else {

        }

        var result = JsonConvert.SerializeObject(new { message = exception.Message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}