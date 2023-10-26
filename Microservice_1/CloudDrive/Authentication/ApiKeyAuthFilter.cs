using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Microservice_1.Authentication;

public class ApiKeyAuthFilter : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult("Api Key missing");
            return;
        }

        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var apiKeys = configuration.GetSection(AuthConstants.ApiKeySectionName).Get<List<string>>();

        if (!apiKeys.Contains(extractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult("Invalid Api Key ");
            return;
        }

    }
}
