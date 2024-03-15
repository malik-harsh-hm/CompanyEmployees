using Contracts;
using Entities.ErrorModels;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace CompanyEmployees.Extensions
{
    public static class ErrorMiddlewareEntensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError => {
                appError.Run(async context => {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        switch (contextFeature.Error)
                        {
                            case NotFoundException:
                                context.Response.StatusCode = StatusCodes.Status404NotFound;
                                break;
                            default:
                                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                                break;
                        }

                        logger.LogError($"Something went wrong: {contextFeature.Error}");

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                        }.ToString());
                    }
                });
            });
        }
    }
}
