using DrMeet.Api.Shared.Domian;



using DrMeet.Api.Shared.Persistence.Repositories;
using DrMeet.Domain.ApplicationSettings;

namespace DrMeet.Api.Shared.Persistence.UnitOfWork;

using DrMeet.Api.Shared.Persistence.DbContexts.EFCore;
using DrMeet.Domain.Blogs;
using DrMeet.Domain.Centers;
using DrMeet.Domain.Doctors;
using DrMeet.Domain.Iran;
using DrMeet.Domain.Others;
using DrMeet.Domain.Patients;
using DrMeet.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;

public class UnitOfWork : IUnitOfWork
{
    private readonly DrMeetDbContext context;
    public UnitOfWork(DrMeetDbContext _context)
    {
        context = _context;
    }
    public IEFCoreRepository<ApplicationSetting> ApplicationSetting => new EFCoreRepository<ApplicationSetting>(context);
    public IEFCoreRepository<ApplicationSettingFileUpload> ApplicationSettingFileUpload => new EFCoreRepository<ApplicationSettingFileUpload>(context);
    public IEFCoreRepository<Blog> Blog => new EFCoreRepository<Blog>(context);
    public IEFCoreRepository<BlogComment> BlogComment => new EFCoreRepository<BlogComment>(context);
    public IEFCoreRepository<User> Users => new EFCoreRepository<User>(context);

    public IEFCoreRepository<Center> Centers => new EFCoreRepository<Center>(context);
    public IEFCoreRepository<CenterDoctorsSelected> CenterDoctorsSelected => new EFCoreRepository<CenterDoctorsSelected>(context);
    public IEFCoreRepository<CenterDoctorsServiceSelected> CenterDoctorsServiceSelected => new EFCoreRepository<CenterDoctorsServiceSelected>(context);
    public IEFCoreRepository<CenterDepartment> CenterDepartment => new EFCoreRepository<CenterDepartment>(context);
    public IEFCoreRepository<CenterDoctorsDepartmantSelected> CenterDoctorsDepartmantSelected => new EFCoreRepository<CenterDoctorsDepartmantSelected>(context);
    public IEFCoreRepository<CenterDoctorServiceOnlineConsultation> CenterDoctorServiceOnlineConsultation => new EFCoreRepository<CenterDoctorServiceOnlineConsultation>(context);
    public IEFCoreRepository<CenterDoctorServicePricing> CenterDoctorServicePricing => new EFCoreRepository<CenterDoctorServicePricing>(context);
    public IEFCoreRepository<CenterSocialMediaAccount> CenterSocialMediaAccount => new EFCoreRepository<CenterSocialMediaAccount>(context);
    public IEFCoreRepository<CenterQuestionAnswerCommentPoints> CenterQuestionAnswerCommentPoints => new EFCoreRepository<CenterQuestionAnswerCommentPoints>(context);
    public IEFCoreRepository<CenterInsurances> CenterInsurances => new EFCoreRepository<CenterInsurances>(context);
    public IEFCoreRepository<CenterQuestionAnswer> CenterQuestionAnswer => new EFCoreRepository<CenterQuestionAnswer>(context);
    public IEFCoreRepository<CenterType> CenterTypes => new EFCoreRepository<CenterType>(context);
    public IEFCoreRepository<CenterUserSelected> CenterUsers => new EFCoreRepository<CenterUserSelected>(context);
    public IEFCoreRepository<Patient> Patients => new EFCoreRepository<Patient>(context);

    public IEFCoreRepository<Doctor> Doctors => new EFCoreRepository<Doctor>(context);
    public IEFCoreRepository<DoctorReserveTime> DoctorReserveTimes => new EFCoreRepository<DoctorReserveTime>(context);
    public IEFCoreRepository<DoctorComment> DoctorComment => new EFCoreRepository<DoctorComment>(context);
    public IEFCoreRepository<DoctorShift> DoctorShifts => new EFCoreRepository<DoctorShift>(context);
    public IEFCoreRepository<DoctorShiftService> DoctorShiftService => new EFCoreRepository<DoctorShiftService>(context);
    public IEFCoreRepository<DoctorShiftTimeItem> DoctorShiftTimeItem => new EFCoreRepository<DoctorShiftTimeItem>(context);

    public IEFCoreRepository<IranCity> IranCities => new EFCoreRepository<IranCity>(context);
    public IEFCoreRepository<IranProvince> IranProvinces => new EFCoreRepository<IranProvince>(context);
    public IEFCoreRepository<Expertise> Expertise => new EFCoreRepository<Expertise>(context);
    public IEFCoreRepository<Insurance> Insurances => new EFCoreRepository<Insurance>(context);
    public IEFCoreRepository<Licenses> Licenses => new EFCoreRepository<Licenses>(context);
    public IEFCoreRepository<ProviderServices> ProviderServices => new EFCoreRepository<ProviderServices>(context);
    public IEFCoreRepository<Slider> Sliders=> new EFCoreRepository<Slider>(context);
    public IEFCoreRepository<Holidays> Holidays => new EFCoreRepository<Holidays>(context);

    public async Task<int> SaveChangesAsync() =>
        await context.SaveChangesAsync();
}
