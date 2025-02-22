using Entities.ErrorDetail;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,       
                        BadRequestException => StatusCodes.Status400BadRequest,
                        UnauthorizedAccessException => StatusCodes.Status401Unauthorized,  
                        ValidationException => StatusCodes.Status422UnprocessableEntity,  
                        DbUpdateException => StatusCodes.Status500InternalServerError,
                        ForbiddenException => StatusCodes.Status403Forbidden,
                        _ => StatusCodes.Status500InternalServerError   
                    };

                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = contextFeature.Error.Message,
						Details = (app.Environment.IsProduction()) ? null :contextFeature.Error.ToString()

					}.ToString());
                }
            });
        });
    }
}