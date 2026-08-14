using OS.Application.Common.Exceptions;
using OS.Application.Common.Models;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text.Json;

namespace OS.Webapi.Controllers.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var errors = new List<string>();
            var message = ex.Message;

            switch (ex)
            {
                case FluentValidation.ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = "Format yoki ma'lumotlarni kiritishda xatolik bbor.";
                    errors = validationException.Errors.Select(e => e.ErrorMessage).ToList();
                    break;

                case UnauthorizedAccessException:
                case SecurityTokenException:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = ex.Message;
                    errors.Add(ex.Message);
                    break;

                case NotFoundException:
                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = ex.Message;
                    errors.Add(ex.Message);
                    break;

                case ConflictException:
                    statusCode = HttpStatusCode.Conflict;
                    message = ex.Message;
                    errors.Add(ex.Message);
                    break;

                case InvalidOperationException:
                case ArgumentException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = ex.Message;
                    errors.Add(ex.Message);
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "Ichki server xatoligi yuz berdi.";
                    errors.Add(ex.Message);
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var responseOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var apiResponse = ApiResponse<object>.Failure(message, errors, (int)statusCode);
            var result = JsonSerializer.Serialize(apiResponse, responseOptions);

            return context.Response.WriteAsync(result);
        }
    }
}
