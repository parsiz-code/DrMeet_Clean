using System.Collections.Specialized;
using RestSharp;

namespace DrMeet.Api.Shared.Services.HttpService;

public interface IHttpService
{
    Task<RestResponse> GetAsync(string url, NameValueCollection? query = null, Dictionary<string, string>? headers = null);
    Task<RestResponse> PostAsync(string url, string model, Dictionary<string, string>? headers = null);
    Task<RestResponse> PostAsync<T>(string url, T model, Dictionary<string, string>? headers = null) where T : class;
    Task<RestResponse> PutAsync(string url, object model, Dictionary<string, string>? headers = null);
    Task<RestResponse> DeleteAsync(string url, Dictionary<string, string>? headers = null);

    Task<RestResponse> PutFormAsync(string url, Dictionary<string, IFormFile> files,
        Dictionary<string, string> data, Dictionary<string, string>? headers = null);
    Task<RestResponse> PostFormAsync(string url, Dictionary<string, IFormFile> files,
        Dictionary<string, string> data, Dictionary<string, string>? headers = null);
}