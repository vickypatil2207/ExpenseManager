using ExpenseManager.Shared;
using System.Text.Json;

namespace ExpenseManager.Api.Middlewares
{
    public class UserAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public UserAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!HasSingupOrSignInRoute(context))
            {
                var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
                if (string.IsNullOrWhiteSpace(userIdClaim))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await WriteJsonResponse(context, "UserId Claim is missing.");
                    return;
                }

                var userIdFromRequest = GetUserIdFromRequest(context);
                if (!string.IsNullOrWhiteSpace(userIdFromRequest) && userIdFromRequest != userIdClaim)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await WriteJsonResponse(context, "UserId mismatch between JWT token and request.");
                    return;
                }
            }

            await _next(context);
        }

        private string GetUserIdFromRequest(HttpContext context)
        {
            string controller = string.Empty;
            if (context.Request.RouteValues.TryGetValue("controller", out var objController))
            {
                controller = (objController?.ToString() ?? string.Empty).ToLower();
            }

            if (context.Request.RouteValues.TryGetValue("userId", out var routeUserId))
            {
                return routeUserId?.ToString() ?? string.Empty;
            }

            if (controller == "user" && context.Request.RouteValues.TryGetValue("id", out var routeId))
            {
                return routeId?.ToString() ?? string.Empty;
            }

            if (context.Request.Query.TryGetValue("userId", out var queryUserId))
            {
                return queryUserId.ToString();
            }

            if (context.Request.ContentLength > 0
                    && (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put))
            {
                using var reader = new StreamReader(context.Request.Body);
                var body = reader.ReadToEnd();
                var jsonBody = JsonDocument.Parse(body);
                if (jsonBody.RootElement.TryGetProperty("userId", out var bodyUserId))
                {
                    return bodyUserId.ToString();
                }
            }

            return string.Empty;
        }

        private bool HasSingupOrSignInRoute(HttpContext context)
        {
            string controller = string.Empty;
            if (context.Request.RouteValues.TryGetValue("controller", out var objController))
            {
                controller = (objController?.ToString() ?? string.Empty).ToLower();
            }

            string action = string.Empty;
            if (context.Request.RouteValues.TryGetValue("action", out var objAction))
            {
                action = (objAction?.ToString() ?? string.Empty).ToLower();
            }

            if (controller == "user" && (string.IsNullOrWhiteSpace(action) || action.Contains("signup") || action.Contains("signin")))
            {
                return true;
            }

            return false;
        }

        private static async Task WriteJsonResponse(HttpContext context, string message)
        {
            ServiceResult<object> serviceResult = ServiceResult<object>.Fail(message);

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(serviceResult));
        }
    }
}
