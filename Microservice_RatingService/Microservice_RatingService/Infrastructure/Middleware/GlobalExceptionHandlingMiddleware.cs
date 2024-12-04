using Microservice_RatingService.Application.Exceptions;
using Microservice_RatingService.Application.Models;

namespace Microservice_RatingService.Infrastructure.Middleware
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = new ApiErrorResponse();
            var statusCode = StatusCodes.Status500InternalServerError;

            switch (exception)
            {
                case RatingNotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    response.Message = exception.Message;
                    break;

                case SellerNotFoundException:
                case BuyerNotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    response.Message = exception.Message;
                    break;

                case RatingValidationException validationEx:
                    statusCode = StatusCodes.Status400BadRequest;
                    response.Message = "Validation failed";
                    response.Errors = validationEx.Errors.Select(e => new ValidationError
                    {
                        Field = "Validation",
                        Message = e
                    }).ToList();
                    break;

                case FluentValidation.ValidationException fluentEx:
                    statusCode = StatusCodes.Status400BadRequest;
                    response.Message = "Validation failed";
                    response.Errors = fluentEx.Errors.Select(e => new ValidationError
                    {
                        Field = e.PropertyName,
                        Message = e.ErrorMessage
                    }).ToList();
                    break;

                default:
                    response.Message = "An internal error occurred. Please try again later.";
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}

