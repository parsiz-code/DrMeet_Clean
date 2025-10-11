using DrMeet.Domain.Doctors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Infrastructure.Persistence.Configurations.Users;

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="DoctorReserveTime"/>.
/// این کلاس تنظیمات نگاشت جدول مربوط به رزرو زمان توسط بیمار برای دریافت خدمت از پزشک را مشخص می‌کند.
/// </summary>
public class DoctorReserveTimeConfiguration : IEntityTypeConfiguration<DoctorReserveTime>
{
    public void Configure(EntityTypeBuilder<DoctorReserveTime> builder)
    {
        // تعیین نام جدول در دیتابیس
        builder.ToTable("DoctorReserveTimes");

        // کلید اصلی جدول
        builder.HasKey(r => r.Id);

        // توضیحات رزرو: اختیاری با محدودیت طول
        builder.Property(r => r.Description)
            .HasMaxLength(500)
            .IsRequired(false);

        // شناسه زمان شیفت پزشک: اختیاری
        builder.Property(r => r.DoctorTimeId)
            .HasMaxLength(50)
            .IsRequired(false);

        // شناسه خدمت انتخاب‌شده توسط پزشک: الزامی
        builder.Property(r => r.CenterDoctorsServiceId)
            .HasMaxLength(50)
            .IsRequired();

        // شناسه بیمار: الزامی
        builder.Property(r => r.PatientId)
            .IsRequired();

        // وضعیت رزرو: الزامی و ذخیره به‌صورت عددی
        builder.Property(r => r.ShiftStatus)
            .HasConversion<int>()
            .IsRequired();

        // تاریخ رزرو: الزامی
        builder.Property(r => r.Date)
            .IsRequired();

        // رابطه با بیمار
        builder.HasOne(r => r.Patient)
            .WithMany(p => p.DoctorReserveTimes)
            .HasForeignKey(r => r.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        // رابطه با خدمت انتخاب‌شده توسط پزشک
        builder.HasOne(r => r.CenterDoctorsServiceSelected)
            .WithMany(s => s.DoctorReserveTimes)
            .HasForeignKey(r => r.CenterDoctorsServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.DoctorShiftTimeItem)
       .WithMany(s => s.DoctorReserveTimes)
       .HasForeignKey(r => r.DoctorTimeId)
       .OnDelete(DeleteBehavior.Cascade);

        // ایندکس ترکیبی برای جلوگیری از ثبت رزرو تکراری توسط یک بیمار در یک تاریخ برای یک خدمت خاص
        builder.HasIndex(r => new { r.PatientId, r.Date, r.CenterDoctorsServiceId })
            .IsUnique();
    }
}
