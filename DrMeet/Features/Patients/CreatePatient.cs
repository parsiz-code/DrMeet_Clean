using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.JwtService;
using DrMeet.Api.Shared.Services.ParsizTeb;
using DrMeet.Api.Shared.Services.ParsizTeb.Models;

namespace DrMeet.Api.Features.Patients;

public static class CreatePatient
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}", handler: async (
                    IParsizTebApiService parsizTebApi,
                    IJwtService jwtService,
                    CreatePatientRequestDto request
                ) =>
                {
                    var result = await parsizTebApi.CreatePatientAsync(request);

                    var token = jwtService.CreateToken(new LoginTokenRequest()
                    {
                        //Id = result
                    });

                    return Ok(new
                    {
                        token
                    });
                })
                .WithTags(ApiInfo.Tag);

        }
    }
}