using DNTCommon.Web.Core;
using DrMeet.Api.Features.Doctors.EndPoints.DTOs;
using DrMeet.Api.Features.ServicesAvailables.DTOs;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.PagedList;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using MongoDB.Driver.Linq;

namespace DrMeet.Api.Shared.Services.ServicesAvailable;

public interface IServicesAvailableService : IDisposable
{
    #region ServicesAvailable

    Task<ReturnUiResult> CreateServicesAvailableAsync(CreateServicesAvailableRequestDto dto);
    Task<ReturnUiResult> EditServicesAvailable(UpdateServicesAvailableRequestDto dto);
    Task<ReturnUiResult> DeleteServicesAvailable(int ServicesAvailableId);
    Task<GetServicesAvailableDetailActiveResponseDto> GetServicesAvailable(int ServicesAvailableId);
    Task<List<GetServicesAvailableDetailActiveResponseDto>> GetServicesAvailables(List<GetCenterServicesAvailableListRequestDto> servicesAvailable);
    Task<PagedList<GetServicesAvailableListResponseDto>> GetServicesAvailables(GetServicesAvailableRequestResponseParams request);
    Task<GetDoctorCenterServieResponseDto> GetServicesDoctorInfoAvailables(GetServicesAvailableRequiedDoctorIdRequestResponseParams request);
    Task<PagedList<GetServicesAvailableSelectedListResponseDto>> GetServicesAvailables(GetServicesAvailableSelectListRequestResponseParams request);
    #endregion
}
public class ServicesAvailableService : IServicesAvailableService
{

    #region Constructor

    private readonly IUnitOfWork _unitOfWork;

    public ServicesAvailableService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region ServicesAvailable

    public async Task<ReturnUiResult> CreateServicesAvailableAsync(CreateServicesAvailableRequestDto dto)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var ServicesAvailableCount = await _unitOfWork.ProviderServices.AsQueryable().CountAsync();

            Domain.Others.ProviderServices ServicesAvailable = new Domain.Others.ProviderServices()
            {
                Name = dto.Name,
                Status = true,

                Order = ServicesAvailableCount + 1,

            };

            await _unitOfWork.ProviderServices.AddAsync(ServicesAvailable);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("ثبت خدمت با موفقیت انجام شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت خدمت با خطا مواجه شد");
            return returnUiResult;
        }
    }

    public async Task<ReturnUiResult> EditServicesAvailable(UpdateServicesAvailableRequestDto dto)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var ServicesAvailable = await _unitOfWork.ProviderServices.GetByIdAsync(dto.Id);

            if (ServicesAvailable == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خدمت یافت نشد");
                return returnUiResult;
            }


            ServicesAvailable.Name = dto.Name;
            ServicesAvailable.Status = dto.Status;


            await _unitOfWork.ProviderServices.UpdateAsync(ServicesAvailable);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("ثبت خدمت با موفقیت انجام شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت خدمت با خطا مواجه شد");
            return returnUiResult;
        }
    }
    public async Task<ReturnUiResult> DeleteServicesAvailable(int ServicesAvailableId)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var ServicesAvailable = await _unitOfWork
                .ProviderServices
                .AsQueryable()
                .FirstOrDefaultAsync(s => s.Id == ServicesAvailableId);

            if (ServicesAvailable == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خدمت یافت نشد");
                return returnUiResult;
            }

            await _unitOfWork.ProviderServices.DeleteAsync(ServicesAvailableId);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("حذف خدمت با موفقیت انجام  شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("خطا  رخ داد");
            return returnUiResult;
        }
    }

    public async Task<GetServicesAvailableDetailActiveResponseDto> GetServicesAvailable(int ServicesAvailableId)
    {
        try
        {
            var ServicesAvailable = await _unitOfWork
                .ProviderServices
                .AsQueryable()
                //.AsNoTraking()
                .Where(a => a.Id == ServicesAvailableId && a.Status == true)
                .Select(s => new GetServicesAvailableDetailActiveResponseDto
                {
                    Name = s.Name,

                    Order = s.Order,
                    Id = s.Id
                }).FirstOrDefaultAsync();

            if (ServicesAvailable == null)
                return null;

            return ServicesAvailable;
        }
        catch (Exception)
        {
            return null;
        }
    }
    public async Task<List<GetServicesAvailableDetailActiveResponseDto>> GetServicesAvailables(List<GetCenterServicesAvailableListRequestDto> servicesAvailable)
    {
        try
        {

            var ids = servicesAvailable.Select(_ => _.Id).ToList();

            var ServicesAvailable = _unitOfWork
                .ProviderServices
                .AsQueryable()
                //.AsNoTracking()
                .Where(a => ids.Contains(a.Id) && a.Status == true)
                .Select(s => new GetServicesAvailableDetailActiveResponseDto
                {
                    Name = s.Name,
                    Order = s.Order,
                    Id = s.Id
                });

            if (ServicesAvailable == null)
                return null;

            return await ServicesAvailable.ToListAsync();
        }
        catch (Exception)
        {
            return null;
        }
    }


    public async Task<PagedList<GetServicesAvailableListResponseDto>> GetServicesAvailables(GetServicesAvailableRequestResponseParams request)
    {
        try
        {
            var response = new PagedList<GetServicesAvailableListResponseDto>();
            var services = _unitOfWork.ProviderServices
                .AsQueryable();

            if (request.DoctorId != null)
            {
                services = services
                    .Where(_ => _.CenterDoctorsService
                    .Select(d => d.DoctorId)
                    .Equals(request.DoctorId.Value))

                    .AsQueryable();
            }
          
            if (request.Status.HasValue)
            {
                services = services
                    .Where(_ =>
                _.CenterDoctorsService != null &&

                _.CenterDoctorsService
                .Select(cds => cds.Status)
                .Equals(request.Status))
                    .AsQueryable();
            }
        


            if (!string.IsNullOrEmpty(request.Title))
            {
                services = services.Where(_ => _.CenterDoctorsService.Select(_ => _.ProviderService.Name).Contains(request.Title)).AsQueryable();
            }
              

            response.List = services
            .Select(s => new GetServicesAvailableListResponseDto
            {
                Name = s.Name,
                Id = s.Id,
                Status = s.Status
            })

             .Skip(request.PageSize.Value * (request.PageNumber!.Value - 1))
             .Take(request.PageSize.Value).ToList();


            double _totalPages = response.List.Count / request.PageSize.Value;

            var paginationParams = new PagedListInfo()
            {
                TotalCount = response.List.Count,
                PageNumber = request.PageNumber.Value,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(_totalPages)
            };
            response.Pagination = paginationParams;

            return response;
        }
        catch (Exception)
        {
            // ثبت لاگ
            return null;
        }
    }
    public async Task<PagedList<GetServicesAvailableSelectedListResponseDto>> GetServicesAvailables(GetServicesAvailableSelectListRequestResponseParams request)
    {
        try
        {
            var response = new PagedList<GetServicesAvailableSelectedListResponseDto>();

            var ServicesAvailableDtos = _unitOfWork
                 .ProviderServices
                 .AsQueryable()
                 .Where(_ => _.Status == true);

            if (!string.IsNullOrEmpty(request.Q))
                ServicesAvailableDtos = ServicesAvailableDtos.Where(_ => _.Name.Contains(request.Q));



            var result = await ServicesAvailableDtos.ToPagedList(s => new GetServicesAvailableSelectedListResponseDto
            {
                Name = s.Name,
                Id = s.Id,

            }, request.PageNumber, request.PageSize);




            return result;
        }
        catch (Exception)
        {
            // ثبت لاگ
            return null;
        }
    }
    public async Task<GetDoctorCenterServieResponseDto> GetServicesDoctorInfoAvailables(GetServicesAvailableRequiedDoctorIdRequestResponseParams request)
    {
        try
        {
            var response = new GetDoctorCenterServieResponseDto();

            //var services = new List<GetDoctorCenterServieResponseDto>();
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.DoctorId);

            if (doctor == null)
            {
                return null;
            }

            if (doctor == null)
            {
                return null;
            }
            var centers = doctor.CenterDoctors.Where(_ =>
                _.CenterDoctorsService != null &&
                _.CenterDoctorsService
                .Select(cds => cds.DoctorId)
                .Equals(request.DoctorId)

           ).AsQueryable();
            if (!string.IsNullOrEmpty(request.Title))
            {

                centers = centers.Where(_ =>
                    _.CenterDoctorsService != null &&

                    _.CenterDoctorsService
                    .Select(cds => cds.ProviderService.Name)
                    .Equals(request.Title)


               ).AsQueryable();
            }


            if (request.Status.HasValue)
            {

                centers = centers
                    .Where(_ =>
                _.CenterDoctorsService != null &&

                _.CenterDoctorsService
                .Select(cds => cds.Status)
                .Equals(request.Status))

                    .AsQueryable();

            }

            var services = await centers.SelectMany(_ => _.CenterDoctorsService).ToListAsync();

            response.Service.List = services
            .Select(s => new GetServicesAvailableListResponseDto
            {
                Name = s.ProviderService.Name,
                Id = s.ProviderServiceId.Value,
                Status = s.Status
            })

             .Skip(request.PageSize.Value * (request.PageNumber!.Value - 1))
             .Take(request.PageSize.Value).ToList();


            double _totalPages = (services.Count() / request.PageSize.Value) + 1;

            var paginationParams = new PagedListInfo()
            {
                TotalCount = services.Count(),
                PageNumber = request.PageNumber.Value,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(_totalPages)
            };
            response.Service.Pagination = paginationParams;




            if (response.doctorInfo is not null && response.doctorInfo is null)
                response.doctorInfo = new DoctorInfoDoctorCenterShiftDto();
            response.doctorInfo = new DoctorInfoDoctorCenterShiftDto { FullName = doctor.User.FullName };

            //  var result =  retunrShiftDtos.ToList();


            return response;
        }
        catch (Exception)
        {
            // ثبت لاگ
            return null;
        }
    }
    #endregion

    #region Dispose 

    void IDisposable.Dispose()
    {
        if (_unitOfWork != null)
            _unitOfWork.ProviderServices.TryDisposeSafe();
    }


    #endregion

}
