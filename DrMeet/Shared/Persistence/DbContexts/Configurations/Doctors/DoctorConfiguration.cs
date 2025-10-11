using DrMeet.Api.Shared.Helpers;
using DrMeet.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Infrastructure.Persistence.Configurations.Users;

/// <summary>
///    برای مدل دکتر.
/// </summary>
public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    /// <summary>
    /// پیکربندی Fluent API برای موجودیت Doctor.
    /// این پیکربندی شامل تنظیمات جدول، کلید اصلی، ویژگی‌ها، نوع داده‌ها، پیش‌فرض‌ها و محدودیت‌هاست.
    /// </summary>
    /// <param name="builder">ابزار پیکربندی موجودیت Doctor</param>
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        // تعیین کلید اصلی که از کلاس پایه به ارث رسیده
        builder.HasKey(e => e.Id);

        // تعیین نام جدول در دیتابیس برای استفاده از روش TPT (Table-per-Type)
        builder.ToTable("Doctors");

        // دوباره تأکید بر کلید اصلی برای اطمینان از صحت پیکربندی
        builder.HasKey(d => d.Id);

        // تنظیم ویژگی‌های عددی و متنی با محدودیت‌ها و مقادیر پیش‌فرض

        builder.Property(d => d.RemoteDoctorId)
            .IsRequired(); // شناسه خارجی پزشک در سیستم‌های دیگر

        builder.Property(d => d.ExperienceYears)
            .HasDefaultValue(0); // تعداد سال‌های تجربه

        builder.Property(d => d.Score)
            .HasDefaultValue(0); // امتیاز پزشک

        builder.Property(d => d.ShowPicture)
            .HasDefaultValue(true); // نمایش تصویر پزشک

        builder.Property(d => d.DayVisitTime)
            .HasDefaultValue(0); // مدت زمان ویزیت روزانه

        builder.Property(d => d.DoctorGroup)
            .HasMaxLength(100)
            .IsRequired(); // گروه تخصصی پزشک

        builder.Property(d => d.UserId)
            .IsRequired(); // شناسه کاربری مرتبط

        builder.Property(d => d.Bio)
            .HasMaxLength(1000); // بیوگرافی پزشک

        builder.Property(d => d.Description)
            .HasMaxLength(2000); // توضیحات تکمیلی


        builder.Property(d => d.NumberMedicalSystem)
            .HasMaxLength(50); // شماره نظام پزشکی

        builder.Property(d => d.ShowInOnlineReserveTime)
            .IsRequired(); // نمایش در زمان رزرو آنلاین

        builder.Property(d => d.OverFifteenYearsExperience)
            .IsRequired(); // سابقه بیش از ۱۵ سال (غیر nullable)

        builder.Property(d => d.WebSite)
            .HasMaxLength(200); // وب‌سایت شخصی پزشک

        // تنظیمات مربوط به مشاوره و قیمت‌ها

        builder.Property(d => d.InPerson)
            .IsRequired(); // آیا پزشک ویزیت حضوری دارد؟

        builder.Property(d => d.PriceInPerson)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0); // هزینه ویزیت حضوری

        builder.Property(d => d.IsVideoConsultation)
            .IsRequired(); // آیا مشاوره ویدیویی دارد؟

        builder.Property(d => d.PriceIsVideoConsultation)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0); // هزینه مشاوره ویدیویی

        builder.Property(d => d.IsPhoneConsultation)
            .IsRequired(); // آیا مشاوره تلفنی دارد؟

        builder.Property(d => d.PriceIsPhoneConsultation)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0); // هزینه مشاوره تلفنی

        builder.Property(d => d.IsTextConsultation)
            .IsRequired(); // آیا مشاوره متنی دارد؟

        builder.Property(d => d.PriceIsTextConsultation)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0); // هزینه مشاوره متنی

        builder.HasOne(d => d.IranProvince)
            .WithMany(p => p.Doctors)
            .HasForeignKey(d => d.ProvinceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.IranCity)
            .WithMany(p => p.Doctors)
            .HasForeignKey(d => d.CityId)
            .OnDelete(DeleteBehavior.Restrict);


        //builder.HasData(
        //new Doctor
        //{
        //    Id = 1,
        //    RemoteDoctorId = 1001,
        //   CreateDate=null,
        //   UpdateDate=null,
        //    ExperienceYears = 12,
        //    Score = 4.7f,
        //    ShowPicture = true,
        //    DayVisitTime = 20,
        //    DoctorGroup = "قلب و عروق",
        //    PhoneNumber = "09120000001",
        //    UserId = 9999,
        //    Bio = "متخصص قلب با بیش از ۱۰ سال تجربه",
        //    Description = "فارغ‌التحصیل دانشگاه تهران. عضو انجمن قلب اروپا.",
        //    NumberMedicalSystem = "123456",
        //    ShowInOnlineReserveTime = true,
        //    OverFifteenYearsExperience = false,
        //    WebSite = "http://doctorali.com",
        //    Address = "تهران، خیابان ولیعصر، کلینیک سلامت",
        //    InPerson = true,
        //    PriceInPerson = 500000,
        //    IsVideoConsultation = true,
        //    PriceIsVideoConsultation = 400000,
        //    IsPhoneConsultation = true,
        //    PriceIsPhoneConsultation = 300000,
        //    IsTextConsultation = false,
        //    PriceIsTextConsultation = 0
        //},
        //new Doctor
        //{
        //    Id = 2,
        //    RemoteDoctorId = 1002,
        //    CreateDate = null,
        //    UpdateDate = null,
        //    ExperienceYears = 18,
        //    Score = 4.9f,
        //    ShowPicture = true,
        //    DayVisitTime = 30,
        //    DoctorGroup = "روانپزشکی",
        //    PhoneNumber = "09120000002",
        //    UserId  = 9998,
        //    Bio = "روانپزشک با تجربه در درمان اختلالات اضطرابی",
        //    Description = "بیش از ۱۵ سال سابقه کار بالینی و تدریس.",
        //    NumberMedicalSystem = "789012",
        //    ShowInOnlineReserveTime = true,
        //    OverFifteenYearsExperience = true,
        //    WebSite = "http://drmoradi.com",
        //    Address = "اصفهان، خیابان چهارباغ بالا، مرکز روانشناسی",
        //    InPerson = false,
        //    PriceInPerson = 0,
        //    IsVideoConsultation = true,
        //    PriceIsVideoConsultation = 450000,
        //    IsPhoneConsultation = true,
        //    PriceIsPhoneConsultation = 250000,
        //    IsTextConsultation = true,
        //    PriceIsTextConsultation = 150000
        //});
    }

}
