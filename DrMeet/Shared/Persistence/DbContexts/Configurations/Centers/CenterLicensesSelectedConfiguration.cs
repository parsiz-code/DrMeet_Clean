

using DrMeet.Domain.Centers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Persistence.EF.Configurations.Centers;

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="CenterLicensesSelected"/>.
/// این کلاس تنظیمات نگاشت جدول مربوط به مجوزهای انتخاب‌شده توسط مراکز درمانی را مشخص می‌کند.
/// </summary>
public class CenterLicensesSelectedConfiguration : IEntityTypeConfiguration<CenterLicensesSelected>
{
    public void Configure(EntityTypeBuilder<CenterLicensesSelected> builder)
    {
        // تعیین نام جدول در دیتابیس
        builder.ToTable("CenterLicensesSelected");

        // کلید اصلی جدول
        builder.HasKey(c => c.Id);

        // شناسه مرکز درمانی: اختیاری
        builder.Property(c => c.CenterId)
            .IsRequired(false);

        // شناسه مجوز: اختیاری
        builder.Property(c => c.LicensesId)
            .IsRequired(false);

        // رابطه با مرکز درمانی
        builder.HasOne(c => c.Center)
            .WithMany(center => center.CenterLicensesSelected)
            .HasForeignKey(c => c.CenterId)
            .OnDelete(DeleteBehavior.SetNull);

        // رابطه با مجوز
        builder.HasOne(c => c.Licenses)
            .WithMany(license => license.CenterLicensesSelected)
            .HasForeignKey(c => c.LicensesId)
            .OnDelete(DeleteBehavior.SetNull);

        // ایندکس ترکیبی برای جلوگیری از ثبت مجوز تکراری برای یک مرکز
        builder.HasIndex(c => new { c.CenterId, c.LicensesId })
            .IsUnique();
    }
}
