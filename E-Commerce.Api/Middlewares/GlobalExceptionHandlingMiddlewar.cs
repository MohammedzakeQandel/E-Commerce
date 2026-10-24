using Azure;
using Domain.Exceptions;

using Shared.ErroreModel;
using System;
using System.Reflection.Metadata;

namespace E_Commerce.Api.Middlewares
{
    public class GlobalExceptionHandlingMiddlewar
    {
        private readonly RequestDelegate _naext;
        private readonly ILogger<GlobalExceptionHandlingMiddlewar> _logger;

        public GlobalExceptionHandlingMiddlewar(RequestDelegate naext, ILogger<GlobalExceptionHandlingMiddlewar> logger)
        {
            _naext = naext;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _naext(context);
                if(context.Response.StatusCode==StatusCodes.Status404NotFound)
                    await HandelNotFoundApiAsync(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while processing the request");
                await HandleExceptionAsync(context, ex);
                
            }
        }

        private async Task HandelNotFoundApiAsync(HttpContext context)
        {
             context.Response.ContentType = "application/json";
            //write response in body 
            var response = new ErroreDetails()
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErroreMessage = $"the endpoint with {context.Request.Path} Not Found"
            }.ToString();
            await context.Response.WriteAsync(response);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var response = new ErroreDetails()
            {
                // StatusCode = context.Response.StatusCode,
                ErroreMessage = ex.Message
            };
            //Change Status Code
            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                ValidationsException validationException => HandelValidationException(validationException, response),
                (_) => StatusCodes.Status500InternalServerError

            };
            //Chang Content Type
            
            //write response in body 
            response.StatusCode = context.Response.StatusCode;
            await context.Response.WriteAsync(response.ToString());

        }

        private int HandelValidationException(ValidationsException validationException, ErroreDetails response)
        {
            response.Errors=validationException.Errors;
            return StatusCodes.Status400BadRequest;
        }
    }
}
