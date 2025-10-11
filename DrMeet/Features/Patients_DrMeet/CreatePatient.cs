using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.JwtService;
using DrMeet.Api.Shared.Services.ParsizTeb;
using DrMeet.Api.Shared.Services.ParsizTeb.Models;
using DrMeet.Api.Shared.Services.Patients;

namespace DrMeet.Api.Features.Patients_DrMeet;

public static class CreatePatient
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}", handler: async (
                    IPatientService patientService,
                    IJwtService jwtService,
                    CreatePatientRequestDto request
                ) =>
                {
                    var result = await patientService.CreatePatientAsync(request);
                    if (result.IsError)
                        return BadRequest(string.Join(",", result.Errors.Select(_ => _.Description)));
                    var token = jwtService.CreateToken(new LoginTokenRequest()
                    {
                        //Id = result.Value
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