using System.Collections.Specialized;
using System.Net;
using System.Text.Json;
using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Exceptions;
using DrMeet.Api.Shared.PagedList;
using DrMeet.Api.Shared.Services.HttpService;
using DrMeet.Api.Shared.Services.ParsizTeb.Models;
using DrMeet.Api.Shared.Services.Patients;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace DrMeet.Api.Shared.Services.ParsizTeb;

public class ParsizTebApiService(IHttpService httpService, IOptions<SiteSetting> options) : IParsizTebApiService
{
    private readonly string _baseUrl = options.Value.ParsizTebApiBaseUrl;

    public async Task<List<GetParsizTebDoctorResponse>> GetDoctorsAsync()
    {
        var response = await httpService.GetAsync(
            $"{_baseUrl}/api/DrMeet/doctor/list", null, GetDefaultHeaders());

        CheckResponseStatusCode(response);

        var result = Deserialize<List<GetParsizTebDoctorResponse>>(response.Content);

        return result ?? [];
    }


    public Task<DoctorLoginRequest> DoctorLoginAsync(DoctorLoginRequestDto request)
    {
        //var response =
        //     await httpService.PostAsync($"{_baseUrl}/api/DrMeet/auth/PatientLogin", request, GetDefaultHeaders());

        //CheckResponseStatusCode(response);

        //var result = Deserialize<PatientLoginResponse>(response.Content);

        //return result!;
        return null;
    }
    public async Task<PatientLoginRequest> PatientLoginAsync(PatientLoginRequestDto request)
    {
        var response =
            await httpService.PostAsync($"{_baseUrl}/api/DrMeet/auth/PatientLogin", request, GetDefaultHeaders());

        CheckResponseStatusCode(response);

        var result = Deserialize<PatientLoginRequest>(response.Content);

        return result!;
    }

    public async Task<PagedList<GetPatientReceptionRecordResponse>> GetPatientReceptionRecordsAsync(GetPatientReceptionRecordParams request, int patientId)
    {
        var query = new NameValueCollection
        {
            { "PageNumber", request.PageNumber.ToString() },
            { "PageSize", request.PageSize.ToString() },
        };

        if (!string.IsNullOrEmpty(request.CenterId))
        {
            query.Add("centerId", request.CenterId);
        }

        var response =
            await httpService.GetAsync($"{_baseUrl}/api/DrMeet/Patient/{patientId}/ReceptionRecord", query,
                GetDefaultHeaders());

        CheckResponseStatusCode(response);

        var result = Deserialize<PagedList<GetPatientReceptionRecordResponse>>(response.Content);

        return result!;
    }

    public async Task ReserveTimeAsync(ReserveTimeDto request)
    {
        var response = await httpService.PostAsync($"{_baseUrl}/api/DrMeet/ReserveTime", request, GetDefaultHeaders());
        CheckResponseStatusCode(response);
    }

    public async Task<GetReceptionDetailResponse?> GetReceptionDetailsAsync(string centerId, int patientId,
        int receptionId)
    {
        var response =
            await httpService.GetAsync(
                $"{_baseUrl}/api/DrMeet/center/{centerId}/Patient/{patientId}/reception/{receptionId}", null,
                GetDefaultHeaders());

        CheckResponseStatusCode(response);

        var result = Deserialize<GetReceptionDetailResponse>(response.Content);

        return result;
    }

    public async Task<PagedList<GetPatientAppointmentsResponse>> GetPatientAppointmentsAsync(GetPatientAppointmentsRequestResponseParamsDto request, int patientId)
    {
        var query = new NameValueCollection
        {
            { "PageNumber", request.PageNumber.ToString() },
            { "PageSize", request.PageSize.ToString() },
        };

        if (request.CenterId!=0)
        {
            query.Add("centerId", request.CenterId.Value.ToString());
        }

        var response =
            await httpService.GetAsync($"{_baseUrl}/api/DrMeet/ReserveTime/patient/{patientId}", query,
                GetDefaultHeaders());

        CheckResponseStatusCode(response);

        var result = Deserialize<PagedList<GetPatientAppointmentsResponse>>(response.Content);

        return result!;
    }


    public async Task<int> CreatePatientAsync(CreatePatientRequestDto request)
    {
        var response =
            await httpService.PostAsync($"{_baseUrl}/api/DrMeet/patient", request,
                GetDefaultHeaders());

        CheckResponseStatusCode(response);
        var result = Deserialize<CreatePatientResponse>(response.Content);
        return result!.Id;


    }

    public Task<int> CreateDoctorAsync(CreateDoctorRequestDto request)
    {
        throw new NotImplementedException();
    }



    public async Task<GetPatientByIdResponse> GetPatientByIdAsync(int id)
    {
        var response =
            await httpService.GetAsync($"{_baseUrl}/api/DrMeet/patient/{id}", null,
                GetDefaultHeaders());

        CheckResponseStatusCode(response);
        var result = Deserialize<GetPatientByIdResponse>(response.Content);
        if (result.PersonalInfo != null)
            result.PersonalInfo.Id = id;
        return result!;
    }


    public async Task<PagedList<GetPatientMedicalFileResponse>> GetPatientMedicalFilesAsync(
        GetPatientMedicalFilesParams request, int patientId)
    {
        var query = new NameValueCollection
        {
            { "PatientId", patientId.ToString() },
            { "PageNumber", request.PageNumber.ToString() },
            { "PageSize", request.PageSize.ToString() },
        };


        if (!string.IsNullOrEmpty(request.CenterId))
        {
            query.Add("centerId", request.CenterId);
        }

        var response =
            await httpService.GetAsync($"{_baseUrl}/api/DrMeet/patient/{patientId}/MedicalFile", query,
                GetDefaultHeaders());

        CheckResponseStatusCode(response);
        var result = Deserialize<PagedList<GetPatientMedicalFileResponse>>(response.Content);
        return result!;
    }


    #region Helpers

    private static TData? Deserialize<TData>(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return default;
        }

        var result = JsonSerializer.Deserialize<BaseApiResponse<TData>>(json, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        });

        return result is null ? default : result.Data;
    }

    private void CheckResponseStatusCode(RestResponse response)
    {
        if (response.StatusCode == 0)
        {
            throw new BadRequestException("سامانه پارسیزطب در دسترس نمی باشد به پشتیبانی مراجعه کنید");
        }

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            throw new BadRequestException("احراز هویت در سامانه پارسیزطب انجام نشد");
        }

        if (response.StatusCode == HttpStatusCode.InternalServerError)
        {
            throw new BadRequestException("خطایی در سامانه پارسیزطب رخ داده است");
        }

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new BadRequestException("خدمت مورد نظر در سامانه پارسیزطب یافت نشد");
        }

        if (string.IsNullOrWhiteSpace(response?.Content))
        {
            throw new BadRequestException("پاسخی از  سامانه پارسیزطب دریافت نشد");
        }


        var jsonObject = JObject.Parse(response.Content);

        var obj = jsonObject["errors"]?.ToList();

        if (obj?.Any() is true)
        {
            throw new BadRequestException(string.Join(',', obj.Values<string>()));
        }
    }


    private bool IsCheckResponseStatusCode(RestResponse response)
    {
        if (response.StatusCode == 0)
        {
            return false;
        }

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return false;

        }

        if (response.StatusCode == HttpStatusCode.InternalServerError)
        {
            return false;

        }

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return false;

        }

        if (string.IsNullOrWhiteSpace(response?.Content))
        {
            return false;

        }


        return true;
    }
    private static Dictionary<string, string> GetDefaultHeaders()
    {
        return new()
        {
            { "ApiKey", "D5EBDD06-609F-4155-BEFA-F6F66FA3CCA6" }
        };
    }



    #endregion
}