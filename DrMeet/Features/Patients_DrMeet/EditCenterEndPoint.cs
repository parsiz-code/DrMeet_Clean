using DrMeet.Api.Features.Patients.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Patients;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace DrMeet.Api.Features.Patients_DrMeet;
public class EditPatientEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/EditPatient", handler: async (
                    IPatientService service,
                    [FromBody] UpdatePatientRequestDto request
                ) =>
            {
                var validationResults = new List<ValidationResult>();
                var context = new ValidationContext(request);
                bool isValid = Validator.TryValidateObject(request, context, validationResults, true);

                if (!isValid)
                {
                    var errors = validationResults.Select(v => v.ErrorMessage).ToList();
                    return BadRequest(string.Join(",", errors));
                }



                if (request is null)
                    return Results.BadRequest("Request body is null.");

                var result = await service.EditPatient(request);

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Results.Ok(result.LstMessage.GetString());
            })
            .WithTags(ApiInfo.Tag);

        }
    }
}