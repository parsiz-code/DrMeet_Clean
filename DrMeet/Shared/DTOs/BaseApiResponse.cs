namespace DrMeet.Api.Shared.DTOs;

public class BaseApiResponse<TData>
{
    public string? Message { get; set; }
    public string[]? Errors { get; set; }
    public int StatusCode { get; set; }
    public TData? Data { get; set; }
}