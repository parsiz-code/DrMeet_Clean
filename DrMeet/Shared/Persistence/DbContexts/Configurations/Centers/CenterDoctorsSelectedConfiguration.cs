

using DrMeet.Domain.Centers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Persistence.EF.Configurations.Centers;

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="CenterDoctorsSelected"/>.
/// این کلاس تنظیمات نگاشت جدول مربوط به پزشکان انتخاب‌شده برای مراکز درمانی را مشخص می‌کند.
/// </summary>
public class CenterDoctorsSelectedConfiguration : IEntityTypeConfiguration<CenterDoctorsSelected>
{
    public void Configure(EntityTypeBuilder<CenterDoctorsSelected> builder)
    {
        // تعیین نام جدول
        builder.ToTable("CenterDoctorsSelected");

        // کلید اصلی
        builder.HasKey(cds => cds.Id);

        // شناسه مرکز: اختیاری
        builder.Property(cds => cds.CenterId)
            .IsRequired(false);

        // شناسه پزشک: اختیاری
        builder.Property(cds => cds.DoctorId)
            .IsRequired(false);

        // رابطه با مرکز درمانی
        builder.HasOne(cds => cds.Center)
            .WithMany(c => c.CenterDoctors)
            .HasForeignKey(cds => cds.CenterId)
            .OnDelete(DeleteBehavior.SetNull); // در صورت حذف مرکز، ارتباط قطع می‌شود

        // رابطه با پزشک
        builder.HasOne(cds => cds.Doctor)
            .WithMany(c => c.CenterDoctors)
            .HasForeignKey(cds => cds.DoctorId)
            .OnDelete(DeleteBehavior.SetNull); // در صورت حذف پزشک، ارتباط قطع می‌شود

        // ایندکس ترکیبی برای جلوگیری از ثبت پزشک تکراری در یک مرکز
        builder.HasIndex(cds => new { cds.CenterId, cds.DoctorId })
            .IsUnique();
    }
}
