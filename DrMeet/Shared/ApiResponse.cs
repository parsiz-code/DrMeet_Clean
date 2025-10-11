using System.Net;

namespace DrMeet.Api.Shared;

public class ApiResponse 
{
    public ApiResponse()
    {
        
    }


    public ApiResponse(string message, HttpStatusCode status = HttpStatusCode.OK)
    {
        if (status == HttpStatusCode.OK)
        {
            Message = message;
        }
        else
        {
            Errors = new string[] { message };
        }

        StatusCode = (int)status;
    }

    public ApiResponse(object? data)
    {
        Data = data;
        StatusCode = (int)HttpStatusCode.OK;
    }

    public ApiResponse(object data, string message)
    {
        Data = data;
        StatusCode = (int)HttpStatusCode.OK;
        Message = message;
    }

    public ApiResponse(string[] errors)
    {
        Errors = errors;
        StatusCode = (int)HttpStatusCode.BadRequest;
    }

    public ApiResponse(string message, HttpStatusCode status, string[] errors, object data)
    {
        Message = message;
        StatusCode = (int)status;
        Errors = errors;
        Data = data;
    }

    public string Message { get; set; }
    public string[] Errors { get; set; }
    public int StatusCode { get; set; }
    public object? Data { get; set; }

}