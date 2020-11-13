using CoreGram.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CoreGram.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandlerExcetionAsync(context, ex);
            }
        }

        private static Task HandlerExcetionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode code;            

            if (exception is BadRequestException) code = HttpStatusCode.BadRequest;
            else if (exception is UnauthorizedException) code = HttpStatusCode.Unauthorized;
            else if (exception is NotFoundException) code = HttpStatusCode.NotFound;
            else if (exception is NotAllowedException) code = HttpStatusCode.MethodNotAllowed;
            else if (exception is UnprocessableEntityException) code = HttpStatusCode.UnprocessableEntity;
            else code = HttpStatusCode.BadRequest;

            return ExceptionResponse(context, code, exception.Message);
        }

        private static Task ExceptionResponse(HttpContext context, HttpStatusCode code, string message)
        {
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(new ResponseError()
            {
                Code = context.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
