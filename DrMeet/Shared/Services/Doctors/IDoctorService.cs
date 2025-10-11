using DNTCommon.Web.Core;
using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.Doctors.Comment.DTOs;
using DrMeet.Api.Features.Doctors.EndPoints.Comment;
using DrMeet.Api.Features.Doctors.EndPoints.DTOs;
using DrMeet.Api.Features.Doctors.EndPoints.Remote.DTOs;
using DrMeet.Api.Shared.Domian.Doctors;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.PagedList;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Api.Shared.Services.DoctorReserveTime;
using DrMeet.Api.Shared.Services.ParsizTeb.Models;
using DrMeet.Api.Shared.Services.UserService;
using DrMeet.Domain.Centers;
using ErrorOr;
using Humanizer;
using Mapster;
using Microsoft.EntityFrameworkCore;
using static DrMeet.Api.Shared.Services.ParsizTeb.Models.GetPatientByIdResponse;

namespace DrMeet.Api.Shared.Services.Doctors
{
    public interface IDoctorService : IDisposable
    {
        #region comment

        Task<ReturnUiResult> CreateCommentDoctorAsync(CreateCommentDoctorRequestDto dto, int Id, UserType userType);
        Task<ReturnUiResult> UpdateCommentDoctorAsync(UpdateCommentDoctorRequestDto dto, int Id, UserType userType);
        Task<ReturnUiResult> DeleteCommentDoctorAsync(DeleteDoctorCommentRequestDto dto, int OwnerDoctorId);
        Task<ReturnUiResult> UpdateCommentStatusDoctorAsync(UpdateStatusCommentDoctorRequestDto dto, int OwnerDoctorId);
        Task<PagedList<DoctorCommentResponseDto>> GetCommentByDoctorAsync(GetCommentsDoctorIdRequestResponseParams dto);
        #endregion
        #region Doctor
        Task<Doctor> GetDoctorById(int id);
        Task<bool> DoctorIsInCenter(int UserId, int DoctorId);
        Task<DoctorLoginRequest> LoginDoctorAsync(DoctorLoginRequestDto doctorDto);
        Task<Doctor> GetDoctorByUserId(int id);
        Task<GetDoctorProfileResponseDto> GetDoctorProfileByUserId(int id);
        Task<DoctorDetailsUserResponseDto> GetDoctorByUserDtoId(int id);
        Task<Doctor> GetDoctorByNationalCodeAsync(string NationalCode);
        Task<ReturnUiResult> UpdateSocialMediaDoctor(UpdateSocialMediaDoctorRequestDto request, int DoctorId);
        Task<ErrorOr<int>> CreateDoctorAsync(CreateDoctorRequestDto dto);
        Task<ErrorOr<int>> CreateDoctorAsync(CreateRemoteDoctorRequestDto dto);
        Task<ReturnUiResult> UpdateProfile(UpdateProfileDoctorRequestDto dto);
        Task<ReturnUiResult> AddDepartmanDoctor(UpdateDepartmanDoctorRequestDto dto);
        Task<ReturnUiResult> AddDepartmanDoctorList(UpdateDepartmanDoctorListRequestDto dto);
        Task<ReturnUiResult> UpdateWorkTypesDoctor(UpdateWorkTypesDoctorRequestDto dto);

        #endregion

    }
    public class DoctorService : IDoctorService
    {

        #region Constructor

        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IDoctorReserveTimeService _doctorReserveTimeService;

        public DoctorService(IUnitOfWork unitOfWork, IUserService userService, IDoctorReserveTimeService doctorReserveTimeService)
        {
            _unitOfWork = unitOfWork;

            _userService = userService;
            _doctorReserveTimeService = doctorReserveTimeService;
        }

        #endregion
        #region Comment

        public async Task<ReturnUiResult> CreateCommentDoctorAsync(CreateCommentDoctorRequestDto dto, int Id, UserType userType)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {


                var Doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.DoctorId);

                if (Doctor == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دکتر یافت نشد");
                    return returnUiResult;
                }
                var userInfo = await _userService.GetInfo(userType, Id);

                if (userInfo == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دسترسی ندارید");
                    return returnUiResult;
                }
                Doctor.DoctorComments.Add(new Domain.Doctors.DoctorComment()

                {

                    DoctorId = dto.DoctorId,
                    Email = userInfo.Email,
                    Name = userInfo.Name,
                    Subject = dto.Subject,
                    Text = dto.Text,
                    Score = dto.Score,
                    Status = false,
                    UserId = userInfo.UserId,
                    BehaviorScore = dto.BehaviorScore,
                    TreatmentQualityScore = dto.TreatmentQualityScore,
                    EconomicEfficiencyScore = dto.EconomicEfficiencyScore,
                    RecoveryScore = dto.RecoveryScore,

                });

                await _unitOfWork.Doctors.UpdateAsync(Doctor);

                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("ثبت نظر با موفقیت انجام شد");
                return returnUiResult;
            }
            catch (Exception)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("ثبت نظر با خطا مواجه شد");
                return returnUiResult;
            }
        }
        public async Task<ReturnUiResult> UpdateCommentDoctorAsync(UpdateCommentDoctorRequestDto dto, int Id, UserType userType)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {


                var Doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.DoctorId);

                if (Doctor == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دکتر یافت نشد");
                    return returnUiResult;
                }

                var userInfo = await _userService.GetInfo(userType, Id);


                var comment = Doctor.DoctorComments.Where(_ => _.Id.Equals(dto.Id)).FirstOrDefault();
                if (comment == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("نظر یافت نشد");
                    return returnUiResult;
                }

                if (userInfo == null || comment.UserId != userInfo.UserId)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دسترسی ندارید");
                    return returnUiResult;
                }



                comment.Email = userInfo.Email;
                comment.Name = userInfo.Name;
                comment.Subject = dto.Subject;
                comment.Text = dto.Text;
                comment.Score = dto.Score;
                comment.UserId = userInfo.UserId;
                comment.BehaviorScore = dto.BehaviorScore;
                comment.TreatmentQualityScore = dto.TreatmentQualityScore;
                comment.EconomicEfficiencyScore = dto.EconomicEfficiencyScore;
                comment.RecoveryScore = dto.RecoveryScore;



                await _unitOfWork.Doctors.UpdateAsync(Doctor);

                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("ثبت نظر با موفقیت انجام شد");
                return returnUiResult;
            }
            catch (Exception)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("ثبت نظر با خطا مواجه شد");
                return returnUiResult;
            }
        }

        public async Task<ReturnUiResult> DeleteCommentDoctorAsync(DeleteDoctorCommentRequestDto dto, int OwnerDoctorId)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                var doctor = await _unitOfWork.Doctors.GetByIdAsync(OwnerDoctorId);
                if (doctor == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دکتر یافت نشد");
                    return returnUiResult;
                }

                var Doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.DoctorId);

                if (Doctor == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دکتر یافت نشد");
                    return returnUiResult;
                }
                if (!Doctor.Id.Equals(OwnerDoctorId))
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دسترسی یافت نشد");
                    return returnUiResult;
                }

                var comment = Doctor.DoctorComments.Where(_ => _.Id.Equals(dto.CommentId)).FirstOrDefault();
                if (comment == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("نظر یافت نشد");
                    return returnUiResult;
                }

                Doctor.DoctorComments.Remove(comment);
                await _unitOfWork.Doctors.UpdateAsync(Doctor);

                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("حذف نظر با موفقیت انجام شد");
                return returnUiResult;
            }
            catch (Exception)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("ثبت نظر با خطا مواجه شد");
                return returnUiResult;
            }
        }
        public async Task<ReturnUiResult> UpdateCommentStatusDoctorAsync(UpdateStatusCommentDoctorRequestDto dto, int OwnerDoctorId)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                var doctor = await _unitOfWork.Doctors.GetByIdAsync(OwnerDoctorId);
                if (doctor == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دکتر یافت نشد");
                    return returnUiResult;
                }

                var Doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.DoctorId);

                if (Doctor == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مقاله یافت نشد");
                    return returnUiResult;
                }
                if (!Doctor.Id.Equals(OwnerDoctorId))
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دسترسی یافت نشد");
                    return returnUiResult;
                }

                var comment = Doctor.DoctorComments.Where(_ => _.Id.Equals(dto.CommentId)).FirstOrDefault();
                if (comment == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("نظر یافت نشد");
                    return returnUiResult;
                }

                comment.Status = dto.Status;



                await _unitOfWork.Doctors.UpdateAsync(Doctor);

                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("ثبت نظر با موفقیت انجام شد");
                return returnUiResult;
            }
            catch (Exception)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("ثبت نظر با خطا مواجه شد");
                return returnUiResult;
            }
        }

        public async Task<PagedList<DoctorCommentResponseDto>> GetCommentByDoctorAsync(GetCommentsDoctorIdRequestResponseParams request)
        {
            try
            {
                var response = new PagedList<DoctorCommentResponseDto>();


                var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.DoctorId);
                if (doctor == null)
                    return null;
                var result = new PagedList<DoctorCommentResponseDto>();

                result.List = doctor.DoctorComments.Select(_ => new DoctorCommentResponseDto
                {
                    Comment = new CommentDto
                    {
                        Email = _.Email,
                        Name = _.Name,
                        Subject = _.Subject,
                        Text = _.Text,
                        Score = _.Score,
                    },
                    BehaviorScore = _.BehaviorScore,
                    EconomicEfficiencyScore = _.EconomicEfficiencyScore,
                    RecoveryScore = _.RecoveryScore,
                    TreatmentQualityScore = _.TreatmentQualityScore,
                }).ToList();

                int totalPage = result.List.Count / request.PageSize.Value;
                result.Pagination = new PagedListInfo
                {
                    PageNumber = request.PageNumber.Value,
                    PageSize = request.PageSize.Value,
                    TotalCount = result.List.Count,
                    TotalPages = totalPage + 1
                };


                return result;
            }
            catch (Exception)
            {
                // ثبت لاگ
                return null;
            }

        }

        #endregion
        #region Doctor

        public async Task<ErrorOr<int>> CreateDoctorAsync(CreateDoctorRequestDto request)
        {
            try
            {

                if (await _unitOfWork.Users.AsQueryable().AnyAsync(_ => _.UserName == request.NationalCode))
                {

                    return Error.Conflict("Doctor", "این دکتر قبلا ثبت شده");
                }
           
                var salt = Hashing.GenerateSalt();
                var user = new Domain.Users.User
                {
                    FullName = request.FirstName+" " +request.LastName,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    UserName = request.NationalCode,
                    UserType = Features.Account.DTOs.UserType.Center,
                    Password = Hashing.HashPassword(request.Password, salt),
                    Salt = salt,
                };

                var doctor = new Doctor

                {

                    ExperienceYears = 12,
                    Score = 4.7f,
                    ShowPicture = true,
                    DayVisitTime = 20,
                    DoctorGroup = "قلب و عروق",
                    PhoneNumber = request.Mobile,
                  
                    Bio = "متخصص قلب با بیش از ۱۰ سال تجربه",
                    Description = "فارغ‌التحصیل دانشگاه تهران. عضو انجمن قلب اروپا.",
                    NumberMedicalSystem = "123456",
                    ShowInOnlineReserveTime = true,
                    OverFifteenYearsExperience = false,
                    WebSite = "http://doctorali.com",
                    Address = "تهران، خیابان ولیعصر، کلینیک سلامت",
                    InPerson = true,
                    PriceInPerson = 500000,
                    IsVideoConsultation = true,
                    PriceIsVideoConsultation = 400000,
                    IsPhoneConsultation = true,
                    PriceIsPhoneConsultation = 300000,
                    IsTextConsultation = false,
                    PriceIsTextConsultation = 0,
                    NationalCode = request.NationalCode,

                    User = user
                    //Centers= _center.Select(_=>new DoctorCenter { CenterId = _.Id,Name= _.Name}).ToList(),
                };
                await _unitOfWork.Doctors.AddAsync(doctor);
                return doctor.Id;
            }
            catch (Exception)
            {
                return Error.Failure("Doctor", "خطای ناشناخته رخ داده");
            }
        }
        public async Task<ErrorOr<int>> CreateDoctorAsync(CreateRemoteDoctorRequestDto request)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                if (request.RemoteDoctorId == 0)
                {

                    return Error.NotFound("Doctor", "دکتر یافت نشد");
                }
                var city = await _unitOfWork.IranCities.AsQueryable().FirstOrDefaultAsync(_ => _.Name.Equals(request.City));
                if (city is null)
                {
                    city = new Domain.Iran.IranCity
                    {
                        Name = request.City,
                 
                    };
                    await _unitOfWork.IranCities.AddAsync(city);
                }
                var provinces = await _unitOfWork.IranProvinces.AsQueryable().FirstOrDefaultAsync(_ => _.Name.Equals(request.Province));
                if (provinces is null)
                {
                    provinces = new Domain.Iran.IranProvince
                    {
                        Name = request.Province,
                
                    };
                    await _unitOfWork.IranProvinces.AddAsync(provinces);
                }

                var doctor = await _unitOfWork.Doctors.AsQueryable().FirstOrDefaultAsync(_ => _.RemoteDoctorId == request.RemoteDoctorId && _.NationalCode == request.NationalCode);
                if (doctor is null)
                {


                    doctor = new Doctor

                    {
                        RemoteDoctorId = request.RemoteDoctorId,
                        Bio = request.Bio,
                        BirthDate = request.BirthDay,

                        NumberMedicalSystem = request.NumberMedicalSystem,
                        OverFifteenYearsExperience = request.OverFifteenYearsExperience,
                        FatherName = request.FatherName,
                        WebSite = request.Website,
                        DayVisitTime = request.DayVisitTime,
                        DoctorGroup = request.DoctorGroup,

                        Gender = request.Gender,
                        PhoneNumber = request.EmergencyPhone,

                        NationalCode = request.NationalCode,


                        User = new Domain.Users.User
                        {
                            FullName = request.FirstName + " " + request.LastName,
                            Picture = string.IsNullOrEmpty(request.Picture) ? DefaultValues.DoctorManPicture : request.Picture,
                            Email = request.Email,
                            UserName = request.NationalCode,
                            UserType = Features.Account.DTOs.UserType.Doctor,
                            MobileNumber = request.Mobile,
                        },
                        CityId = city.Id,
                        ProvinceId = provinces.Id,

                        //Centers= _center.Select(_=>new DoctorCenter { CenterId = _.Id,Name= _.Name}).ToList(),
                    };
                    await _unitOfWork.Doctors.AddAsync(doctor);
                }
                else
                {
                    doctor.RemoteDoctorId = request.RemoteDoctorId;
                    doctor.Bio = string.IsNullOrEmpty(request.Bio) ? doctor.Bio : request.Bio;
                    doctor.BirthDate = string.IsNullOrEmpty(request.BirthDay) ? doctor.BirthDate : request.BirthDay;
                    doctor.User.Email = string.IsNullOrEmpty(request.Email) ? doctor.User.Email : request.Email;
                    doctor.NumberMedicalSystem = string.IsNullOrEmpty(request.NumberMedicalSystem) ? doctor.NumberMedicalSystem : request.NumberMedicalSystem;
                    doctor.OverFifteenYearsExperience = request.OverFifteenYearsExperience;
                    doctor.FatherName = string.IsNullOrEmpty(request.FatherName) ? doctor.FatherName : request.FatherName;
                    doctor.WebSite = string.IsNullOrEmpty(request.Website) ? doctor.WebSite : request.Website;
                    doctor.DayVisitTime = request.DayVisitTime == 0 ? doctor.DayVisitTime : request.DayVisitTime;
                    doctor.DoctorGroup = string.IsNullOrEmpty(request.DoctorGroup) ? doctor.DoctorGroup : request.DoctorGroup;
                    //  doctor.ExpertiseName = string.IsNullOrEmpty(request.ExpertiseName) ? doctor.ExpertiseName : request.ExpertiseName;
                    doctor.Gender = request.Gender;
                    doctor.PhoneNumber = string.IsNullOrEmpty(request.EmergencyPhone) ? doctor.PhoneNumber : request.EmergencyPhone;
                    doctor.User.Picture = string.IsNullOrEmpty(request.Picture) ? doctor.User.Picture : request.Picture;
                    doctor.NationalCode = request.NationalCode;
                    doctor.User.FullName = request.FirstName + " " + request.LastName;

                    doctor.User.MobileNumber = request.Mobile;
                    doctor.User.UserName = request.NationalCode;
                    doctor.CityId = city.Id;
                    doctor.ProvinceId = provinces.Id;
                    await _unitOfWork.Doctors.UpdateAsync(doctor);
                }


                return doctor.Id;
            }
            catch (Exception)
            {
                return Error.Failure("Doctor", "خطای ناشناخته رخ داده");
            }
        }

        public async Task<Doctor> GetDoctorByNationalCodeAsync(string nationalCode)
        {
            try
            {
                //var user = await _unitOfWork
                //    .Users
                //    .AsQueryable()
                //    //.AsNoTraking()
                //    .Where(a => a.UserName == nationalCode)
                //   .FirstOrDefaultAsync();
                var Doctor = await _unitOfWork
                   .Doctors
                   .AsQueryable()
                   //.AsNoTraking()
                   .Where(a => a.NationalCode == nationalCode)
                  .FirstOrDefaultAsync();


                if (Doctor == null)
                    return null;

                return Doctor;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Doctor> GetDoctorById(int id)
        {

            var Doctor = await _unitOfWork
                .Doctors
                .AsQueryable()
                //.AsNoTraking()
                .Where(a => a.Id == id)
               .FirstOrDefaultAsync();

            if (Doctor == null)
                return null;

            return Doctor;

        }

        public async Task<GetDoctorProfileResponseDto> GetDoctorProfileByUserId(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);

            if (doctor is null)
                return null;

            var result = doctor.Adapt<GetDoctorProfileResponseDto>();

            result.CommentCount = doctor.DoctorComments.Count;
            result.Score = new DoctorCommentScoreResponseDto { Average = doctor.DoctorComments.Count > 0 ? doctor.DoctorComments.Average(_ => _.Score) : 0, Count = doctor.DoctorComments.Count };


            var curentCenterIds = doctor.CenterDoctors
                .Select(_ => _.CenterId)
                .ToList();

            var center = await _unitOfWork.Centers
                .AsQueryable()
                .Where(_ => curentCenterIds.Contains(_.Id))
                .FirstOrDefaultAsync();
            if (center != null)
            {
                result.Address = new DoctorAddressResponseDto
                {
                    Province = await _unitOfWork.IranProvinces
                              .AsQueryable()
                              .Where(_ => _.Id == center.ProvinceId)
                              .Select(_ => _.Name)
                              .FirstOrDefaultAsync(),

                    City = await _unitOfWork.IranCities
                              .AsQueryable()
                              .Where(_ => _.Id == center.CityId)
                              .Select(_ => _.Name)
                              .FirstOrDefaultAsync(),

                    AddressLine = center?.Address,
                    Latitude = center?.CenterLocation != null ? center.CenterLocation.Location.Y : 0,
                    Longitude = center?.CenterLocation != null ? center.CenterLocation.Location.X : 0,
                };


            }



            result.ShowInOnlineReserveTime = doctor.ShowInOnlineReserveTime;

            result.OnlineVisit = await _unitOfWork.CenterDoctorServiceOnlineConsultation
                .AsQueryable()
                .Where(_ => _.DoctorId == doctor.Id)
                .Select(_ => new DoctorOnlineVisitResponseDto
                {
                    Price = _.Price,
                    ServiceName = _.ProviderServices.Name,
                    IsOnlineVisitAvailable = doctor.ShowInOnlineReserveTime,

                    //TODO 
                }).ToListAsync();
            foreach (var item in result.Centers)
            {
                var appointment = new DoctorAppointmentResponseDto();
                var rezerveTime = await _doctorReserveTimeService.GetDoctorReserveTimes(new Features.DoctorReserveTimes.DTOs.GetDoctorFreeOneTimeReserveTimeRequestDto { DoctorId = doctor.Id, CenterId = item.CenterId });
                if (rezerveTime.IsError)
                    appointment.IsAppointmentAvailable = false;
                else
                {
                    appointment.NextAvailableAppointment = rezerveTime.Value.Date.ToShortDateString();
                    appointment.IsAppointmentAvailable = true;
                }
                item.DoctorAppointment = appointment;

            }

            //result.Appointment = appointment;

            result.MonthlyVisitCount = doctor.DayVisitTime * 30;
            result.DayVisitTime = doctor.DayVisitTime;

            result.TelephoneConsultation = new DoctorTelephoneConsultationResponseDto { Price = doctor.PriceIsPhoneConsultation, IsAvailable = doctor.IsPhoneConsultation };
            result.TextConsultation = new DoctorTextConsultationResponseDto { IsAvailable = doctor.IsTextConsultation, Price = doctor.PriceIsTextConsultation };

            return result;
        }

        public async Task<Doctor> GetDoctorByUserId(int id)
        {

            var Doctor = await _unitOfWork
                .Doctors
                .AsQueryable()
                //.AsNoTraking()
                .Where(a => a.UserId == id)
               .FirstOrDefaultAsync();

            if (Doctor == null)
                return null;

            return Doctor;

        }
        public async Task<bool> DoctorIsInCenter(int UserId, int DoctorId)
        {
            var centerId = await _unitOfWork
              .Centers
              .AsQueryable()
              //.AsNoTraking()
              .Where(a => a.CenterUser.Select(_ => _.UserId).Contains(UserId))
              .Select(_ => _.Id)
             .FirstOrDefaultAsync();

            return _unitOfWork
                .Doctors
                .AsQueryable()
                //.AsNoTraking()
                .Any(a => a.CenterDoctors.Any(_ => _.CenterId == centerId) && a.Id == DoctorId);



        }

        public async Task<DoctorDetailsUserResponseDto> GetDoctorByUserDtoId(int id)
        {
            var model = new DoctorDetailsUserResponseDto();
            var Doctor = await _unitOfWork
              .Doctors
              .AsQueryable()
              //.AsNoTraking()
              .Where(a => a.Id == id)
              .Select(_ => new
              {
                  LastName = _.User.LastName,
                  NationalCode = _.NationalCode,
                  FirstName = _.User.FirstName,
                  Id = _.Id,
                  Mobile = _.User.MobileNumber,
                  FatherName = _.FatherName,
                  BirthDay = _.BirthDate,
                  UserName = _.User.UserName,
                  Picture = _.User.Picture,
                  Email = _.User.Email,
              })
             .FirstOrDefaultAsync();

            if (Doctor == null)
                return null;

            model.PersonalInfo = Doctor;
            model.Centers = await _unitOfWork
              .Doctors
              .AsQueryable()
              //.AsNoTraking()
              .Where(a => a.Id == id)
              .SelectMany(_ => _.CenterDoctors).Select(_ => new CenterResponseDto { Id = _.Center.Id, Name = _.Center.Name }).ToListAsync();
            return model;
        }


        public async Task<DoctorLoginRequest> LoginDoctorAsync(DoctorLoginRequestDto doctorDto)
        {
            var doctor = await _unitOfWork.Doctors.AsQueryable().Where(_ => _.User.UserName == doctorDto.Username).FirstOrDefaultAsync();
            if (doctor == null)
                return null;
            var user = await _userService.GetUser(doctor.UserId);
            if (doctor == null) return null;
            var IsValidPassord = Hashing.ComparePassword(user.Password, doctorDto.Password, user.Salt) ? new DoctorLoginRequest() { Id = doctor.Id } : null;
            return IsValidPassord;

        }
        public async Task<ReturnUiResult> UpdateProfile(UpdateProfileDoctorRequestDto dto)
        {
            //await _unitOfWork.IranCities.AddAsync(new Features.Iran.Entities.IranCity { Name = "sari" });
            //await _unitOfWork.IranCities.AddAsync(new Features.Iran.Entities.IranCity { Name = "mazandaran" });
            var returnUiResult = new ReturnUiResult();
            try
            {

               
                var ListNewCenters = new List<CenterDoctorsSelected>();

                //foreach (var item in dto.CenterId[0].Split(",").ToList() ?? new List<string>())
                //{
                //    if (await _unitOfWork.Centers.AsQueryable().Where(_ => _.Id.Equals(item)).AnyAsync())
                //    {
                //        ListNewCenters.Add(new CenterDoctorsSelected { CenterId = item, });
                //    }

                //}

                var doctor = await GetDoctorById(dto.Id.Value);
                if (doctor == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دکتر یافت نشد");
                    return returnUiResult;
                }
                var user = await _unitOfWork.Users.GetByIdAsync(doctor.UserId);
                if (user == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("کاربر یافت نشد");
                    return returnUiResult;
                }
                if (dto.LastName != null && !string.IsNullOrEmpty(dto.LastName))
                    doctor.User.LastName = dto.LastName!;

                if (dto.FirstName != null && !string.IsNullOrEmpty(dto.FirstName))
                    doctor.User.FirstName = dto.FirstName!;


                if (dto.FatherName != null && !string.IsNullOrEmpty(dto.FatherName))
                    doctor.FatherName = dto.FatherName!;

                if (dto.Bio != null && !string.IsNullOrEmpty(dto.Bio))
                    doctor.Bio = dto.Bio!;

                if (dto.Descrption != null && !string.IsNullOrEmpty(dto.Descrption))
                    doctor.Description = dto.Descrption!;

                if (dto.ProvinceId != null && dto.ProvinceId.HasValue)
                {
                    if (await _unitOfWork.IranProvinces.AsQueryable().AnyAsync(_ => _.Id.Equals(dto.ProvinceId)))
                        doctor.ProvinceId = dto.ProvinceId!;
                }
                if (dto.ExpertiseId != null && dto.ExpertiseId.HasValue)
                {
                    if (await _unitOfWork.Expertise.AsQueryable().AnyAsync(_ => _.Id.Equals(dto.ExpertiseId)))
                    {
                        doctor.DoctorExpertises.Add(new Domain.Doctors.DoctorExpertise
                        {
                            ExpertiseId = dto.ExpertiseId.Value!,
                        });
                    }
                  
                }

                if (dto.CityId != null && (dto.CityId).HasValue)
                {
                    if (await _unitOfWork.IranCities.AsQueryable().AnyAsync(_ => _.Id.Equals(dto.CityId)))
                        doctor.CityId = dto.CityId!;
                }


                if (dto.Region != null && !string.IsNullOrEmpty(dto.Region))
                    doctor.Region = dto.Region!;

                if (dto.BirthDate != null && !string.IsNullOrEmpty(dto.BirthDate))
                    doctor.BirthDate = dto.BirthDate!;

                if (dto.Over15YearsOfExperience != null)
                    doctor.OverFifteenYearsExperience = dto.Over15YearsOfExperience.Value;

                if (dto.Email != null && !string.IsNullOrEmpty(dto.Email))
                    doctor.User.Email = dto.Email!;

                if (dto.WebSite != null && !string.IsNullOrEmpty(dto.WebSite))
                    doctor.WebSite = dto.WebSite!;

                if (dto.InPerson != null)
                    doctor.InPerson = dto.InPerson!;

                if (dto.IsPhoneConsultation != null)
                    doctor.IsPhoneConsultation = dto.IsPhoneConsultation!;

                if (dto.IsVideoConsultation != null)
                    doctor.IsVideoConsultation = dto.IsVideoConsultation!;
                //if (dto.ExpertiseName != null && !string.IsNullOrEmpty(dto.ExpertiseName))
                //    doctor.ExpertiseName = dto.ExpertiseName!;

                if (dto.NumberOfMedicalSystem != null && !string.IsNullOrEmpty(dto.NumberOfMedicalSystem))
                    doctor.NumberMedicalSystem = dto.NumberOfMedicalSystem!;

                //if (!string.IsNullOrEmpty(dto.Password))
                //    doctor.Password = Hashing.HashPassword(dto.Password, doctor.Salt);


                if ((dto.ExpertiseId).HasValue)
                {
                    if (await _unitOfWork.Expertise.AsQueryable().AnyAsync(_ => _.Id.Equals(dto.ExpertiseId)))
                    {

                        doctor.DoctorExpertises.Add(new Domain.Doctors.DoctorExpertise
                        {
                            ExpertiseId = dto.ExpertiseId.Value!,
                        });
                    }

                }
                //if (ListCenters != null)
                //{

                //    doctor.CenterDoctors = ListNewCenters;
                //}

                if (dto.Picture != null)
                {
                    await FileUploadManager.DeleteAsync(doctor.User.Picture!);
                    doctor.User.Picture = await FileUploadManager.UploadAsync(dto.Picture!, FolderImagesType.Doctor);
                }
                await _unitOfWork.Doctors.UpdateAsync(doctor);
                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("دکتر با موفقیت آپدیت شد");
                return returnUiResult;
            }
            catch (Exception)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا در بروزرسانی دکتر ایجاد شد");
                return returnUiResult;
            }

        }




        public async Task<ReturnUiResult> AddDepartmanDoctor(UpdateDepartmanDoctorRequestDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {

                var doctor = await GetDoctorById(dto.DoctorId);
                if (doctor == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دکتر یافت نشد");
                    return returnUiResult;
                }

                var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterDepartment.CenterId);
                if (center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
                var model =
                    new Domain.Centers.CenterDoctorsSelected
                    {
                        CenterId = center.Id,

                        CenterDoctorsDepartmant = center.CenterDepartment.Where(a => a.Id.Equals(dto.CenterDepartment.DepartmentId)).Select(_ =>
                new CenterDoctorsDepartmantSelected
                {
                    //CenterId = center.Id,
                    CenterDepartmentId = dto.CenterDepartment.DepartmentId,
                    CenterDoctorsSelectedId = doctor.Id,

                }).ToList()
                    };
                doctor.CenterDoctors.Add(model);
                await _unitOfWork.Doctors.UpdateAsync(doctor);
                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("دکتر با موفقیت آپدیت شد");
                return returnUiResult;

            }
            catch (Exception)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا در بروزرسانی دکتر ایجاد شد");
                return returnUiResult;
            }
        }
        public async Task<ReturnUiResult> UpdateWorkTypesDoctor(UpdateWorkTypesDoctorRequestDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {

                var doctor = await GetDoctorById(dto.DoctorId);
                if (doctor == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دکتر یافت نشد");
                    return returnUiResult;
                }

                doctor.InPerson = dto.InPerson;
                doctor.PriceInPerson = dto.PriceInPerson;

                doctor.IsPhoneConsultation = dto.IsPhoneConsultation;
                doctor.PriceIsPhoneConsultation = dto.PriceIsPhoneConsultation;

                doctor.IsVideoConsultation = dto.IsVideoConsultation;
                doctor.PriceIsVideoConsultation = dto.PriceIsVideoConsultation;


                doctor.IsTextConsultation = dto.IsTextConsultation;
                doctor.PriceIsTextConsultation = dto.PriceIsTextConsultation;


                await _unitOfWork.Doctors.UpdateAsync(doctor);
                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("دکتر با موفقیت آپدیت شد");
                return returnUiResult;

            }
            catch (Exception)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا در بروزرسانی دکتر ایجاد شد");
                return returnUiResult;
            }
        }

        public async Task<ReturnUiResult> AddDepartmanDoctorList(UpdateDepartmanDoctorListRequestDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {

                foreach (var DoctorId in dto.DoctorIds)
                {
                    var doctor = await GetDoctorById(DoctorId);
                    if (doctor == null)
                    {
                        returnUiResult.ReturnResult = ReturnResult.Error;
                        returnUiResult.LstMessage.Add("دکتر یافت نشد");
                        return returnUiResult;
                    }

                    var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterDepartment.CenterId);
                    if (center == null)
                    {
                        returnUiResult.ReturnResult = ReturnResult.Error;
                        returnUiResult.LstMessage.Add("مرکز یافت نشد");
                        return returnUiResult;
                    }
                    var model =
                        new CenterDoctorsSelected
                        {
                            CenterId = center.Id,
                            DoctorId = doctor.Id,
                            CenterDoctorsDepartmant = center.CenterDepartment.Where(a => a.Id.Equals(dto.CenterDepartment.DepartmentId)).Select(_ =>
                    new CenterDoctorsDepartmantSelected
                    {
                        //CenterId = center.Id,
                        CenterDepartmentId = dto.CenterDepartment.DepartmentId,
                        CenterDoctorsSelectedId = doctor.Id,
                    }).ToList()
                        };
                    if (!doctor.CenterDoctors.Where(_ => _.CenterId == dto.CenterDepartment.CenterId).Any())
                        doctor.CenterDoctors.Add(model);
                    await _unitOfWork.Doctors.UpdateAsync(doctor);
                }


                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("دکتر با موفقیت آپدیت شد");
                return returnUiResult;

            }
            catch (Exception)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا در بروزرسانی دکتر ایجاد شد");
                return returnUiResult;
            }
        }

        public async Task<ReturnUiResult> UpdateSocialMediaDoctor(UpdateSocialMediaDoctorRequestDto request, int DoctorId)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                var doctor = await GetDoctorById(DoctorId);
                if (doctor == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دکتر یافت نشد");
                    return returnUiResult;
                }
                int SocialMediaAccountId = 1;
                doctor.DoctorSocialMediaAccounts = request.SocialMediaAccount.Select(_ =>
                new DoctorSocialMediaAccount
                {
                    DoctorId = DoctorId,

                    UsernameOrUrl = _.UsernameOrUrl,
                    Platform = _.Platform

                }).ToList();
                await _unitOfWork.Doctors.UpdateAsync(doctor);
                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("دکتر با موفقیت آپدیت شد");
                return returnUiResult;
            }
            catch (Exception)
            {

                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا در بروزرسانی دکتر ایجاد شد");
                return returnUiResult;
            }

        }

        #endregion

        #region Dispose 

        void IDisposable.Dispose()
        {
            if (_unitOfWork != null)
                _unitOfWork.Doctors.TryDisposeSafe();
        }




        #endregion

    }
}
