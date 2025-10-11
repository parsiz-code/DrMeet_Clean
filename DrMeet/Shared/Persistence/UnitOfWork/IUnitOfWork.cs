using DrMeet.Api.Shared.Domian;

using DrMeet.Api.Shared.Persistence.Repositories;
using DrMeet.Domain.ApplicationSettings;
using DrMeet.Domain.Blogs;
using DrMeet.Domain.Centers;
using DrMeet.Domain.Doctors;
using DrMeet.Domain.Iran;
using DrMeet.Domain.Others;
using DrMeet.Domain.Patients;
using DrMeet.Domain.Users;

namespace DrMeet.Api.Shared.Persistence.UnitOfWork;


    public interface IUnitOfWork
    {
        IEFCoreRepository<ApplicationSetting> ApplicationSetting { get; }
        IEFCoreRepository<ApplicationSettingFileUpload> ApplicationSettingFileUpload { get; }
        IEFCoreRepository<Blog> Blog { get; }
        IEFCoreRepository<BlogComment> BlogComment { get; }
        IEFCoreRepository<User> Users { get; }

        IEFCoreRepository<Center> Centers { get; }
        IEFCoreRepository<CenterDoctorsSelected> CenterDoctorsSelected { get; }
        IEFCoreRepository<CenterDoctorsServiceSelected> CenterDoctorsServiceSelected { get; }
        IEFCoreRepository<CenterDepartment> CenterDepartment { get; }
        IEFCoreRepository<CenterDoctorsDepartmantSelected> CenterDoctorsDepartmantSelected { get; }
        IEFCoreRepository<CenterDoctorServiceOnlineConsultation> CenterDoctorServiceOnlineConsultation { get; }
        IEFCoreRepository<CenterDoctorServicePricing> CenterDoctorServicePricing { get; }
        IEFCoreRepository<CenterSocialMediaAccount> CenterSocialMediaAccount { get; }
        IEFCoreRepository<CenterQuestionAnswerCommentPoints> CenterQuestionAnswerCommentPoints { get; }
        IEFCoreRepository<CenterInsurances> CenterInsurances { get; }
        IEFCoreRepository<CenterQuestionAnswer> CenterQuestionAnswer { get; }
        IEFCoreRepository<CenterType> CenterTypes { get; }
        IEFCoreRepository<CenterUserSelected> CenterUsers { get; }
        IEFCoreRepository<Patient> Patients { get; }

        IEFCoreRepository<Doctor> Doctors { get; }
        IEFCoreRepository<DoctorReserveTime> DoctorReserveTimes { get; }
        IEFCoreRepository<DoctorComment> DoctorComment { get; }
        IEFCoreRepository<DoctorShift> DoctorShifts { get; }
        IEFCoreRepository<DoctorShiftService> DoctorShiftService { get; }
        IEFCoreRepository<DoctorShiftTimeItem> DoctorShiftTimeItem { get; }

        IEFCoreRepository<IranCity> IranCities { get; }
        IEFCoreRepository<IranProvince> IranProvinces { get; }
        IEFCoreRepository<Expertise> Expertise { get; }
        IEFCoreRepository<Insurance> Insurances { get; }
        IEFCoreRepository<Licenses> Licenses { get; }
        IEFCoreRepository<ProviderServices> ProviderServices { get; }
        IEFCoreRepository<Slider> Sliders { get; }
        IEFCoreRepository<Holidays> Holidays { get; }

        Task<int> SaveChangesAsync();
    }


