using DNTCommon.Web.Core;
using DNTPersianUtils.Core;
using DNTPersianUtils.Core.IranCities;
using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.Blogs.Comment.DTOs;
using DrMeet.Api.Features.Centers.Comment.DTOs;
using DrMeet.Api.Features.Centers.DTOs;
using DrMeet.Api.Features.Centers.QuestionAnswer.DTOs;
using DrMeet.Api.Features.DoctorReserveTimes.DTOs;
using DrMeet.Api.Features.Doctors.Comment.DTOs;
using DrMeet.Api.Features.Doctors.EndPoints.DTOs;
using DrMeet.Api.Features.DoctorsShifts.EndPoints.DTOs;
using DrMeet.Api.Features.Insurances.DTOs;
using DrMeet.Api.Features.ServicesAvailables.DTOs;
using DrMeet.Api.Shared.Domian;

using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.PagedList;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Api.Shared.Services.ParsizTeb.Models;
using DrMeet.Api.Shared.Services.ServicesAvailable;
using DrMeet.Api.Shared.Services.UserService;
using DrMeet.Domain.Centers;
using ErrorOr;
using Humanizer;
using iTextSharp.text;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;


using NetTopologySuite.Geometries;

//using Parbad.Internal;
using SkiaSharp;
using System;
using System.Linq;
using System.Xml.Linq;
using static DrMeet.Api.Features.Doctors.EndPoints.DTOs.GetDoctorDetailDto;
using static DrMeet.Api.Shared.Services.ParsizTeb.Models.GetPatientByIdResponse;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Error = ErrorOr.Error;

namespace DrMeet.Api.Shared.Services.Centers
{
    public interface ICenterService : IDisposable
    {
        #region QuestionAnswer
        Task<ReturnUiResult> DeleteCreateCenterQuestionAnswerAsyncAsync(DeleteCenterQuestionAnswerDto dto, int CenterId);
        Task<ReturnUiResult> UpdateCommentStatusCenterAsync(UpdateStatusCenterQuestionAnswerDto dto, int CenterId);
        #endregion

        #region comment
        Task<ReturnUiResult> CreateCommentCenterAsync(CreateCommentCenterDto dto, int Id, UserType userType);
        Task<ReturnUiResult> UpdateCommentCenterAsync(UpdateCommentCenterDto dto, int Id, UserType userType);
        Task<ReturnUiResult> DeleteCommentCenterAsync(DeleteCenterCommentDto dto, int DoctorId);
        Task<ReturnUiResult> UpdateCommentStatusCenterAsync(UpdateStatusCommentCenterDto dto, int DoctorId);

        Task<PagedList<CenterCommentDto>> GetCommentByCenterAsync(GetCommentsCenterIdResponseParams dto);
        #endregion
        #region Center
        #region Update
        Task<ReturnUiResult> UpdateCenterInsurancesAsync(ToggleCenterInsurancesDto dto);
        Task<ReturnUiResult> DeleteCenterInsurancesAsync(DeleteCenterInsurancesDto
            dto);
        Task<ReturnUiResult> UpdateCenterServicesAvailableAsync(ToggleCenterServicesAvailableDto dto);
        Task<ReturnUiResult> DeleteCenterServicesAvailableAsync(ToggleCenterServicesAvailableDto dto);
        Task<ReturnUiResult> UpdateCenterLicensesAsync(UpdateCenterLicensesDto dto);
        Task<ReturnUiResult> UpdateCenterSupplementalInsurancesAsync(UpdateCenterSupplementalInsurancesDto dto);
        Task<ReturnUiResult> UpdateCenterContractedBasicInsurancesAsync(UpdateCenterContractedBasicInsurancesDto dto);
        Task<ReturnUiResult> UpdateCenterServicesAvailableAsync(UpdateCenterServicesAvailableDto dto);
        Task<ReturnUiResult> UpdateCenterServicesAvailableAsync(UpdateDoctorCenterServicesAvailableDto dto);
        Task<ReturnUiResult> UpdateCenterServicesAvailableAsync(ToggleDoctorCenterServicesAvailableDto dto);
        Task<ReturnUiResult> DeleteDoctorCenterServicesAvailableAsync(ToggleDoctorCenterServicesAvailableDto dto);
        Task<ReturnUiResult> UpdateCenterDepartmentAsync(CreateCenterDepartmentDto dto);
        Task<ReturnUiResult> UpdateCenterDepartmentAsync(ToggleCenterDepartmentDto dto);
        Task<ReturnUiResult> UpdateCenterPictureAsync(UpdateCenterPictureDto dto);
        Task<ReturnUiResult> UpdateCenterGeneralAsync(UpdateGeneralCenterDto dto);

        #endregion
        #region Center Departmant


        Task<ReturnUiResult> ToogleStatusDoctorCenterAsync(ToggleDoctorCenterDto dto);
        Task<ReturnUiResult> AddDoctorCenterAsync(AddDoctorCenterDto dto);

        Task<ReturnUiResult> AddDoctorCenterAsync(ToggleDoctorCenterDto dto);
        Task<ReturnUiResult> DeleteDoctorCenterAsync(AddDoctorCenterDto dto);

        Task<ReturnUiResult> DeleteDoctorCenterAsync(ToggleDoctorCenterDto dto);
        Task<PagedList<DepartmentCenterDto>> GetDepartmantAsync(GetDoctorCenterDepartmantRequest dto);
        #endregion

        Task<PagedList<GetDoctorCenterDetailDto>> GetCenterDoctor(GetDoctorCenterResponseParams request);

        #region GetCenter
        Task<GetCenterDetailDto> GetCenterDetailAsync(int CenterId);
        Task<GetCenterDetailResponseDto> GetCenterDetails(int CenterId);
        Task<CenterPictureDtoResponseDto> GetCenterPicture(int CenterId);
        Task<CenterServicesAvailableDtoResponseDto> GetCenterServicesAvailable(int CenterId, CenterServicesAvailableRequestDto request);
        Task<PagedList<GetInsuranceDetailResponseDto>> GetCenterContractedBasicInsurances(int CenterId, CenterContractedInsurancesRequestDto request);
        Task<PagedList<GetInsuranceDetailResponseDto>> GetCenterContractedSupplementalInsurances(int CenterId, CenterContractedInsurancesRequestDto request);
        Task<CenterDepartmentResponseDto> GetCenterDepartmen(int CenterId);

        Task<PagedList<GetCenterDto>> GetCenters(GetCenterResponseParams request);
        Task<PagedList<GetCenterDto>> GetCenters(NearbyLocationDto request);
        #endregion



        Task<ErrorOr<(int userId, int centerId)>> CreateCenterAsync(CreateCenterDto dto);
        Task<ReturnUiResult> CreateCenterQuestionAnswerAsync(CreateCenterQuestionAnswerDto dto);


        Task<ReturnUiResult> EditCenter(EditLocationCenterDto dto);

        Task<ReturnUiResult> DeleteCenter(int CenterId);
        Task<CenterLoginRequest> LoginCenterAsync(CenterLoginDto centerDto);

        #endregion

    }
    public class CenterService : ICenterService
    {

        #region Constructor
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServicesAvailableService _servicesAvailableService;

        public CenterService(IUnitOfWork unitOfWork, IUserService userService, IServicesAvailableService servicesAvailableService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _servicesAvailableService = servicesAvailableService;
        }

        #endregion

        #region QuestionAnswer


        public async Task<ReturnUiResult> CreateCenterQuestionAnswerAsync(CreateCenterQuestionAnswerDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);
            if (center == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("مرکز یافت نشد");
                return returnUiResult;
            }
            int CenterQuestionAnswerId = 1;
            int negativePointsId = 1;
            int positivePointsId = 1;
            center.CenterQuestionAnswer.AddRange(dto.QuestionAnswer.Select(_ => new CenterQuestionAnswer
            {

                CreateDate = DateTime.Now,
                NegativePoints = _.NegativePoints.Select(n => new CenterQuestionAnswerCommentPoints { Message = n.Message }).ToList(),
                PositivePoints = _.PositivePoints.Select(n => new CenterQuestionAnswerCommentPoints { Message = n.Message }).ToList(),
                Id = CenterQuestionAnswerId++,
                Text = _.Text,
                Type = _.Type,
                Status = true,
                ParentId = _.ParentId.HasValue ? _.ParentId.Value : null,
                CenterId = _.CenterId

            }));

            await _unitOfWork.Centers.UpdateAsync(center);
            // await _unitOfWork.SaveChange();

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("مرکز با موفقیت بروزرسانی شد");

            return returnUiResult;
        }


        public async Task<ReturnUiResult> DeleteCreateCenterQuestionAnswerAsyncAsync(DeleteCenterQuestionAnswerDto dto, int CenterId)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                var doctor = await _unitOfWork.Doctors.GetByIdAsync(CenterId);
                if (doctor == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }

                var Center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);

                if (Center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
                //if (!doctor.Id.Equals(DoctorId))
                //{
                //    returnUiResult.ReturnResult = ReturnResult.Error;
                //    returnUiResult.LstMessage.Add("دسترسی یافت نشد");
                //    return returnUiResult;
                //}

                var questionAnswer = Center.CenterComment.Where(_ => _.Id.Equals(dto.QuestionAnswerId)).FirstOrDefault();
                if (questionAnswer == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("پرسشی یافت نشد");
                    return returnUiResult;
                }

                Center.CenterComment.Remove(questionAnswer);
                await _unitOfWork.Centers.UpdateAsync(Center);

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
        public async Task<ReturnUiResult> UpdateCommentStatusCenterAsync(UpdateStatusCenterQuestionAnswerDto dto, int CenterId)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                var doctor = await _unitOfWork.Centers.GetByIdAsync(CenterId);
                if (doctor == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }

                var Center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);

                if (Center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
                //if (!Center.DoctorId.Equals(DoctorId))
                //{
                //    returnUiResult.ReturnResult = ReturnResult.Error;
                //    returnUiResult.LstMessage.Add("دسترسی یافت نشد");
                //    return returnUiResult;
                //}

                var questionAnswer = Center.CenterQuestionAnswer.Where(_ => _.Id.Equals(dto.QuestionAnswerId)).FirstOrDefault();
                if (questionAnswer == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("نظر یافت نشد");
                    return returnUiResult;
                }

                questionAnswer.Status = dto.Status;



                await _unitOfWork.Centers.UpdateAsync(Center);

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

        #endregion

        #region Comment

        public async Task<ReturnUiResult> CreateCommentCenterAsync(CreateCommentCenterDto dto, int Id, UserType userType)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {


                var Center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);

                if (Center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
                var userInfo = await _userService.GetInfo(userType, Id);

                if (userInfo == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دسترسی ندارید");
                    return returnUiResult;
                }


                Center.CenterComment.Add(new CenterComment
                {
                    Id = Center.CenterComment.Count + 1,
                    Email = userInfo.Email,
                    Name = userInfo.Name,
                    Subject = dto.Subject,
                    Text = dto.Text,
                    Score = dto.Score,
                    Status = false,
                    UserId = userInfo.UserId,
                    CenterId = dto.CenterId,
                });

                await _unitOfWork.Centers.UpdateAsync(Center);

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
        public async Task<ReturnUiResult> UpdateCommentCenterAsync(UpdateCommentCenterDto dto, int Id, UserType userType)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {


                var Center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);

                if (Center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
                var comment = Center.CenterComment.Where(_ => _.Id.Equals(dto.Id)).FirstOrDefault();
                if (comment == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("نظر یافت نشد");
                    return returnUiResult;
                }
                var userInfo = await _userService.GetInfo(userType, Id);

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



                await _unitOfWork.Centers.UpdateAsync(Center);

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

        public async Task<ReturnUiResult> DeleteCommentCenterAsync(DeleteCenterCommentDto dto, int DoctorId)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                var doctor = await _unitOfWork.Doctors.GetByIdAsync(DoctorId);
                if (doctor == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دکتر یافت نشد");
                    return returnUiResult;
                }

                var Center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);

                if (Center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
                //if (!doctor.Id.Equals(DoctorId))
                //{
                //    returnUiResult.ReturnResult = ReturnResult.Error;
                //    returnUiResult.LstMessage.Add("دسترسی یافت نشد");
                //    return returnUiResult;
                //}

                var comment = Center.CenterComment.Where(_ => _.Id.Equals(dto.CommentId)).FirstOrDefault();
                if (comment == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("نظر یافت نشد");
                    return returnUiResult;
                }

                Center.CenterComment.Remove(comment);
                await _unitOfWork.Centers.UpdateAsync(Center);

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
        public async Task<ReturnUiResult> UpdateCommentStatusCenterAsync(UpdateStatusCommentCenterDto dto, int DoctorId)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                var doctor = await _unitOfWork.Doctors.GetByIdAsync(DoctorId);
                if (doctor == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دکتر یافت نشد");
                    return returnUiResult;
                }

                var Center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);

                if (Center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
                //if (!Center.DoctorId.Equals(DoctorId))
                //{
                //    returnUiResult.ReturnResult = ReturnResult.Error;
                //    returnUiResult.LstMessage.Add("دسترسی یافت نشد");
                //    return returnUiResult;
                //}

                var comment = Center.CenterComment.Where(_ => _.Id.Equals(dto.CommentId)).FirstOrDefault();
                if (comment == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("نظر یافت نشد");
                    return returnUiResult;
                }

                comment.Status = dto.Status;



                await _unitOfWork.Centers.UpdateAsync(Center);

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

        public async Task<PagedList<CenterCommentDto>> GetCommentByCenterAsync(GetCommentsCenterIdResponseParams request)
        {
            try
            {
                var response = new PagedList<CenterCommentDto>();


                var center = await _unitOfWork.Centers.GetByIdAsync(request.CenterId);
                if (center == null)
                    return null;
                var result = new PagedList<CenterCommentDto>();

                result.List = center.CenterComment.Select(_ => new CenterCommentDto
                {
                    Comment = new CommentDto
                    {
                        Email = _.Email,
                        Name = _.Name,
                        Subject = _.Subject,
                        Text = _.Text,
                        Score = _.Score,
                    },

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
        #region Center


        #region Update 
        public async Task<ReturnUiResult> UpdateCenterLicensesAsync(UpdateCenterLicensesDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                if (dto.LicensesId == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مجوز ای یافت نشد");
                    return returnUiResult;
                }
                var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);
                if (center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
                var Ids = dto.LicensesId.Count > 0 ? dto.LicensesId.ToList() : new List<int>();
                if (dto.LicensesId.Count > 0)
                {

                    foreach (var item in dto.LicensesId.ToList() ?? new List<int>())
                    {
                        if (!await _unitOfWork.Licenses.AsQueryable().Where(_ => _.Id.Equals(item)).AnyAsync())
                            Ids.Remove(item);
                    }
                    center.CenterLicensesSelected = Ids.Select(_ => new CenterLicensesSelected
                    {
                        LicensesId = _,
                    }).ToList();
                    await _unitOfWork.Centers.UpdateAsync(center);

                }

                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("مرکز با موفقیت بروزرسانی شد");
                return returnUiResult;
            }
            catch (Exception)
            {

                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا رخ داد");
                return returnUiResult;
            }



        }

        public async Task<ReturnUiResult> UpdateCenterSupplementalInsurancesAsync(UpdateCenterSupplementalInsurancesDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                if (dto.ContractedSupplementalInsurancesId == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("بیمه ای یافت نشد");
                    return returnUiResult;
                }
                var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);
                if (center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
                var Ids = dto.ContractedSupplementalInsurancesId.Count > 0 ? dto.ContractedSupplementalInsurancesId.ToList() : new List<int>();

                center?.CenterInsurances?.Clear();
                foreach (var item in dto.ContractedSupplementalInsurancesId.ToList() ?? new List<int>())
                {
                    if (center?.CenterInsurances is null)
                    {
                        center.CenterInsurances = new List<CenterInsurances>();
                    }
                    var insurance = await _unitOfWork.Insurances.AsQueryable().Where(_ => _.Id.Equals(item) && _.IsBaseInsurance == false).FirstOrDefaultAsync();
                    if (insurance is not null)
                        center?.CenterInsurances?.Add
                            (new CenterInsurances { InsuranceId = insurance.Id, Status = true });
                }

                await _unitOfWork.Centers.UpdateAsync(center);


                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("مرکز با موفقیت بروزرسانی شد");
                return returnUiResult;
            }
            catch (Exception)
            {

                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا رخ داد");
                return returnUiResult;
            }



        }

        public async Task<ReturnUiResult> UpdateCenterContractedBasicInsurancesAsync(UpdateCenterContractedBasicInsurancesDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                if (dto.ContractedBasicInsurancesId == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("بیمه ای یافت نشد");
                    return returnUiResult;
                }
                var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);
                if (center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
                var Ids = dto.ContractedBasicInsurancesId.Count > 0 ? dto.ContractedBasicInsurancesId.ToList() : new List<int>();

                center?.CenterInsurances?.Clear();
                foreach (var item in dto.ContractedBasicInsurancesId.ToList() ?? new List<int>())
                {

                    if (center?.CenterInsurances is null)
                    {
                        center.CenterInsurances = new List<CenterInsurances>();
                    }

                    var insurance = await _unitOfWork.Insurances.AsQueryable().Where(_ => _.Id.Equals(item) && _.IsBaseInsurance == true).FirstOrDefaultAsync();
                    if (insurance is not null)
                        center?.CenterInsurances?.Add
                            (new CenterInsurances { InsuranceId = insurance.Id, Status = true });
                }
                await _unitOfWork.Centers.UpdateAsync(center);



                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("مرکز با موفقیت بروزرسانی شد");
                return returnUiResult;
            }
            catch (Exception)
            {

                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا رخ داد");
                return returnUiResult;
            }



        }


        public async Task<ReturnUiResult> UpdateCenterServicesAvailableAsync(UpdateCenterServicesAvailableDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                if (dto.ServicesDoctor == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("خدمت ای یافت نشد");
                    return returnUiResult;
                }
                var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);
                if (center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
                if (dto.ServicesDoctor.Count > 0)
                {
                    center?.CenterServices?.Clear();
                    foreach (var item in dto.ServicesDoctor)
                    {
                        if (center?.CenterServices is null)
                        {
                            center.CenterServices = [];
                        }



                        var servicesAvailable = await _unitOfWork.ProviderServices.AsQueryable().Where(_ => _.Id.Equals(item.ServicesAvailableId)).FirstOrDefaultAsync();
                        if (servicesAvailable is not null)
                            center?.CenterServices?.Add
                                (new CenterServiceSelected { ServiceId = servicesAvailable.Id, Status = true });
                    }
                    await _unitOfWork.Centers.UpdateAsync(center);

                }

                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("مرکز با موفقیت بروزرسانی شد");
                return returnUiResult;
            }
            catch (Exception)
            {

                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا رخ داد");
                return returnUiResult;
            }



        }
        public async Task<ReturnUiResult> UpdateCenterServicesAvailableAsync(UpdateDoctorCenterServicesAvailableDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                if (dto.ServicesAvailableId == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("خدمت ای یافت نشد");
                    return returnUiResult;
                }
                var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);
                if (center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }

                var doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.DoctorId);
                if (doctor == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دکتر یافت نشد");
                    return returnUiResult;
                }

                var currentCenter = doctor.CenterDoctors.Where(_ => _.CenterId == dto.CenterId).FirstOrDefault();
                if (currentCenter == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
                var Ids = dto.ServicesAvailableId.Count > 0 ? dto.ServicesAvailableId.ToList() : new List<int>();

                center?.CenterServices?.Clear();
                foreach (var item in dto.ServicesAvailableId.ToList() ?? new List<int>())
                {
                    //if (currentCenter is not null && center?.CenterServices is null)
                    //{
                    //    currentCenter.centerser = new List<DoctorService>();
                    //}

                    var servicesAvailable = await _unitOfWork.ProviderServices.AsQueryable().Where(_ => _.Id.Equals(item)).FirstOrDefaultAsync();
                    if (servicesAvailable is not null)
                        center?.CenterServices.Add
                            (new CenterServiceSelected { Status = true, ServiceId = servicesAvailable.Id });
                }
                await _unitOfWork.Doctors.UpdateAsync(doctor);



                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("مرکز با موفقیت بروزرسانی شد");
                return returnUiResult;
            }
            catch (Exception)
            {

                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا رخ داد");
                return returnUiResult;
            }



        }

        public async Task<ReturnUiResult> UpdateCenterServicesAvailableAsync(ToggleDoctorCenterServicesAvailableDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                if (dto.ServicesAvailableId == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("خدمت ای یافت نشد");
                    return returnUiResult;
                }
                var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);
                if (center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }

                var doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.DoctorId.Value);
                if (doctor == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دکتر یافت نشد");
                    return returnUiResult;
                }


                var currentService = center.CenterServices.Where(_ => _.ServiceId == dto.ServicesAvailableId).FirstOrDefault();
                if (currentService == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("خدمت یافت نشد");
                    return returnUiResult;
                }
                currentService.Status = !currentService.Status;
                await _unitOfWork.Doctors.UpdateAsync(doctor);



                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("مرکز با موفقیت بروزرسانی شد");
                return returnUiResult;
            }
            catch (Exception)
            {

                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا رخ داد");
                return returnUiResult;
            }



        }

        public async Task<ReturnUiResult> DeleteDoctorCenterServicesAvailableAsync(ToggleDoctorCenterServicesAvailableDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                if (dto.ServicesAvailableId == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("خدمت ای یافت نشد");
                    return returnUiResult;
                }
                var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);
                if (center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }

                var doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.DoctorId.Value);
                if (doctor == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دکتر یافت نشد");
                    return returnUiResult;
                }


                var currentService = center.CenterServices.Where(_ => _.ServiceId == dto.ServicesAvailableId).FirstOrDefault();
                if (currentService == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("خدمت یافت نشد");
                    return returnUiResult;
                }
                center.CenterServices.Remove(currentService);
                await _unitOfWork.Doctors.UpdateAsync(doctor);



                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("مرکز با موفقیت بروزرسانی شد");
                return returnUiResult;
            }
            catch (Exception)
            {

                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا رخ داد");
                return returnUiResult;
            }



        }
        public async Task<ReturnUiResult> UpdateCenterServicesAvailableAsync(ToggleCenterServicesAvailableDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                if (dto.ServicesAvailableId == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("خدمت ای یافت نشد");
                    return returnUiResult;
                }
                var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);
                if (center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }


                CenterServiceSelected? service;
                service = center.CenterServices?.Where(_ => _.Id == dto.ServicesAvailableId).FirstOrDefault();
                if (service is not null)
                {
                    service.Status = !service.Status;
                    await _unitOfWork.Centers.UpdateAsync(center);

                }
                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("مرکز با موفقیت بروزرسانی شد");
                return returnUiResult;
            }
            catch (Exception)
            {

                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا رخ داد");
                return returnUiResult;
            }



        }
        public async Task<ReturnUiResult> DeleteCenterServicesAvailableAsync(ToggleCenterServicesAvailableDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                if (dto.ServicesAvailableId == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("خدمت ای یافت نشد");
                    return returnUiResult;
                }


                var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);
                if (center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
                CenterServiceSelected? service;
                service = center.CenterServices?.Where(_ => _.Id == dto.ServicesAvailableId).FirstOrDefault();
                if (service is not null)
                {
                    center.CenterServices?.Remove(service);
                    await _unitOfWork.Centers.UpdateAsync(center);

                }
                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("مرکز با موفقیت بروزرسانی شد");
                return returnUiResult;
            }
            catch (Exception)
            {

                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا رخ داد");
                return returnUiResult;
            }



        }
        public async Task<ReturnUiResult> UpdateCenterInsurancesAsync(ToggleCenterInsurancesDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                var insurances = await _unitOfWork.Insurances.GetByIdAsync(dto.InsurancesId);
                if (insurances == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("بیمه ای یافت نشد");
                    return returnUiResult;
                }
                var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);
                if (center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
                if (dto.IsBasic)
                {
                    var service = center.CenterInsurances?.Where(_ => _.Id == dto.InsurancesId).FirstOrDefault();
                    if (service is not null)
                        service.Status = !service.Status;
                }

                else
                {
                    var service = center.CenterInsurances?.Where(_ => _.Id == dto.InsurancesId).FirstOrDefault();
                    if (service is not null)
                        service.Status = !service.Status;
                }


                await _unitOfWork.Centers.UpdateAsync(center);
                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("مرکز با موفقیت بروزرسانی شد");
                return returnUiResult;
            }
            catch (Exception)
            {

                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا رخ داد");
                return returnUiResult;
            }



        }
        public async Task<ReturnUiResult> DeleteCenterInsurancesAsync(DeleteCenterInsurancesDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                var insurances = await _unitOfWork.Insurances.GetByIdAsync(dto.InsurancesId);
                if (insurances == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("بیمه ای یافت نشد");
                    return returnUiResult;
                }
                var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);
                if (center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }

                var service1 = center.CenterInsurances?.Where(_ => _.Id == dto.InsurancesId).FirstOrDefault();
                if (service1 is not null)
                    center.CenterInsurances?.Remove(service1);


                var service2 = center.CenterInsurances?.Where(_ => _.Id == dto.InsurancesId).FirstOrDefault();
                if (service2 is not null)
                    center.CenterInsurances?.Remove(service2);



                await _unitOfWork.Centers.UpdateAsync(center);
                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("مرکز با موفقیت حذف شد");
                return returnUiResult;
            }
            catch (Exception)
            {

                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا رخ داد");
                return returnUiResult;
            }



        }
        public async Task<ReturnUiResult> UpdateCenterDepartmentAsync(CreateCenterDepartmentDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                if (dto.CenterDepartment == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("بخش های مربوط به  مرکز یافت نشد");
                    return returnUiResult;
                }
                var center = await _unitOfWork.Centers
                             .AsQueryable()
                             .Where(_ => _.Id == dto.CenterId)
                             .Include(_ => _.CenterDepartment)
                             .FirstOrDefaultAsync();
                if (center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
                var Ids = dto.CenterDepartment.Count > 0 ? dto.CenterDepartment.ToList() : new List<string>();
                if (dto.CenterDepartment.Count > 0)
                {
                    await _unitOfWork.CenterDepartment.DeleteRangeAsync(center.CenterDepartment);

                    center.CenterDepartment = dto.CenterDepartment.Count > 0 ? dto.CenterDepartment.Select(_ => new CenterDepartment { Name = _, }).ToList() : new List<CenterDepartment>();

                    await _unitOfWork.Centers.AttachAsync(center);

                }

                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("مرکز با موفقیت بروزرسانی شد");
                return returnUiResult;
            }
            catch (Exception)
            {

                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا رخ داد");
                return returnUiResult;
            }



        }

        public async Task<ReturnUiResult> UpdateCenterDepartmentAsync(ToggleCenterDepartmentDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {

                var center = await _unitOfWork.Centers
                            .AsQueryable()
                            .Where(_ => _.Id == dto.CenterId)
                            .Include(_ => _.CenterDepartment)
                         
                            .FirstOrDefaultAsync();
                if (center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
                var departmant = center.CenterDepartment?.Where(_ => _.Id == dto.DepartmentId).FirstOrDefault();
                if (departmant == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("بخش یافت نشد");
                    return returnUiResult;
                }
                departmant.Status = !departmant.Status;
                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("مرکز با موفقیت بروزرسانی شد");

                await _unitOfWork.Centers.AttachAsync(center);
                return returnUiResult;
            }
            catch (Exception)
            {

                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا رخ داد");
                return returnUiResult;
            }



        }
        public async Task<ReturnUiResult> UpdateCenterPictureAsync(UpdateCenterPictureDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {

                var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);
                if (center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
                center.CenterPicture = dto.Picture.Select(_ => new CenterPicture
                {
                    Picture = _.Picture,
                    PictureType = _.PictureType,
                }).ToList();

                center.Picture = dto.MainPicture;
                await _unitOfWork.Centers.UpdateAsync(center);
                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("مرکز با موفقیت بروزرسانی شد");
                return returnUiResult;
            }
            catch (Exception)
            {

                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا رخ داد");
                return returnUiResult;
            }



        }





        public async Task<ReturnUiResult> UpdateCenterGeneralAsync(UpdateGeneralCenterDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {

                var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);
                if (center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
                center.Name = dto.Name;
                center.WebSite = dto.WebSite;
                center.Address = dto.Address;
                center.Description = dto.Description;
                center.Bio = dto.Bio;
                center.DateOfEstablishment = dto.DateOfEstablishment;
                center.PhoneNumber = dto.PhoneNumber;
                center.Phone = dto.Phone != null ? string.Join(",", dto.Phone) : string.Empty;
                center.FaxNumber = dto.FaxNumber;
                center.Email = dto.Email;

                await _unitOfWork.Centers.AttachAsync(center);

                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("مرکز با موفقیت بروزرسانی شد");
                return returnUiResult;
            }
            catch (Exception)
            {

                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا رخ داد");
                return returnUiResult;
            }



        }


        #endregion

        #region Departmant  


        public async Task<ReturnUiResult> ToogleStatusDoctorCenterAsync(ToggleDoctorCenterDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {

                var center = await _unitOfWork.Centers
                          .AsQueryable()
                          .Where(_ => _.Id == dto.CenterId)
                        
                          .Include(_ => _.CenterDoctors)
                          .FirstOrDefaultAsync();
                if (center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
              

                if (!center.CenterDoctors.Any(_ => _.DoctorId == dto.DoctorId))
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دکتر یافت نشد");
                    return returnUiResult;
                }
                var currentCenter = center?.CenterDoctors.FirstOrDefault(_ => _.DoctorId == dto.DoctorId);
                if (currentCenter == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
                currentCenter.Status = (!currentCenter.Status);
                //foreach (var department in currentCenter.Departments)
                //{

                //    department.Status = (!department.Status);
                //}

                await _unitOfWork.Centers.AttachAsync(center);
                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("دکتر با موفقیت بروزرسانی شد");
                return returnUiResult;
            }
            catch (Exception)
            {

                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا رخ داد");
                return returnUiResult;
            }



        }
        public async Task<ReturnUiResult> AddDoctorCenterAsync(AddDoctorCenterDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {

                var center = await _unitOfWork.Centers.AsQueryable()
                    .Include(_ => _.CenterDepartment)
                    .Include(_ => _.CenterDoctors)
                    .Where(_ => _.Id == dto.CenterId).FirstOrDefaultAsync();
                if (center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
                var departmant = center.CenterDepartment?.FirstOrDefault(_ => _.Id == dto.DepartmanId);
                if (center.CenterDepartment == null || departmant == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("ساختمان یافت نشد");
                    return returnUiResult;
                }
                var doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.DoctorId);
                if (doctor == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دکتر یافت نشد");
                    return returnUiResult;
                }

                if (!center.CenterDoctors.Any(_ => _.CenterId == dto.CenterId))
                {
                    center.CenterDoctors.Add(new CenterDoctorsSelected
                    {
                        CenterId = dto.CenterId,
                        DoctorId = dto.DoctorId,
                    });
                }
                var currentCenter = center.CenterDoctors.FirstOrDefault(_ => _.CenterId == dto.CenterId);

                if (!currentCenter.CenterDoctorsDepartmant.Any(_ => _.Id == dto.DepartmanId))
                {
                    currentCenter.CenterDoctorsDepartmant.Add(new CenterDoctorsDepartmantSelected { CenterDepartmentId = departmant.Id, Status = true });
                }
                await _unitOfWork.Doctors.AttachAsync(doctor);
                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("دکتر با موفقیت بروزرسانی شد");
                return returnUiResult;
            }
            catch (Exception)
            {

                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا رخ داد");
                return returnUiResult;
            }



        }
        public async Task<PagedList<DepartmentCenterDto>> GetDepartmantAsync(GetDoctorCenterDepartmantRequest dto)
        {
            try
            {
                var response = new PagedList<DepartmentCenterDto>();

                // 1. گرفتن مرکز
                var center = await _unitOfWork.Centers
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(c => c.CenterDepartment)
                    .FirstOrDefaultAsync(c => c.Id == dto.CenterId);

                if (center == null || center.CenterDepartment == null || !center.CenterDepartment.Any())
                    return response; // 👈 همیشه PagedList خالی برگردون، نه null

                // 2. اعمال فیلتر عنوان
                var departments = center.CenterDepartment.AsQueryable();
                if (!string.IsNullOrWhiteSpace(dto.Title))
                    departments = departments.Where(d => d.Name.Contains(dto.Title));

                // 3. صفحه‌بندی
                var totalCount = departments.Count();
                var totalPages = (int)Math.Ceiling(totalCount / (double)dto.PageSize);

                var departmentList = departments
                    .Skip((dto.PageNumber.Value - 1) * dto.PageSize.Value)
                    .Take(dto.PageSize.Value)
                    .Select(d => new DepartmentCenterDto
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Status = d.Status
                    })
                    .ToList();

                // 4. ساخت خروجی
                response.List = departmentList;
                response.Pagination = new PagedListInfo
                {
                    PageNumber = dto.PageNumber.Value,
                    PageSize = dto.PageSize,
                    TotalCount = totalCount,
                    TotalPages = totalPages
                };

                return response;
            }
            catch (Exception ex)
            {
                // 👈 پیشنهاد: لاگ کردن خطا
                return new PagedList<DepartmentCenterDto>
                {
                    List = new List<DepartmentCenterDto>(),
                    Pagination = new PagedListInfo
                    {
                        PageNumber = dto.PageNumber.Value,
                        PageSize = dto.PageSize,
                        TotalCount = 0,
                        TotalPages = 0
                    }
                };
            }
        }


        public async Task<ReturnUiResult> DeleteDoctorCenterAsync(AddDoctorCenterDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {

                var center = await _unitOfWork.Centers
                    .AsQueryable()
                    .Where(_ => _.Id == dto.CenterId)
                    .Include(_ => _.CenterDepartment)
                    .Include(_ => _.CenterDoctors).ThenInclude(_ => _.CenterDoctorsDepartmant)
                    .FirstOrDefaultAsync();

                if (center == null)

                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
                var departmant = center.CenterDepartment?.FirstOrDefault(_ => _.Id == dto.DepartmanId);
                if (center.CenterDepartment == null || departmant == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("ساختمان یافت نشد");
                    return returnUiResult;
                }
                var doctor = await _unitOfWork.Doctors
                    .AsQueryable()
                    .Where(_ => _.Id == dto.DoctorId)
                    .Include(_ => _.CenterDoctors)
                    .FirstOrDefaultAsync();

                var currentCenter = center?.CenterDoctors?.FirstOrDefault(_ => _.DoctorId == dto.DoctorId);
                if (currentCenter == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
                var currentDepartmant = currentCenter?.CenterDoctorsDepartmant?.FirstOrDefault(_ => _.CenterDepartmentId == dto.DepartmanId);

                if (currentDepartmant == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("ساختمان یافت نشد");
                    return returnUiResult;
                }

                await _unitOfWork.CenterDoctorsDepartmantSelected.DeleteAsync(currentDepartmant.Id);


                if (currentCenter?.CenterDoctorsDepartmant?.Count == 0)
                    await _unitOfWork.CenterDoctorsSelected.DeleteAsync(currentCenter.Id);

                await _unitOfWork.Doctors.AttachAsync(doctor);

                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("دکتر با موفقیت بروزرسانی شد");
                return returnUiResult;
            }
            catch (Exception)
            {

                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا رخ داد");
                return returnUiResult;
            }



        }

        public async Task<ReturnUiResult> AddDoctorCenterAsync(ToggleDoctorCenterDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {

                var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);
                if (center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }

                var doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.DoctorId);
                if (doctor == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دکتر یافت نشد");
                    return returnUiResult;
                }

                if (doctor.CenterDoctors is null || !doctor.CenterDoctors.Any(_ => _.CenterId == dto.CenterId))
                {
                    doctor?.CenterDoctors?.Add(new CenterDoctorsSelected
                    {
                        CenterId = dto.CenterId,


                    });
                }

                await _unitOfWork.Doctors.AttachAsync(doctor!);
                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("دکتر با موفقیت بروزرسانی شد");
                return returnUiResult;
            }
            catch (Exception)
            {

                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا رخ داد");
                return returnUiResult;
            }



        }
        public async Task<ReturnUiResult> DeleteDoctorCenterAsync(ToggleDoctorCenterDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {

                var center = await _unitOfWork.Centers
                    .AsQueryable()
                    .Where(_ => _.Id == dto.CenterId)
                    .Include(_ => _.CenterDoctors).ThenInclude(_=>_.CenterDoctorsDepartmant)
                    .FirstOrDefaultAsync();
                if (center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }


                var currentCenter = center?.CenterDoctors?.FirstOrDefault(_ => _.DoctorId == dto.DoctorId);
                if (currentCenter == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }
                await _unitOfWork.CenterDoctorsDepartmantSelected.DeleteRangeAsync(currentCenter?.CenterDoctorsDepartmant);

                await _unitOfWork.CenterDoctorsSelected.DeleteAsync(currentCenter.Id);
                //center?.CenterDoctors?.Remove(currentCenter);
                await _unitOfWork.Centers.AttachAsync(center!);

                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("دکتر با موفقیت بروزرسانی شد");
                return returnUiResult;
            }
            catch (Exception)
            {

                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا رخ داد");
                return returnUiResult;
            }



        }

        #endregion
        public async Task<ErrorOr<(int userId, int centerId)>> CreateCenterAsync(CreateCenterDto dto)
        {
            var province = await _unitOfWork.IranProvinces.GetByIdAsync(dto.ProvinceId);
            if (province is null)
                return Error.NotFound(description: "استان یافت نشد");

            var centerType = await _unitOfWork.CenterTypes.GetByIdAsync(dto.CenterTypeId);
            if (centerType is null)
                return Error.NotFound(description: "نوع مرکز یافت نشد");

            var city = await _unitOfWork.IranCities.GetByIdAsync(dto.CityId);
            if (city is null)
                return Error.NotFound(description: "شهر یافت نشد");

            var isUsernameTaken = await _unitOfWork.Centers
                .AsQueryable()
                .AnyAsync(c => c.CenterUser.Select(u => u.User.UserName).Contains(dto.AdminName));

            if (isUsernameTaken)
                return Error.Conflict(description: "نام کاربری قبلاً ثبت شده است");

            var salt = Hashing.GenerateSalt();
            var user = new Domain.Users.User
            {
                FullName = dto.Name,
                FirstName = string.Empty,
                LastName = string.Empty,
                UserName = dto.AdminName,
                UserType = Features.Account.DTOs.UserType.Center,
                Password = Hashing.HashPassword(dto.Password, salt),
                Salt = salt,
            };

            var center = new Center
            {
                Name = dto.Name,
                CenterTypeId = dto.CenterTypeId,
                CityId = dto.CityId,
                ProvinceId = dto.ProvinceId,
                Region = dto.Region,
                TariffExpirationDate = DateTime.Now.AddYears(1),
                CenterUser = new List<CenterUserSelected>
        {
            new CenterUserSelected { User = user }
        }
            };

            await _unitOfWork.Centers.AddAsync(center);
            // await _unitOfWork.SaveChange();

            return (user.Id, center.Id);
        }




        public async Task<ReturnUiResult> EditCenter(EditLocationCenterDto dto)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                //await _unitOfWork.IranCities.AddAsync(new Features.Iran.Entities.IranCity { Name = "sari" });
                //await _unitOfWork.IranProvinces.AddAsync(new Features.Iran.Entities.IranProvince { Name = "mazandaran" });


                var curentCenter = (dto.CenterId) == 0 ? null : await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);

                var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);
                if (center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }


                center.CenterLocation = new CenterLocation
                {
                    Location = new Point(dto.Location.Longitude, dto.Location.Latitude)
                    {
                        SRID = 4326
                    }
                };

                await _unitOfWork.Centers.UpdateAsync(center);
                // await _unitOfWork.SaveChange();

                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("مرکز با موفقیت بروزرسانی شد");
                return returnUiResult;
            }
            catch (Exception)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا رخ داد");
                return returnUiResult;
            }
        }


        public async Task<CenterPictureDtoResponseDto> GetCenterPicture(int CenterId)
        {
            var Center = await _unitOfWork
                  .Centers
                  .AsQueryable()
                  //.AsNoTraking()
                  .Where(a => a.Id == CenterId)
                 .FirstOrDefaultAsync();

            if (Center == null)
                return null;
            var centerDto = new CenterPictureDtoResponseDto();

            centerDto.Picture = Center.CenterPicture.Select(_ => new PictureCenterDto
            {
                Picture = _.Picture,
                PictureType = _.PictureType

            }).ToList();
            centerDto.MainPicture = Center.Picture;
            return centerDto;
        }
        public async Task<CenterServicesAvailableDtoResponseDto> GetCenterServicesAvailable(int CenterId, CenterServicesAvailableRequestDto request)
        {
            var Center = await _unitOfWork
                  .Centers
                  .AsQueryable()
                  //.AsNoTraking()
                  .Where(a => a.Id == CenterId)
                 .FirstOrDefaultAsync();

            if (Center == null)
                return null;
            var centerDto = new CenterServicesAvailableDtoResponseDto();
            centerDto.ServicesAvailable = new PagedList<CenterItemServicesAvailable>();

            var serivce = Center.CenterServices?.ToList();


            if (request.Status.HasValue)
                serivce = serivce.Where(_ => _.Status == request.Status.Value).ToList();

            if (!request.Title.IsNullOrEmpty())
                serivce = serivce.Where(_ => _.ProviderServices.Name.Contains(request.Title)).ToList();

            centerDto.ServicesAvailable.List = serivce.Select(s => new CenterItemServicesAvailable
            {
                Name = s.ProviderServices.Name,
                Id = s.ServiceId,
                Status = s.Status,
            }).ToList();

            double _totalPages = centerDto.ServicesAvailable.List.Count / request.PageSize.Value;

            var paginationParams = new PagedListInfo()
            {
                TotalCount = centerDto.ServicesAvailable.List.Count,
                PageNumber = request.PageNumber.Value,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(_totalPages) + 1
            };
            centerDto.ServicesAvailable.Pagination = paginationParams;
            //TODO
            return centerDto;

        }

        public async Task<PagedList<GetInsuranceDetailResponseDto>> GetCenterContractedBasicInsurances(int CenterId, CenterContractedInsurancesRequestDto request)
        {
            var Center = await _unitOfWork
                  .Centers
                  .AsQueryable()
                  //.AsNoTraking()
                  .Where(a => a.Id == CenterId)
                 .FirstOrDefaultAsync();

            if (Center == null)
                return null;

            var response = new PagedList<GetInsuranceDetailResponseDto>();

            var insuranceIds = Center.CenterInsurances?.Select(_ => _.Id).ToList();
            if (insuranceIds != null)
            {

                var insurances = _unitOfWork.Insurances
                    .AsQueryable()
                    .Where(i => insuranceIds.Contains(i.Id));
                if (!request.Title.IsNullOrEmpty())
                    insurances = insurances.Where(_ => _.Name.Contains(request.Title));




                var data = await insurances.ToListAsync();
                response.List = data.Select(i => new GetInsuranceDetailResponseDto
                {
                    Id = i.Id,
                    IsBaseInsurance = i.IsBaseInsurance,
                    Name = i.Name,
                    Order = i.Order,
                    Picture = i.Picture,
                    Status = Center.CenterInsurances?
                            .FirstOrDefault(c => c.Id == i.Id)?.Status ?? true
                })
                    .ToList();
                double _totalPages = data.Count / request.PageSize.Value;

                var paginationParams = new PagedListInfo()
                {
                    TotalCount = data.Count,
                    PageNumber = request.PageNumber.Value,
                    PageSize = request.PageSize,
                    TotalPages = (int)Math.Ceiling(_totalPages) + 1
                };
                response.Pagination = paginationParams;
            }

            return response;
        }

        public async Task<PagedList<GetInsuranceDetailResponseDto>> GetCenterContractedSupplementalInsurances(int CenterId, CenterContractedInsurancesRequestDto request)
        {
            var Center = await _unitOfWork
                  .Centers
                  .AsQueryable()
                  //.AsNoTraking()
                  .Where(a => a.Id == CenterId)
                 .FirstOrDefaultAsync();

            if (Center == null)
                return null;
            var response = new PagedList<GetInsuranceDetailResponseDto>();



            var ContractedSupplementalinsuranceIds = Center.CenterInsurances?.Select(_ => _.Id).ToList();

            if (ContractedSupplementalinsuranceIds != null)
            {
                var insurances = _unitOfWork.Insurances
                    .AsQueryable()
                    .Where(i => ContractedSupplementalinsuranceIds.Contains(i.Id));
                if (!request.Title.IsNullOrEmpty())
                    insurances = insurances.Where(_ => _.Name.Contains(request.Title));

                var data = await insurances.ToListAsync();


                response.List = data
                  .Select(i => new GetInsuranceDetailResponseDto
                  {
                      Id = i.Id,
                      IsBaseInsurance = i.IsBaseInsurance,
                      Name = i.Name,
                      Order = i.Order,
                      Picture = i.Picture,
                      Status = Center.CenterInsurances?
                          .FirstOrDefault(c => c.Id == i.Id)?.Status ?? true
                  })
                  .ToList();

                double _totalPages = data.Count / request.PageSize.Value;

                var paginationParams = new PagedListInfo()
                {
                    TotalCount = data.Count,
                    PageNumber = request.PageNumber.Value,
                    PageSize = request.PageSize,
                    TotalPages = (int)Math.Ceiling(_totalPages) + 1
                };
                response.Pagination = paginationParams;


            }

            return response;
        }

        public async Task<CenterDepartmentResponseDto> GetCenterDepartmen(int CenterId)
        {
            var Center = await _unitOfWork
                  .Centers
                  .AsQueryable()
                  //.AsNoTraking()
                  .Where(a => a.Id == CenterId)
                 .FirstOrDefaultAsync();

            if (Center == null)
                return null;
            var centerDto = new CenterDepartmentResponseDto();

            centerDto.CenterDepartment = Center.CenterDepartment?.Select(_ => new CenterDepartmentDto { CenterId = CenterId, DepartmentId = _.Id, DepartmentName = _.Name }).ToList();
            return centerDto;
        }
        public async Task<GetCenterDetailDto?> GetCenterDetailAsync(int centerId)
        {
            var centerDto = await _unitOfWork.Centers
                .AsQueryable()
                .AsNoTracking()
                .Where(c => c.Id == centerId)
                .Select(c => new GetCenterDetailDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    CityId = c.CityId,
                    CityName = c.IranCity!.Name,
                    ProvinceId = c.ProvinceId,
                    ProvinceName = c.IranProvince!.Name,
                    Region = c.Region,
                    Bio = c.Bio,
                    DateOfEstablishment = c.DateOfEstablishment.HasValue
                        ? c.DateOfEstablishment.Value.ToString("yyyy/MM/dd")
                        : null,
                    AdminName = c.CenterUser
                        .Select(u => u.User.UserName)
                        .FirstOrDefault(),
                    PhoneNumber = c.PhoneNumber,
                    Phone = c.Phone ?? string.Empty,
                    FaxNumber = c.FaxNumber,
                    Email = c.Email,
                    WebSite = c.WebSite,
                    Address = c.Address,
                    Licenses = null, // بعداً می‌تونی از relation پر کنی
                    Description = c.Description,
                    MainPicture = c.Picture,
                    Location = new LocationDto
                    {
                        Latitude = c.CenterLocation != null ? c.CenterLocation.Location!.X : 0,
                        Longitude = c.CenterLocation != null ? c.CenterLocation.Location.Y : 0
                    },
                    CenterIds = c.CenterUser.Select(_ => _.CenterId).ToList()

                })
                .FirstOrDefaultAsync();

            return centerDto;
        }

        public async Task<GetCenterDetailResponseDto?> GetCenterDetails(int centerId)
        {
            try
            {
                var center = await _unitOfWork.Centers
                    .AsQueryable()
                    .AsNoTracking()
                    .Include(c => c.IranProvince)
                    .Include(c => c.IranCity)
                    .Include(c => c.CenterLocation)
                    .Include(c => c.CenterDepartment)
                    .Include(c => c.CenterPicture)
                    .Include(c => c.CenterServices).ThenInclude(s => s.ProviderServices)
                    .Include(c => c.CenterUser).ThenInclude(cu => cu.User)
                    .Include(c => c.CenterInsurances).ThenInclude(ci => ci.Insurance)
                    .FirstOrDefaultAsync(c => c.Id == centerId);

                if (center is null)
                    return null;

                // --- مرکز ---
                var centerDto = new GetCenterDetailDto
                {
                    Id = center.Id,
                    Name = center.Name,
                    CityId = center.CityId,
                    CityName = center.IranCity?.Name,
                    ProvinceId = center.ProvinceId,
                    ProvinceName = center.IranProvince?.Name,
                    Region = center.Region,
                    Bio = center.Bio,
                    DateOfEstablishment = center.DateOfEstablishment?.ToString("yyyy/MM/dd"),
                    AdminName = center.CenterUser?.FirstOrDefault()?.User.UserName,
                    PhoneNumber = center.PhoneNumber,
                    Phone = center.Phone ?? string.Empty,
                    FaxNumber = center.FaxNumber,
                    Email = center.Email,
                    WebSite = center.WebSite,
                    Address = center.Address,
                    Licenses = null, // TODO: در صورت نیاز پر کن
                    Description = center.Description,
                    MainPicture = center.Picture,
                    Location = new LocationDto
                    {
                        Latitude = center.CenterLocation?.Location?.Y ?? 0,
                        Longitude = center.CenterLocation?.Location?.X ?? 0
                    }
                };

                // --- دپارتمان‌ها ---
                var departments = center.CenterDepartment?
                    .Select(d => new CenterDepartmentDto
                    {
                        CenterId = centerId,
                        DepartmentId = d.Id,
                        DepartmentName = d.Name
                    })
                    .ToList() ?? new();

                // --- تصاویر ---
                var pictures = center.CenterPicture?
                    .Select(p => new PictureCenterDto
                    {
                        Picture = p.Picture,
                        PictureType = p.PictureType
                    })
                    .ToList() ?? new();

                // --- سرویس‌ها ---
                var services = center.CenterServices?
                    .Select(s => new CenterItemServicesAvailable
                    {
                        Id = s.Id,
                        Name = s.ProviderServices.Name
                    })
                    .ToList() ?? new();

                // --- بیمه‌ها ---
                var insurances = center.CenterInsurances?
                    .Where(ci => ci.Insurance != null)
                    .Select(ci => new GetInsuranceDetailResponseDto
                    {
                        Id = ci.Id,
                        IsBaseInsurance = ci.Insurance!.IsBaseInsurance,
                        Name = ci.Insurance.Name,
                        Order = ci.Insurance.Order,
                        Picture = ci.Insurance.Picture ?? string.Empty,
                        Status = ci.Status
                    })
                    .ToList() ?? new();

                var response = new GetCenterDetailResponseDto
                {
                    Center = centerDto,
                    CenterDepartment = departments,
                    Picture = pictures,
                    ServicesAvailable = services,
                    ContractedBasicInsurances = insurances.Where(i => i.IsBaseInsurance).ToList(),
                    ContractedSupplementalInsurances = insurances.Where(i => !i.IsBaseInsurance).ToList()
                };

                return response;
            }
            catch (Exception ex)
            {
                // 👈 بهتره لاگ داشته باشی
                return null;
            }
        }

        public async Task<PagedList<GetDoctorCenterDetailDto>> GetCenterDoctor(GetDoctorCenterResponseParams request)
        {
            try
            {
                // مرکز رو همراه با ارتباط‌ها لود می‌کنیم
                var center = await _unitOfWork.Centers.AsQueryable()
                    .Include(c => c.CenterServices)
                    .Include(c => c.CenterDoctors)
                        .ThenInclude(cd => cd.CenterDoctorsDepartmant)
                            .ThenInclude(cdd => cdd.CenterDepartment)
                    .Include(c => c.CenterDoctors)
                        .ThenInclude(cd => cd.Doctor)
                            .ThenInclude(d => d.User)
                    .Include(c => c.CenterDoctors)
                        .ThenInclude(cd => cd.Doctor)
                            .ThenInclude(d => d.DoctorExpertises)
                                .ThenInclude(de => de.Expertise)
                    .FirstOrDefaultAsync(c => c.Id == request.CenterId);

                if (center == null)
                    return null;

                // فیلتر دکترها
                var doctorsQuery = _unitOfWork.Doctors.AsQueryable()
                    .Include(d => d.User)
                    .Include(d => d.DoctorExpertises).ThenInclude(e => e.Expertise)
                    .Include(d => d.CenterDoctors)
                        .ThenInclude(cd => cd.CenterDoctorsDepartmant)
                            .ThenInclude(cdd => cdd.CenterDepartment)
                    .Include(d => d.CenterDoctors)
                        .ThenInclude(cd => cd.CenterDoctorsService)
                            .ThenInclude(cs => cs.ProviderService)
                    .AsQueryable();

                if (request.CenterId.HasValue)
                {
                    doctorsQuery = doctorsQuery.Where(d =>
                        d.CenterDoctors.Any(cd => cd.CenterId == request.CenterId));
                }

                if (!string.IsNullOrWhiteSpace(request.Title))
                {
                    doctorsQuery = doctorsQuery.Where(d =>
                        d.User.FullName.Contains(request.Title));
                }

                // join با شیفت‌ها
                var doctorQuery =
                    from doctor in doctorsQuery
                    join shift in _unitOfWork.DoctorShifts.AsQueryable()
                        on doctor.Id equals shift.CenterDoctorsDepartmant.CenterDoctorsSelected.DoctorId into shiftGroup
                    select new GetDoctorCenterDetailDto
                    {
                        Id = doctor.Id,
                        FullName = doctor.User.FullName??string.Empty,
                        Gender = doctor.Gender,
                        Mobile = doctor.User.MobileNumber ?? string.Empty,
                        PhoneNumber = doctor.User.MobileNumber ?? string.Empty,
                        Picture = doctor.User.Picture,

                        Status = doctor.CenterDoctors
                            .Where(cd => cd.CenterId == request.CenterId)
                            .Select(cd => cd.Status)
                            .FirstOrDefault(),

                        Departments = doctor.CenterDoctors
                            .SelectMany(cd => cd.CenterDoctorsDepartmant)
                            .Select(cdd => new DepartmentAllDto
                            {
                                Id = cdd.Id,
                                Name = cdd.CenterDepartment.Name??string.Empty
                            }).ToList(),

                        ExpertiseName = string.Join(",", doctor.DoctorExpertises.Select(de => de.Expertise.Name)),

                        Shifts = shiftGroup
                            .Where(sh => sh.CenterDoctorsDepartmant!=null&& sh.CenterDoctorsDepartmant.CenterDepartment!=null&& sh.CenterDoctorsDepartmant.CenterDepartment.CenterId == request.CenterId)
                            .Select(sh => new ShiftDoctorCenterDto
                            {
                                ShiftId = sh.Id,
                                DayOfWeek = sh.DayOfWeek,
                                DoctorId = sh.CenterDoctorsDepartmant.CenterDoctorsSelected.DoctorId ?? 0,
                                StartTime = sh.StartTime,
                                EndTime = sh.EndTime,
                                ShiftType = sh.ShiftType
                            }).ToList(),

                        Services = doctor.CenterDoctors
                            .SelectMany(cd => cd.CenterDoctorsService)
                            .Select(s => new GetServicesAvailableListResponseDto
                            {
                                Id = s.ProviderServiceId ?? 0,
                                Name = s.ProviderService.Name ?? string.Empty,
                                Status = s.Status
                            }).ToList()
                    };

                // صفحه‌بندی
                var result = await doctorQuery.ToPagedList(request.PageNumber, request.PageSize);

                // مرتب‌سازی شیفت‌ها
                foreach (var item in result.List)
                {
                    item.Shifts = item.Shifts.OrderBy(s => s.DayOfWeek).ToList();
                }

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PagedList<GetCenterDto>> GetCenters(GetCenterResponseParams request)
        {
            try
            {
                var response = new PagedList<GetCenterDto>();

                var CenterDtos = _unitOfWork
                     .Centers
                     .AsQueryable();

                if (!string.IsNullOrEmpty(request.Title))
                    CenterDtos = CenterDtos.Where(_ => _.Name.Contains(request.Title));

                var result = await CenterDtos.ToPagedList(s => new GetCenterDto
                {

                    Id = s.Id,
                    Name = s.Name
                }, request.PageNumber, request.PageSize);


                return result;
            }
            catch (Exception)
            {
                // ثبت لاگ
                return null; ;
            }
        }


        public async Task<PagedList<GetCenterDto>> GetCenters(NearbyLocationDto request)
        {
            try
            {
                var response = new PagedList<GetCenterDto>();

                var center = await _unitOfWork
                     .Centers
                     .GetByIdAsync(request.CenterId);
                if (center == null)
                    return null;

                var userLocation = new NetTopologySuite.Geometries.Point(center.CenterLocation.Location.X, center.CenterLocation.Location.Y) { SRID = 4326 };

                var centers = await _unitOfWork.Centers.AsQueryable()
        .Where(c => c.CenterLocation != null && c.CenterLocation.Location != null &&
                    c.CenterLocation.Location.Distance(userLocation) <= request.RadiusInMeters)
        .ToListAsync();




                //.AsNoTraking()
                response.List = centers
                .Select(s => new GetCenterDto
                {

                    Id = s.Id,
                    Name = s.Name
                })

                 .Skip(request.PageSize.Value * request.PageNumber!.Value)
                 .Take(request.PageSize.Value).ToList();



                if (centers == null)
                    return null;

                //double _totalPages = centers.Count() / request.PageSize.Value;

                //var paginationParams = new PaginationParams()
                //{
                //    TotalCount = centers.Count(),
                //    PageNumber = request.PageNumber.Value,
                //    PageSize = request.PageSize,
                //    TotalPages = (int)Math.Ceiling(_totalPages)
                //};
                //response.Pagination = paginationParams;

                return response;
            }
            catch (Exception)
            {
                // ثبت لاگ
                return null; ;
            }
        }
        public async Task<ReturnUiResult> DeleteCenter(int CenterId)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                var Center = await _unitOfWork
                    .Centers
                    .AsQueryable()
                    .FirstOrDefaultAsync(s => s.Id == CenterId);

                if (Center == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("مرکز یافت نشد");
                    return returnUiResult;
                }

                await _unitOfWork.Centers.DeleteAsync(CenterId);

                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("مرکز با موفقیت حذف شد ");
                return returnUiResult;
            }
            catch (Exception)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("مرکز یافت نشد");
                return returnUiResult;
            }
        }


        public async Task<CenterLoginRequest> LoginCenterAsync(CenterLoginDto CenterDto)
        {

            var user = await _unitOfWork.Users.AsQueryable().FirstOrDefaultAsync(_ => _.UserName.Equals(CenterDto.Username));
            if (user == null) return null;
            var IsValidPassord = Hashing.ComparePassword(user.Password, CenterDto.Password, user.Salt) ? new CenterLoginRequest() { Id = user.CenterUser.Select(_ => _.Id).FirstOrDefault() } : null;
            return IsValidPassord;

        }

        #endregion

        #region Dispose 

        void IDisposable.Dispose()
        {
            if (_unitOfWork != null)
                _unitOfWork.Centers.TryDisposeSafe();
        }



        #endregion

    }
}
