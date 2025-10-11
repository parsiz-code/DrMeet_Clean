using System.Net;
using FluentValidation.Results;

namespace DrMeet.Api.Shared.Contracts;

public interface IEndpoint {
    void MapEndpoint(IEndpointRouteBuilder app);
}


public abstract class BaseEndpoint {

    protected IResult Ok(string message)
        => TypedResults.Ok(new ApiResponse(message));

    protected IResult Ok(object? data)
        => TypedResults.Ok(new ApiResponse(data));

    protected IResult BadRequest(string message)
        => TypedResults.BadRequest(new ApiResponse(message,
            HttpStatusCode.BadRequest));

    protected IResult BadRequest(string[] message)
        => TypedResults.BadRequest(new ApiResponse(message));

    protected IResult BadRequest(ValidationResult result)
        => BadRequest(result.Errors.Select(x => x.ErrorMessage).ToArray());

    //protected IResult NotFound(string message)
    //    => TypedResults.NotFound(new ApiResponse(message,
    //        HttpStatusCode.NotFound));

    protected IResult Unauthorized()
        => TypedResults.Unauthorized();
}
