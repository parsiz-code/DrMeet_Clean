using DrMeet.Domain.Doctors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Infrastructure.Persistence.Configurations.Users;

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
