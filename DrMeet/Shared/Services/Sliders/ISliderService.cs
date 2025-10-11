using DNTCommon.Web.Core;

using DrMeet.Api.Features.Sliders.DTOs;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.PagedList;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Api.Shared.Services.UserService;
using DrMeet.Domain.Others;
using MongoDB.Driver.Linq;

namespace DrMeet.Api.Shared.Services.Sliders
{
    public interface ISliderService : IDisposable
    {

        #region Slider

        // ثبت مقاله
        Task<ReturnUiResult> CreateSliderAsync(CreateSliderRequestDto dto, int UserId);
        // گرفتن یک مقاله
        Task<GetSliderDetailResponseDto> GetSlider(int SliderId);
        // ویرایش مقاله
        Task<ReturnUiResult> EditSlider(UpdateSliderRequestDto dtom, int UserId);
        // گزفتن کل مقاله
        Task<PagedList.PagedList<GetSliderListResponseDto>> GetSliders(GetSliderRrequestResponseParams request);
        Task<PagedList<GetSliderListResponseDto>> GetSliders(GetSliderByCenterIdRequestResponseParams request);
        Task<PagedList<GetSliderListResponseDto>> GetSliders(GetSliderByDoctorIdRequestResponseParams request);

        Task<ReturnUiResult> DeleteSlider(int SliderId, int UserId);


        #endregion

    }
    public class SliderService : ISliderService
    {

        #region Constructor

        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public SliderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Slider

        public async Task<ReturnUiResult> CreateSliderAsync(CreateSliderRequestDto dto, int UserId)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {

             
                Domain.Others.Slider ds = new Domain.Others.Slider()
                {
                    CreateDate = DateTime.Now,
                    Title = dto.Title,
                    CenterId = UserId,
                    Status = true,
                    ImagePath = await FileUploadManager.UploadAsync(dto.ImagePath, FolderImagesType.Sliders),
                };

                await _unitOfWork.Sliders.AddAsync(ds);
                // await _unitOfWork.SaveChange();

                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("ثبت بنر با موفقیت انجام شد");
                return returnUiResult;
            }
            catch (Exception)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("ثبت بنر با خطا مواجه شد");
                return returnUiResult;
            }
        }

        public async Task<ReturnUiResult> EditSlider(UpdateSliderRequestDto dto, int centerId)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                var Slider = await _unitOfWork.Sliders.GetByIdAsync(dto.Id);

                if (Slider == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("اسلایدر یافت نشد");
                    return returnUiResult;
                }

              
                if (!Slider.CenterId.Equals(centerId))
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دسترسی یافت نشد");
                    return returnUiResult;
                }
                Slider.Title = dto.Title;
                Slider.CenterId = centerId;
                if (dto.ImagePath is not null)
                {

                    Slider.ImagePath = await FileUploadManager.UploadAsync(dto.ImagePath, FolderImagesType.Sliders);
                }





                await _unitOfWork.Sliders.UpdateAsync(Slider);
                // await _unitOfWork.SaveChange();
                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("ثبت اسلایدر با موفقیت انجام شد");
                return returnUiResult;

            }
            catch (Exception)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("ثبت اسلایدر با خطا مواجه شد");
                return returnUiResult;
            }
        }

        public async Task<GetSliderDetailResponseDto> GetSlider(int SliderId)
        {
            try
            {
                var Slider = await _unitOfWork
                    .Sliders
                    .AsQueryable()
                    //.AsNoTraking()
                    .Where(a => a.Id == SliderId)
                    .Select(s => new GetSliderDetailResponseDto
                    {
                        CreateDate = s.CreateDate,
                        Title = s.Title,
                        ImagePath = s.ImagePath,
                        Id = s.Id
                    }).FirstOrDefaultAsync();

                if (Slider == null)
                    return null;

                return Slider;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<PagedList<GetSliderListResponseDto>> GetSliders(GetSliderByDoctorIdRequestResponseParams request)
        {
            try
            {
                var response = new PagedList<GetSliderListResponseDto>();

                var SliderDtos = _unitOfWork
                     .Sliders
                     .AsQueryable();

                //if (request.DoctorId!=0)
                //{
                //    var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.DoctorId);
                //    if (doctor != null)
                //        SliderDtos = SliderDtos.Where(_ => _.UserId.Equals(doctor.UserId));
                //}



                var result = await SliderDtos.ToPagedList<Domain.Others.Slider, GetSliderListResponseDto>(s => new GetSliderListResponseDto
                {
                    Title = s.Title,
                    ImagePath = s.ImagePath,
                    Id = s.Id
                }, request.PageNumber, request.PageSize);
                //.AsNoTraking()

                return result;
            }
            catch (Exception)
            {
                // ثبت لاگ
                throw;
            }
        }
        public async Task<PagedList<GetSliderListResponseDto>> GetSliders(GetSliderByCenterIdRequestResponseParams request)
        {
            try
            {
                var response = new PagedList<GetSliderListResponseDto>();

                var SliderDtos = _unitOfWork
                     .Sliders
                     .AsQueryable();

                if ((request.CenterId)!=0)
                {
                    var center = await _unitOfWork.Centers.GetByIdAsync(request.CenterId);
                    if (center != null)
                        SliderDtos = SliderDtos.Where(_ => _.CenterId.Equals(center.Id));
                }



                var result = await SliderDtos.ToPagedList<Slider, GetSliderListResponseDto>(s => new GetSliderListResponseDto
                {
                    Title = s.Title,
                    ImagePath = s.ImagePath,
                    Id = s.Id
                }, request.PageNumber, request.PageSize);
                //.AsNoTraking()

                return result;
            }
            catch (Exception)
            {
                // ثبت لاگ
                throw;
            }
        }



        public async Task<DrMeet.Api.Shared.PagedList.PagedList<GetSliderListResponseDto>> GetSliders(GetSliderRrequestResponseParams request)
        {
            try
            {
                var response = new PagedList<GetSliderListResponseDto>();

                var SliderDtos = _unitOfWork
                     .Sliders
                     .AsQueryable();

                if (!string.IsNullOrEmpty(request.Title))
                    SliderDtos = SliderDtos.Where(_ => _.Title.Contains(request.Title));


                var result = await SliderDtos.ToPagedList<Slider, GetSliderListResponseDto>(s => new GetSliderListResponseDto
                {
                    Title = s.Title,
                    ImagePath = s.ImagePath,
                    Id = s.Id
                }, request.PageNumber, request.PageSize);

                return result;
            }
            catch (Exception)
            {
                // ثبت لاگ
                throw;
            }
        }



        public async Task<ReturnUiResult> DeleteSlider(int SliderId, int centerId)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                var Slider = await _unitOfWork
                    .Sliders
                    .AsQueryable()
                    .FirstOrDefaultAsync(s => s.Id == SliderId);

             
                if (Slider == null)
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("بنر یافت نشد");
                    return returnUiResult;
                }

                if (!Slider.CenterId.Equals(centerId))
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دسترسی یافت نشد");
                    return returnUiResult;
                }
                await FileUploadManager.DeleteAsync(Slider.ImagePath);
                await _unitOfWork.Sliders.DeleteAsync(SliderId);

                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("حذف بنر با موفقیت انجام  شد");
                return returnUiResult;
            }
            catch (Exception)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا  رخ داد");
                return returnUiResult;
            }
        }

        #endregion

        #region Dispose 

        void IDisposable.Dispose()
        {
            if (_unitOfWork != null)
                _unitOfWork.Sliders.TryDisposeSafe();
        }

        #endregion

    }
}
