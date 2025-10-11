using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Shared.PagedList;
using DrMeet.Api.Shared.Services.ParsizTeb.Models;

namespace DrMeet.Api.Shared.Services.ParsizTeb;

public interface IParsizTebApiService
{
    Task<List<GetParsizTebDoctorResponse>> GetDoctorsAsync();
    Task<PatientLoginRequest> PatientLoginAsync(PatientLoginRequestDto request);
    Task<DoctorLoginRequest> DoctorLoginAsync(DoctorLoginRequestDto request);

    Task<PagedList<GetPatientReceptionRecordResponse>> GetPatientReceptionRecordsAsync(
        GetPatientReceptionRecordParams request, int patientId);
    Task ReserveTimeAsync(ReserveTimeDto request);

    Task<GetReceptionDetailResponse?> GetReceptionDetailsAsync(string centerId, int patientId,
        int receptionId);

    Task<PagedList<GetPatientAppointmentsResponse>> GetPatientAppointmentsAsync(
        GetPatientAppointmentsRequestResponseParamsDto request, int patientId);
    Task<int> CreatePatientAsync(CreatePatientRequestDto request);
    Task<int> CreateDoctorAsync(CreateDoctorRequestDto request);

    Task<GetPatientByIdResponse> GetPatientByIdAsync(int id);
    Task<PagedList<GetPatientMedicalFileResponse>> GetPatientMedicalFilesAsync(GetPatientMedicalFilesParams request,int patientId);
}