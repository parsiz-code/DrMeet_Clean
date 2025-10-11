using DrMeet.Api.Features.Centers.DTOs;

using DrMeet.Api.Shared.Domian;
using static DrMeet.Api.Shared.Services.ParsizTeb.Models.GetPatientByIdResponse;

namespace DrMeet.Api.Features.Account.DTOs
{
    public class DetailsUserResponseDto
    {

        public DetailsUserResponseDto()
        {
            Centers = new List<CenterResponseDto>();
        }
        public object PersonalInfo { get; set; }
        public List<CenterResponseDto> Centers { get; set; }
    }
    public class DetailsAdminUserResponseDto
    {

       
        public object PersonalInfo { get; set; }
        
    }

    public class DoctorDetailsUserResponseDto
    {

        public DoctorDetailsUserResponseDto()
        {
   
        }
        public object PersonalInfo { get; set; }
        public List<CenterResponseDto> Centers { get; set; }
    }
}
