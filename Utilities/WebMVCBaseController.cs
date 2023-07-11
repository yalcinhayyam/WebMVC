
using Microsoft.AspNetCore.Mvc;
namespace WebMVC.Utilities;


public class WebMVCBaseController<T> : Controller
{

    private readonly ILogger<T> logger;
    protected IHttpClientFactory Factory => factory ??= HttpContext.RequestServices.GetRequiredService<IHttpClientFactory>();
    private IHttpClientFactory? factory;

    public WebMVCBaseController(ILogger<T> logger)
    {
        this.logger = logger;
    }

    public async Task<Result<T>?> GetFromJsonAsync<T>(string? requestUri, CancellationToken cancellationToken = default)
    {
        using var client = Factory.CreateClient("BaseClient");

        HttpResponseMessage response = await client.GetAsync(requestUri, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            string errorMessage = $"HTTP request failed with status code: {response.StatusCode}";
            
            logger.LogError(errorMessage, response);

            return new NetworkError(errorMessage);
        }

        return (await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken))!;
    }


}



