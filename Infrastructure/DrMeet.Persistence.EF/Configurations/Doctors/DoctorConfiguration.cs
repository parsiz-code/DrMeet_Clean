using DrMeet.Domain.Doctors;
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

        builder.Property(d => d.Over15YearsOfExperience)
            .HasDefaultValue(false); // سابقه بیش از ۱۵ سال (nullable)

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

        // ویژگی CenterId به صورت لیست رشته‌ای است و در دیتابیس ذخیره نمی‌شود
        // مگر اینکه با ValueConverter یا جدول رابطه‌ای جداگانه مدیریت شود
        builder.Ignore(d => d.CenterId);
    }

}


/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="DoctorShiftService"/>.
/// این کلاس تنظیمات نگاشت جدول مربوط به خدمات فعال در شیفت‌های پزشک را مشخص می‌کند.
/// </summary>
public class DoctorShiftServiceConfiguration : IEntityTypeConfiguration<DoctorShiftService>
{
    public void Configure(EntityTypeBuilder<DoctorShiftService> builder)
    {
        // تعیین نام جدول در دیتابیس
        builder.ToTable("DoctorShiftServices");

        // کلید اصلی جدول
        builder.HasKey(s => s.Id);

        // شناسه شیفت پزشک: الزامی
        builder.Property(s => s.DoctorShiftId)
            .IsRequired();

        // شناسه خدمت انتخاب‌شده توسط پزشک در مرکز: الزامی
        builder.Property(s => s.CenterDoctorsServiceId)
            .IsRequired();

        // رابطه با شیفت پزشک
        builder.HasOne(s => s.DoctorShift)
            .WithMany(shift => shift.DoctorShiftServices)
            .HasForeignKey(s => s.DoctorShiftId)
            .OnDelete(DeleteBehavior.Cascade);

        // رابطه با خدمت انتخاب‌شده توسط پزشک
        builder.HasOne(s => s.CenterDoctorsServiceSelected)
            .WithMany(service => service.DoctorShiftServices)
            .HasForeignKey(s => s.CenterDoctorsServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        // ایندکس ترکیبی برای جلوگیری از ثبت خدمت تکراری در یک شیفت خاص
        builder.HasIndex(s => new { s.DoctorShiftId, s.CenterDoctorsServiceId })
            .IsUnique();
    }
}
