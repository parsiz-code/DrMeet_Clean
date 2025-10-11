//using DNTCommon.Web.Core;
//using DNTPersianUtils.Core.IranCities;
//using DrMeet.Api.Features.DoctorReserveTimes.DTOs;
//using DrMeet.Api.Features.Doctors.EndPoints.DTOs;

//using DrMeet.Api.Shared;
//using DrMeet.Api.Shared.Contracts;
//using DrMeet.Api.Shared.Domian;
//using DrMeet.Api.Shared.Extensions;
//using DrMeet.Api.Shared.PagedList;
//using DrMeet.Api.Shared.Persistence.UnitOfWork;
//using DrMeet.Api.Shared.Services.DoctorReserveTime;
//using DrMeet.Api.Shared.Services.ParsizTeb;
//using iTextSharp.text;
//using Mapster;
//using Mapster.Utils;
//using MongoDB.Driver.Linq;
//using System.Linq;
//namespace DrMeet.Api.Features.Doctors.EndPoints;

//public static class GetDoctorList
//{
//    public class EndPoint(IParsizTebApiService parsizTebApiService, IDoctorReserveTimeService doctorReserveTimeService) : BaseEndpoint, IEndpoint
//    {
//        public void MapEndpoint(IEndpointRouteBuilder app)
//        {
//            app.MapGet($"{ApiInfo.Prefix}", handler: async (
//                    IUnitOfWork uow,
//                    [AsParameters] GetDoctorListParams request
//                ) =>
//                {
                
//                    var query = uow.Doctors.AsQueryable();

//                    if (!string.IsNullOrEmpty(request.Name))
//                    {
//                        query =
//                            query.Where(d => d.FirstName.Contains(request.Name) || d.LastName.Contains(request.Name));
//                   }

//                    if (request.WorkDays is { Length: > 0 })
//                    {
//                        query = query
//                             .Where(d => d.Shifts.Any(sh => request.WorkDays.Contains(sh.DayOfWeek)));

//                    }

//                    if (request.CityId.HasValue())
//                    {
//                        var city = await uow.IranCities.GetByIdAsync(request.CityId);
//                        query = query.Where(x => x.CityId == request.CityId);


//                    }
//                    if (request.ServicePrice.HasValue)
//                    {
//                        var doctorIds = await uow.DoctorTariffs.AsQueryable()
//                                    .Where(d => d.Price > request.ServicePrice)
//                                    .Select(d => d.DoctorId)
//                                    .ToListAsync();
//                        query = query
//                         .Where(doc => doctorIds.Contains(doc.Id));
//                    }
//                    if (request.ProvinceId.HasValue())
//                    {

//                        query = query.Where(x => x.ProvinceId == request.ProvinceId);

//                    }
//                    if (request.ExpertisesName.HasValue())
//                    {
//                        query = query.Where(x => x.ExpertiseName.ToLower().Contains(request.ExpertisesName));
                      
//                    }
//                    var a = query.ToList();
//                    if (request.ServiceId is not null)
//                    {

//                        var service = await uow.ServicesAvailable.GetByIdAsync(request.ServiceId);
//                        var doctorIds = await uow.DoctorTariffs.AsQueryable()
//                                            .Where(d => d.ServicesAvailableId == request.ServiceId)
//                                            .Select(d => d.DoctorId)
//                                            .ToListAsync();

//                        query = query
//                            .Where(doc => doctorIds.Contains(doc.Id));
                    
//                    }

//                    if (request.Gender.HasValue)
//                    {
//                        query = query.Where(d => d.Gender == request.Gender);
                      
//                    }

//                    if (request.Score.HasValue)
//                    {
//                        query = query.Where(d => d.Score >= request.Score);
//                    }

               

//                    var result =await query.ToPagedList(x => new GetDoctorListDto
//                    {
//                        Id = x.Id,
//                        FullName = x.FirstName + " " + x.LastName,
//                        Picture = string.IsNullOrEmpty(x.Picture) ? "" : x.Picture,
//                        Expertise = x.ExpertiseName,
//                        OnlineVisit = new OnlineVisit
//                        {
//                            IsOnlineVisitAvailable = x.ShowInOnlineReserveTime,
//                            Price = 300_000//todo,
//                        },
//                        Score = new DoctorScore
//                        {
//                            Average = x.Score,
//                            Count = new Random().Next(5, 100)
//                        }
//                    },request.PageNumber.Value, request.PageSize.Value);

      

//                    foreach (var item in result.List)
//                    {
//                        item.Picture = string.IsNullOrEmpty(item.Picture) ? DefaultValues.DoctorManPicture : item.Picture;

//                        item.ExperienceYears = item.ExperienceYears is 0 ? new Random().Next(2, 10) : item.ExperienceYears;
                 
                   
//                        item.Address = new()
//                        {
//                            AddressLine = "خیابان شریعتی ",
//                            City = "ساری",
//                            Province = "مازندران",
//                        };
//                    }
//                    return Ok(result);
//                })
//                .WithTags(ApiInfo.Tag);
//        }
//    }
//}