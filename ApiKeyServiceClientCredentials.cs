using Microsoft.Rest;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

public class ApiKeyServiceClientCredentials : ServiceClientCredentials
{
    private readonly string subscriptionKey;

    public ApiKeyServiceClientCredentials(string subscriptionKey)
    {
        this.subscriptionKey = subscriptionKey;
    }
}

public class ServiceClientCredentials
{
    private IEnumerable<string?> subscriptionKey;

    public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add("Ocp-Apim-Subscription-Key", this.subscriptionKey);
        return base.ProcessHttpRequestAsync(request, cancellationToken);
    }
}