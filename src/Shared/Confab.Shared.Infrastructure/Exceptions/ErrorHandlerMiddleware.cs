using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Confab.Shared.Infrastructure.Exceptions
{
    internal class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> logger;
        private readonly IExceptionCompositionRoot exceptionCompositionRoot;

        public ErrorHandlerMiddleware(
            ILogger<ErrorHandlerMiddleware> logger,
            IExceptionCompositionRoot exceptionCompositionRoot)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.exceptionCompositionRoot = exceptionCompositionRoot ?? throw new ArgumentNullException(nameof(exceptionCompositionRoot));
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                this.logger.LogError(exception, exception.Message);
                await HandleErrorAsync(context, exception);
            }
        }

        private async Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var errorResponse = this.exceptionCompositionRoot.Map(exception);
            context.Response.StatusCode = (int)(errorResponse?.StatusCode ?? System.Net.HttpStatusCode.InternalServerError);
            var response = errorResponse?.Respone;
            if (response is null)
            {
                return;
            }
            
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
