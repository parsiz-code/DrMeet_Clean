//using DrMeet.Api.Features.Doctors.Services;
//using DrMeet.Api.Shared.Contracts;

//namespace DrMeet.Api.Features.Doctors.EndPoints;

//public static class ImportDoctors
//{
//    public class EndPoint : BaseEndpoint, IEndpoint
//    {
//        public void MapEndpoint(IEndpointRouteBuilder app)
//        {
//            app.MapGet($"{ApiInfo.Prefix}/import/4B13077D-E6BD-4516-9F73-875CB1D8BB3F", handler: async (
//                    ImportDoctorsService service
//                ) =>
//                {
//                    await service.ImportAsync();
//                    return Ok("ok");
//                })
//                .WithTags(ApiInfo.Tag)
//                .ExcludeFromDescription();
//        }
//    }
//}