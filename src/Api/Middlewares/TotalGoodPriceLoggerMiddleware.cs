using System.Text.Json;
using Api.Responses.V3;

namespace Api.Middlewares;

public class TotalGoodPriceLoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    
    public TotalGoodPriceLoggerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<TotalGoodPriceLoggerMiddleware>();
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.EnableBuffering();
        context.Request.Body.Seek(0, SeekOrigin.Begin);
        var srRequest = new StreamReader(context.Request.Body);
        var requestBodyString = await srRequest.ReadToEndAsync();
        _logger.LogInformation(requestBodyString);

        await _next.Invoke(context);
    }
}