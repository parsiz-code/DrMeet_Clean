using DNTCommon.Web.Core;
using DrMeet.Api.Features.Account.DTOs;

using DrMeet.Api.Features.Patients.DTOs;
using DrMeet.Api.Features.Patients.DTOs;
using DrMeet.Api.Shared.Domian;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Api.Shared.Services.ParsizTeb.Models;
using DrMeet.Api.Shared.Services.UserService;
using DrMeet.Domain.Patients;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

using static DrMeet.Api.Shared.Services.ParsizTeb.Models.GetPatientByIdResponse;

namespace DrMeet.Api.Shared.Services.Patients
{
    public interface IPatientService : IDisposable
    {

        #region Patient
        Task<DetailsUserResponseDto> GetPatientDtoByRemoteId(int id);
        Task<Patient> GetPatientById(int id);
        Task<DetailsUserResponseDto> GetPatientDtoById(int id);
        Task<ErrorOr<int>> CreatePatientAsync(CreatePatientRequestDto dto);
        Task<ErrorOr<PatientCustomLoginRequest>> LoginPatientAsync(PatientLoginRequestDto PatientDto);

        Task<Patient> GetPatientByNationalCodeAsync(string code);


        Task<ReturnUiResult> EditPatient(UpdatePatientRequestDto dto);

        Task<ReturnUiResult> DeletePatient(int PatientId);

        Task<ReturnUiResult> EditPatientGlobal(UpdatePatientGlobalRequestDto dto, int patientId);
        #endregion

    }
    public class PatientService : IPatientService
    {

        #region Constructor

        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public PatientService(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;

            _userService = userService;
        }

        #endregion

        #region Patient

        public async Task<ErrorOr<int>> CreatePatientAsync(CreatePatientRequestDto request)
        {
            try
            {
                if (await _unitOfWork.Patients.AsQueryable().AnyAsync(_ => _.NationalCode == request.NationalCode &&
                _.CenterPatientSelected.Select(c => c.CenterId).Equals(request.CenterId)))
                {

                    return Error.Conflict("Doctor", "این بیمار قبلا ثبت شده");
                }
                if (!await _unitOfWork.Centers.AsQueryable().AnyAsync(_ => _.Id == request.CenterId))
                {

                    return Error.Conflict("Doctor", "مرکزیافت نشد");
                }
                var userId = await _userService.CreateUser(
                    new Features.Account.DTOs.CreateUserRequestDto()
                    {
                        Password = request.Password,
                        UserName = request.NationalCode,
                        UserType = Features.Account.DTOs.UserType.Patient,
                        //NationalCode = request.NationalCode,
                        //FirstName = request.FirstName,
                        //LastName = request.LastName,
                        //Mobile = request.Mobile,
                    });

                var Patient = new Patient

                {
                    NationalCode = request.NationalCode,
                   
                    UserId = userId,
                  
                    Picture=DefaultValues.PatientManPicture,
                    User = new Domain.Users.User
                    {
                        Id = userId,
                        Password = null,
                        UserName = request.NationalCode,
                        UserType = Features.Account.DTOs.UserType.Patient,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        MobileNumber=request.Mobile,
                        
                        
                    },
                    CenterPatientSelected=new List<CenterPatientSelected>
                    {
                        new CenterPatientSelected{CenterId=request.CenterId}
                    }

                    //   PhoneNumber = request.Mobile,

                };

                await _unitOfWork.Patients.AddAsync(Patient);
                return Patient.Id;
            }
            catch (Exception)
            {
                return Error.Failure("Doctor", "خطای ناشناخته رخ داد");
            }
        }


        public async Task<ReturnUiResult> EditPatient(UpdatePatientRequestDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                //await _unitOfWork.IranCities.AddAsync(new Features.Iran.Entities.IranCity { Name = "sari" });
                //await _unitOfWork.IranProvinces.AddAsync(new Features.Iran.Entities.IranProvince { Name = "mazandaran" });




                var Patient = await _unitOfWork.Patients.GetByIdAsync(dto.Id);
                if (Patient == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("بیمار یافت نشد");
                    return returnUiResult;
                }
                var User = await _unitOfWork.Users.GetByIdAsync(Patient.UserId);
                if (User == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("بیمار یافت نشد");
                    return returnUiResult;
                }

                Patient.User.FirstName = dto.FirstName;
                Patient.User.LastName = dto.LastName;
                Patient.User.MobileNumber = dto.Mobile;
                Patient.NationalCode = dto.NationalCode;
                Patient.User.UserName = dto.NationalCode;
                User.UserName = dto.NationalCode;


                await _unitOfWork.Patients.UpdateAsync(Patient);
                await _unitOfWork.Users.UpdateAsync(User);
                // await _unitOfWork.SaveChange();

                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("بیمار با موفقیت بروزرسانی شد");
                return returnUiResult;
            }
            catch (Exception)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا رخ داد");
                return returnUiResult;
            }
        }

        public async Task<ReturnUiResult> EditPatientGlobal(UpdatePatientGlobalRequestDto dto, int patientId)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                var Patient = await _unitOfWork.Patients.GetByIdAsync(patientId);
                if (Patient == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("بیمار یافت نشد");
                    return returnUiResult;
                }
                var User = await _unitOfWork.Users.GetByIdAsync(Patient.UserId);
                if (User == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("بیمار یافت نشد");
                    return returnUiResult;
                }
                if(!await _unitOfWork.Insurances.AsQueryable().AnyAsync(_=>_.IsBaseInsurance&&_.Id==dto.InsuranceId))
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("بیمه پایه یافت نشد");
                    return returnUiResult;
                }
                if (!await _unitOfWork.Insurances.AsQueryable().AnyAsync(_ => _.IsBaseInsurance==false && _.Id == dto.SupplementInsuranceId))
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("بیمه تکمیلی یافت نشد");
                    return returnUiResult;
                }
                Patient.SupplementInsuranceId = dto.SupplementInsuranceId;
                Patient.InsuranceId = dto.InsuranceId;
                Patient.User.FirstName = dto.FirstName;
                Patient.User.LastName = dto.LastName;
                Patient .User.MobileNumber = dto.Mobile;
                Patient.User.Email = dto.Email;
                Patient.Picture = dto.Picture;
                Patient.BirthDate = dto.BirthDay;
                Patient.NationalCode = dto.NationalCode;
                Patient.User.UserName = dto.NationalCode;
                User.UserName = dto.NationalCode;


                await _unitOfWork.Patients.UpdateAsync(Patient);
                await _unitOfWork.Users.UpdateAsync(User);
                // await _unitOfWork.SaveChange();

                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("بیمار با موفقیت بروزرسانی شد");
                return returnUiResult;
            }
            catch (Exception)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا رخ داد");
                return returnUiResult;
            }
        }
        public async Task<ReturnUiResult> DeletePatient(int PatientId)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                var Patient = await _unitOfWork
                    .Patients
                    .AsQueryable()
                    .FirstOrDefaultAsync(s => s.Id == PatientId);

                if (Patient == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("بیمار یافت نشد");
                    return returnUiResult;
                }

                await _unitOfWork.Patients.DeleteAsync(PatientId);

                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("بیمار با موفقیت حذف شد ");
                return returnUiResult;
            }
            catch (Exception)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("مرکز یافت نشد");
                return returnUiResult;
            }
        }
        public async Task<Patient> GetPatientByNationalCodeAsync(string nationalCode)
        {
            try
            {
                //var user = await _unitOfWork.Users.AsQueryable().Where(_ => _.NationalCode.Equals(nationalCode)).FirstOrDefaultAsync();

                var Patient = await _unitOfWork
                    .Patients

                    .AsQueryable()
                    //.AsNoTraking()
                    .Where(a => a.NationalCode == nationalCode)
                   .FirstOrDefaultAsync();

                if (Patient == null)
                    return null;

                return Patient;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Patient> GetPatientById(int id)
        {

            var Patient = await _unitOfWork
                .Patients
                .AsQueryable()
                //.AsNoTraking()
                .Where(a => a.Id == id)

               .FirstOrDefaultAsync();
            Patient.User.Salt = null;
            Patient.User.Password = null;
            if (Patient == null)
                return null;

            return Patient;

        }

        public async Task<DetailsUserResponseDto> GetPatientDtoById(int id)
        {
            var model = new DetailsUserResponseDto();
            var patient = await _unitOfWork
                .Patients.GetByIdAsync(id);
               

            if (patient == null)
                return null;

            model.PersonalInfo = new PatientDetailsDto
            {
                LastName = patient.User.LastName,
                NationalCode = patient.NationalCode,
                FirstName = patient.User.FirstName,
                Id = patient.Id,
                Mobile = patient.User.MobileNumber,
                FatherName = patient.FatherName,
                BirthDay = patient.BirthDate,
                UserName = patient.User.UserName,
                Email = patient.User.Email,

                Picture = patient.Picture
            };


                model.Centers.AddRange(patient.CenterPatientSelected.Select(_=>new CenterResponseDto
                {
                     Id = _.Center.Id,
                     Name=_.Center.Name
                }));
            return model;

        }

        public async Task<DetailsUserResponseDto> GetPatientDtoByRemoteId(int id)
        {
            var model = new DetailsUserResponseDto();
            var patient = await _unitOfWork.Patients
                .AsQueryable()

                .Include(_ => _.CenterPatientSelected)
                .ThenInclude(_ => _.Center)
                .Where(_=>_.PatientRemoteId == id)
                .FirstOrDefaultAsync();


            if (patient == null)
                return null;

            model.PersonalInfo = new PatientDetailsDto
            {
                LastName = patient.User.LastName,
                NationalCode = patient.NationalCode,
                FirstName = patient.User.FirstName,
                Id = patient.Id,
                Mobile = patient.User.MobileNumber,
                FatherName = patient.FatherName,
                BirthDay = patient.BirthDate,
                UserName = patient.User.UserName,
                Email = patient.User.Email,

                Picture = patient.Picture
            };


            model.Centers.AddRange(patient.CenterPatientSelected.Select(_ => new CenterResponseDto
            {
                Id = _.Center.Id,
                Name = _.Center.Name
            }));
            return model;

        }
        public async Task<ErrorOr<PatientCustomLoginRequest>> LoginPatientAsync(PatientLoginRequestDto PatientDto)
        {
            var Patient = await GetPatientByNationalCodeAsync(PatientDto.Username);


            if (Patient == null) return Error.NotFound("Doctor", "این بیماردرسامانه ثبت نشده");
            var user = await _userService.GetUser(Patient.UserId);
            var IsValidPassord = Hashing.ComparePassword(user.Password, PatientDto.Password, user.Salt) ? new PatientCustomLoginRequest() { Id = Patient.Id } : null;
            return IsValidPassord;

        }

        #endregion

        #region Dispose 

        void IDisposable.Dispose()
        {
            if (_unitOfWork != null)
                _unitOfWork.Patients.TryDisposeSafe();
        }

        #endregion

    }
}
