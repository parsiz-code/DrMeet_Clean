using DNTCommon.Web.Core;
using DrMeet.Api.Shared.Domian;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Domain.ApplicationSettings;
using MongoDB.Driver.Linq;

namespace DrMeet.Api.Shared.Services.Setting
{
    public interface ISettingService : IDisposable
    {

        #region Setting


        // گرفتن یک مقاله
        Task<ApplicationSettingResponseDto> GetSetting();
        // ویرایش مقاله
        Task<ReturnUiResult> EditSetting(ApplicationSettingRequestDto dtom);
        // گزفتن کل مقاله


        #endregion

    }
    public class SettingService : ISettingService
    {

        #region Constructor

        private readonly IUnitOfWork _unitOfWork;

        public SettingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        #endregion

        #region Setting


        public async Task<ReturnUiResult> EditSetting(ApplicationSettingRequestDto dtom)
        {
            var returnUiResult = new ReturnUiResult();
            try
            {
                ApplicationSetting model;
                var isEdit = await _unitOfWork.ApplicationSetting.AsQueryable().AnyAsync();
                if (isEdit)
                {
                    model = await _unitOfWork.ApplicationSetting.AsQueryable().FirstOrDefaultAsync();

                    model.AppVersion = dtom.AppVersion;
                    model.ApiVersion = dtom.ApiVersion;
                    model.FileUploadSetting = dtom.FileUploadSetting.Select(_ => new ApplicationSettingFileUpload
                    {
                        MaximumSize = _.MaximumSize,
                        MaximumSizeFriendlyName = _.MaximumSizeFriendlyName,
                        Type = _.Type,
                        ValidExtensions = _.ValidExtensions,
                    }).ToList();
                    await _unitOfWork.ApplicationSetting.UpdateAsync(model);

                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("تنظیمات اپدیت شد");
                    return returnUiResult;
                }
                else
                {
                    model = new ApplicationSetting
                    {
                        AppVersion = dtom.AppVersion,
                        ApiVersion = dtom.ApiVersion,
                        FileUploadSetting = dtom.FileUploadSetting.Select(_ => new ApplicationSettingFileUpload
                        {
                            MaximumSize = _.MaximumSize,
                            MaximumSizeFriendlyName = _.MaximumSizeFriendlyName,
                            Type = _.Type,
                            ValidExtensions = _.ValidExtensions,
                        }).ToList(),

                    };
                    await _unitOfWork.ApplicationSetting.AddAsync(model);
         
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("تنظیمات ثبت شد");
                    return returnUiResult;
                }
            }
            catch (Exception)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خطا در  ثبت رخ داد");
                return returnUiResult;

            }
        }

        public async Task<ApplicationSettingResponseDto> GetSetting()
        {
            try
            {
                var model = await _unitOfWork.ApplicationSetting.AsQueryable().FirstOrDefaultAsync();
                if (model == null)
                    return null;

                var newModel = new ApplicationSettingResponseDto();

                newModel.AppVersion = model.AppVersion;
                newModel.ApiVersion = model.ApiVersion;
                newModel.FileUploadSetting = model.FileUploadSetting.Select(_ => new FileUploadSettingDto
                {
                    MaximumSize = _.MaximumSize,
                    MaximumSizeFriendlyName = _.MaximumSizeFriendlyName,
                    Type = _.Type,
                    ValidExtensions = _.ValidExtensions,
                }).ToList();
                return newModel;
            }
            catch (Exception)
            {

                return null;
            }
         
        }

        #endregion

        #region Dispose 

        void IDisposable.Dispose()
        {
            if (_unitOfWork != null)
                _unitOfWork.ApplicationSetting.TryDisposeSafe();
        }

        #endregion

    }
}
