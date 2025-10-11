using DrMeet.Api.Features.Appointment;
using DrMeet.Api.Features.Doctors.EndPoints.DTOs;
using DrMeet.Api.Shared.Domian.Doctors;
using Mapster;

namespace DrMeet.Api.Shared.Mappping;

public static class MappingProfile
{
    public static void AddMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<Doctor, GetDoctorSelectListDto>
            .NewConfig();
            //.Map(member: dest => dest.FullName, source: src => src.())
            //.Map(member: dest => dest.Picture, source: src => src.GetPicture());

        TypeAdapterConfig<Doctor, GetDoctorDetailDto>
            .NewConfig();
        //.Map(member: dest => dest.FullName, source: src => src.GetFullName())
        //.Map(member: dest => dest.Picture, source: src => src.GetPicture());

        TypeAdapterConfig<Doctor, GetDoctorProfileResponseDto>
        .NewConfig();
        //.Map(member: dest => dest.FullName, source: src => src.GetFullName())
        //.Map(member: dest => dest.Picture, source: src => src.GetPicture());

        //TypeAdapterConfig<DoctorFreeTimesForReserveTime, GetDoctorFreeTimes.GetDoctorFreeTimesForReserveTimeResponse>
        //    .NewConfig()
        //    .Map(member: dest => dest.FreeTimes, source: src => src.FreeTimes.Where(t => !t.ReservedBefore).ToList());
    }
}