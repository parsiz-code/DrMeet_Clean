using System.Collections.Specialized;
using System.Text;
using Newtonsoft.Json;
using RestSharp;

namespace DrMeet.Api.Shared.Services.HttpService;

public class HttpService : IHttpService
{
    private readonly RestClient _client = new();
    private const string UrlEncodedContentType = "application/x-www-form-urlencoded";
    public const string JsonContentType = "application/json";

    public async Task<RestResponse> GetAsync(string url, NameValueCollection? query = null,
        Dictionary<string, string>? headers = null)
    {
        var uriBuilder = new UriBuilder(url);

        var request = new RestRequest(uriBuilder.ToString(), Method.Get);

        if (query != null)
        {
            foreach (string item in query)
            {
                request.AddQueryParameter(item, query[item]);
            }
        }

        if (headers != null && headers.Any())
        {
            foreach (var h in headers)
            {
                request.AddHeader(h.Key, h.Value);
            }
        }

        return await _client.ExecuteAsync(request)
            .ConfigureAwait(false);
    }

    public async Task<RestResponse> PostAsync(string url, string model, Dictionary<string, string>? headers = null)
    {
        var uriBuilder = new UriBuilder(url);

        var request = new RestRequest(uriBuilder.ToString(), Method.Post);

        if (headers != null && headers.Any())
        {
            foreach (var h in headers)
            {
                request.AddHeader(h.Key, h.Value);
            }
        }

        if (headers != null && headers.Any(s => s.Value == UrlEncodedContentType))
        {
            request.AddParameter(UrlEncodedContentType, model, ParameterType.RequestBody);
        }
        else
        {
            request.AddJsonBody(model);
        }

        return await _client.ExecuteAsync(request).ConfigureAwait(false);
    }


    public async Task<RestResponse> PostAsync<T>(string url, T model, Dictionary<string, string>? headers = null)
        where T : class
    {
        var uriBuilder = new UriBuilder(url);

        var request = new RestRequest(uriBuilder.ToString(), Method.Post);

        if (headers != null && headers.Any())
        {
            foreach (var h in headers)
            {
                request.AddHeader(h.Key, h.Value);
            }
        }

        if (headers != null && headers.Any(s => s.Value == UrlEncodedContentType))
        {
            request.AddParameter(UrlEncodedContentType, UrlEncodedBodyGenerator(model), ParameterType.RequestBody);
        }
        else
        {
            request.AddJsonBody(model);
        }

        return await _client.ExecuteAsync(request).ConfigureAwait(false);
    }

        
    public async Task<RestResponse> PostFormAsync(string url, Dictionary<string, IFormFile> files,
        Dictionary<string, string> data, Dictionary<string, string>? headers = null)
    {
        var uriBuilder = new UriBuilder(url);
        var request = new RestRequest(uriBuilder.ToString(), Method.Post);

        if (headers != null && headers.Any())
        {
            foreach (var h in headers)
            {
                request.AddHeader(h.Key, h.Value);
            }
        }

        foreach (var item in files??[])
        {
            await using var ms = new MemoryStream();
            await item.Value.CopyToAsync(ms);
            var fileBytes = ms.ToArray();
            request.AddFile(item.Key, fileBytes, item.Value.FileName);
        }

        foreach (var item in data??[])
        {
            request.AddParameter(item.Key, item.Value, ParameterType.RequestBody);
        }
            
        request.AddHeader("Content-Type", "multipart/form-data");
            
        return await _client
            .ExecuteAsync(request)
            .ConfigureAwait(false);
    }
        
    public async Task<RestResponse> PutAsync(string url, object model, Dictionary<string, string>? headers = null)
    {
        var uriBuilder = new UriBuilder(url);
        var request = new RestRequest(uriBuilder.ToString(), Method.Put);

        if (headers != null && headers.Any())
        {
            foreach (var h in headers)
            {
                request.AddHeader(h.Key, h.Value);
            }
        }

        request.AddJsonBody(JsonConvert.SerializeObject(model));
        return await _client.ExecuteAsync(request)
            .ConfigureAwait(false);
    }

    public async Task<RestResponse> DeleteAsync(string url, Dictionary<string, string>? headers = null)
    {
        var uriBuilder = new UriBuilder(url);
        var request = new RestRequest(uriBuilder.ToString(), Method.Delete);
        if (headers != null && headers.Any())
        {
            foreach (var h in headers)
            {
                request.AddHeader(h.Key, h.Value);
            }
        }

        return await _client.ExecuteAsync(request)
            .ConfigureAwait(false);
    }


    public async Task<RestResponse> PutFormAsync(string url, Dictionary<string, IFormFile> files,
        Dictionary<string, string> data, Dictionary<string, string>? headers = null)
    {
        var uriBuilder = new UriBuilder(url);
        var request = new RestRequest(uriBuilder.ToString(), Method.Put);

        if (headers != null && headers.Any())
        {
            foreach (var h in headers)
            {
                request.AddHeader(h.Key, h.Value);
            }
        }

        foreach (var item in files??[])
        {
            await using var ms = new MemoryStream();
            await item.Value.CopyToAsync(ms);
            var fileBytes = ms.ToArray();
            request.AddFile(item.Key, fileBytes, item.Value.FileName);
        }

        foreach (var item in data??[])
        {
            request.AddParameter(item.Key, item.Value, ParameterType.RequestBody);
        }
            
        request.AddHeader("Content-Type", "multipart/form-data");
            
        return await _client
            .ExecuteAsync(request)
            .ConfigureAwait(false);
    }


    private string UrlEncodedBodyGenerator(object urlEncodeDModel)
    {
        var builder = new StringBuilder();

        foreach (var prop in urlEncodeDModel.GetType().GetProperties())
        {
            if (prop.GetValue(urlEncodeDModel, null) as string is { } value)
                builder.Append($"{prop.Name}={value}&");
        }

        var result = builder.ToString();

        if (result.EndsWith("&"))
            result = result[..^1];

        return result;
    }
}